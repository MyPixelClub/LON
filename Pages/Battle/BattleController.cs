using System;
using System.Collections;
using System.Collections.Generic;
using Battle;
using Cards.Card;
using DG.Tweening;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;
using Collection;
using System.Linq;

namespace FarmPage.Battle
{
    public class BattleController : MonoBehaviour
    {
        private static readonly int Effect = Animator.StringToHash("Effect");

        [SerializeField]
        private BattleAnimator _battleAnimator;

        [SerializeField] 
        private BattleIntro _roundIntro;

        [SerializeField]
        private CardAnimator[] _enemyCardAnimators;

        [SerializeField]
        private CardAnimator[] _playerCardAnimators;

        [SerializeField] 
        private Shaking shaking;

        [SerializeField] 
        private Animator _turnEffect;

        [SerializeField] 
        private GameObject _battleChouse;

        [SerializeField] 
        private Window _winWindow;
        
        [SerializeField]
        private Window _loseWindow;

        [SerializeField] private PrizeWindow _prizeWindow;

        [SerializeField] private AttackDeck _attackDeck;

        private EnemyBattle _enemy;
        private Card[] _enemyCards;
       // private int _previousRandomNumber = -1;

        [HideInInspector] public RandomPrize[] _randomPrizes;

        public void InitBattle(EnemyBattle enemy)
        {
            _enemy = enemy;
        }

        public void StartFight()
        {
            RenderEnemyDefCard();
            
            gameObject.SetActive(true);
            
            foreach (var playerCard in _playerCardAnimators) 
                playerCard.Hide();

            foreach (var enemyCard in _enemyCardAnimators) 
                enemyCard.Hide();

            HideNonAllActiveCards();

            StartCoroutine(Fight());
        }

        private void HideNonAllActiveCards()
        {            
            HideNonActiveCards(_attackDeck.CardsInDeck, _playerCardAnimators);
        }

        private void RenderEnemyDefCard()
        {
            _enemyCards = _enemy.Cards.ToArray();
            
            for (int i = 0; i < _enemy.Cards.Count; i++)
            {
                _enemyCardAnimators[i].Init(_enemyCards[i]);
            }
        }

        private IEnumerator Fight()
        {
            yield return _battleAnimator.AppearanceCards(_enemyCardAnimators, _playerCardAnimators, 
                GetAliveCards(GetCardArrayFrom(_attackDeck.CardsInDeck)), GetAliveCards(_enemyCards));

            int roundCounter = 0;
            while (GetAmountCardsHealth(_enemyCardAnimators) > 0 && GetAmountCardsHealth(_playerCardAnimators) > 0)
            {
                roundCounter++;
                yield return _roundIntro.PlayRoundIntro(roundCounter);
                yield return new WaitForSeconds(0.5f);
                yield return roundCounter % 2 != 0 ? PlayerTurn() : EnemyTurn();
            }
            
            if (GetAmountCardsHealth(_playerCardAnimators) >= GetAmountCardsHealth(_enemyCardAnimators))
                yield return PlayerWin();
            else
                yield return PlayerLose();
        }

        private IEnumerator PlayerTurn()
        {
            var playerAliveCardNumbers = GetAliveCards(GetCardArrayFrom(_attackDeck.CardsInDeck));
            var enemyAliveCardNumbers = GetAliveCards(_enemyCards);

            yield return Turn(playerAliveCardNumbers, enemyAliveCardNumbers,
                _playerCardAnimators.ToList(), _enemyCardAnimators.ToList(),
                GetCardArrayFrom(_attackDeck.CardsInDeck).ToList(), _enemyCards.ToList());
        }

        private IEnumerator EnemyTurn()
        {
            var playerAliveCardNumbers = GetAliveCards(GetCardArrayFrom(_attackDeck.CardsInDeck));
            var enemyAliveCardNumbers = GetAliveCards(_enemyCards);

            yield return Turn(enemyAliveCardNumbers, playerAliveCardNumbers, 
                _enemyCardAnimators.ToList(), _playerCardAnimators.ToList(),
                _enemyCards.ToList(), GetCardArrayFrom(_attackDeck.CardsInDeck).ToList());
        }

        private IEnumerator Turn(List<int> myAliveCardNumbers, List<int> opponentAliveCardNumbers, 
            List<CardAnimator> myCardAnimators, List<CardAnimator> opponentCardAnimators, List<Card> myCards, List<Card> opponentCards)
        {
            var randomMyCardDamageCount = _enemyCardAnimators.Length < 2 ? 1 : 2;

            for (int j = 0; j < randomMyCardDamageCount; j++)
            {
                if (FindRandomAliveCard(opponentCardAnimators) == null)
                    yield break;

                var myCardAnimator = FindRandomAliveCard(myCardAnimators);
                if (myCardAnimator == null) throw new InvalidOperationException();

                Card randomMyCard = myCardAnimator.Card;

                myCardAnimator.Selected();
                yield return new WaitForSeconds(0.2f);

                var randomOpponentCardDamageCount = 1;

                if (opponentCardAnimators.Count > 2)
                    randomOpponentCardDamageCount = Random.Range(1, 3);

                var attackEffect = randomMyCard.AttackEffect;
                var attack = randomMyCard.Attack;

                if (IsRandomChange(randomMyCard.SkillChance))
                {
                    var skillEffect = randomMyCard.SkillEffect;
                    
                    foreach (var opponentCardAnimator in opponentCardAnimators)
                        StartCoroutine(opponentCardAnimator.Hit(skillEffect, attack));

                    yield return new WaitForSeconds(0.2f);
                    shaking.Shake(0.5f, 10);
                    yield return new WaitForSeconds(0.5f);
                }
                else
                {
                    for (int k = 0; k < randomOpponentCardDamageCount; k++)
                    {
                        CardAnimator opponentCardAnimator = FindRandomAliveCard(opponentCardAnimators);

                        if (opponentCardAnimator == null) yield break;

                        var myAnimatorPosition = myCardAnimator.transform.position;
                        var opponentAnimatorPosition = opponentCardAnimator.transform.position;
                            
                        float angleTurnEffect = 
                            Mathf.Atan2(myAnimatorPosition.y - opponentAnimatorPosition.y, 
                                myAnimatorPosition.x - opponentAnimatorPosition.x) * Mathf.Rad2Deg;
                            
                        var turnEffectPosition =
                            new Vector3(
                                (myAnimatorPosition.x + opponentAnimatorPosition.x) / 2, 
                                transform.position.y, transform.position.z);
                            
                        var turnEffect = 
                            Instantiate(_turnEffect, turnEffectPosition, new Vector3(0, 0, angleTurnEffect)
                                .EulerToQuaternion(), transform);

                        var turnEffectImage = turnEffect.GetComponentInChildren<Image>();
                        turnEffectImage.color = Color.clear;
                        turnEffectImage.DOColor(Color.white, 0.2f);
                            
                        var ratioScale = 1f;
                        var ratioScaleRotation = (opponentAnimatorPosition.x - myAnimatorPosition.x) < 0 ? 1 : -1;
                        var scale = ratioScale * (opponentAnimatorPosition.x - myAnimatorPosition.x) *
                                    ratioScaleRotation;

                        if (Math.Abs(scale) < 1f)
                            scale = -1;
                            
                        turnEffect.transform.localScale = turnEffect.transform.localScale.ToX(scale);
                        turnEffect.SetTrigger(Effect);
                            
                        StartCoroutine(opponentCardAnimator.Hit(attackEffect, attack));

                        
                        yield return new WaitForSeconds(0.2f);
                        shaking.Shake(0.5f, 10);
                        yield return new WaitForSeconds(0.1f);
                            
                        turnEffectImage.DOColor(Color.clear, 0.2f).OnComplete(()=>Destroy(turnEffect.gameObject));
                    }

                    myCardAnimator.Unselected();
                    
                    yield return new WaitForSeconds(0.7f);
                }
            }

            yield return new WaitForSeconds(1);
        }

        private CardAnimator FindRandomAliveCard(List<CardAnimator> opponents)
        {
            var randomOpponent = opponents[Random.Range(0, opponents.Count)];

            if (randomOpponent != null & randomOpponent.HealthLeft > 0)
                return randomOpponent;
            else
                foreach (var opponent in opponents)
                    if (opponent != null & opponent.HealthLeft > 0)
                        return FindRandomAliveCard(opponents);

            return null;
        }

        private bool IsRandomChange(float change) => 
            Random.Range(0, 10000) <= (int)(change * 100);

        private int GetAmountCardsHealth(CardAnimator[] cards)
        {
            int amountHealt = 0;

            foreach (CardAnimator card in cards)
                if (card != null)
                amountHealt += card.HealthLeft;

            return amountHealt;
        }

        private List<int> GetAliveCards(Card[] cards)
        {
            var aliveCards = new List<int>();
            
            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i] != null) 
                    aliveCards.Add(i);
            }

            return aliveCards;
        }
    
        private void HideNonActiveCards(List<CardCellInDeck> cards, CardAnimator[] cardAnimators)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i].IsSet == false)
                    cardAnimators[i].Hide();
                else
                    cardAnimators[i].Init(cards[i].Card);
            }
        }

        private Card[] GetCardArrayFrom(List<CardCellInDeck> card)
        {
            var cards = new Card[card.Count];

            for (int i = 0; i < card.Count; i++)
            {
                cards[i] = card[i].Card;
            }

            return cards;
        }

        private IEnumerator PlayerWin()
        {
            yield return new WaitForSeconds(1);
            _winWindow.ShowSmooth(_enemy);
            _prizeWindow.Render(_enemy.RandomPrizes);
        }

        private IEnumerator PlayerLose()
        {
            yield return new WaitForSeconds(1);
            _loseWindow.ShowSmooth(_enemy);
        }
    }
}
