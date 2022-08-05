using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace FarmPage.Quest
{
    public class QuestFight : MonoBehaviour
    {
        [SerializeField] private Player _player;

        [SerializeField] private QuestPlayerCards _playerCards;
        [SerializeField] private QuestEnemyCollection _enemyCollection;

        [SerializeField] private QuestPrizeWindow _winWindow;
        [SerializeField] private GameObject _loseWindow;              

        [SerializeField] private PlayerAvatarQuest _avatar;

        private Chapter _chapter;

        public void StartFight(Chapter chapter)
        {
            _chapter = chapter;
            _enemyCollection.Render(chapter.EnemyQuestsData);
            _playerCards.Render();

            gameObject.SetActive(true);
            StartCoroutine(Fight());
        }

        private IEnumerator Fight()
        {
            yield return new WaitForSeconds(1f);

            while (_enemyCollection.IsUnitsAlive && _playerCards.IsUnitsAlive)
            {
                _enemyCollection.TakeDamage(_playerCards.UnitsDamage);
                yield return new WaitForSeconds(1);

                if (_enemyCollection.IsUnitsAlive && _playerCards.IsUnitsAlive)
                {
                    _playerCards.TakeDamage(_enemyCollection.UnitsDamage);
                    yield return new WaitForSeconds(1);
                }
            }

            yield return new WaitForSeconds(0.5f);

            if (_playerCards.IsUnitsAlive)
                yield return PlayerWin();
            else
                yield return PlayerLose();

            gameObject.SetActive(false);
        }

        private IEnumerator PlayerWin()
        {
            _player.IncreaseEXP(_chapter.Exp);

            yield return new WaitForSeconds(1f);
            //_winWindow.OpenPrizeWindow(_prizes);

            if (_chapter.NextChapter != null)
            {
                _chapter.NextChapter.UnlockedChapter();
                _chapter.ChapterList.SetCountQuestPased(_chapter.NextChapter.Id);
            }
        }

        private IEnumerator PlayerLose()
        {
            _avatar.Darkening();
            yield return new WaitForSeconds(1f);
            _loseWindow.SetActive(true);
        }
    }
}
