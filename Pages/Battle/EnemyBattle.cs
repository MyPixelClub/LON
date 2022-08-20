using System.Collections;
using System.Collections.Generic;
using FarmPage.Battle;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class EnemyBattle : MonoBehaviour
{
    [SerializeField] private Image _avatar;
    [SerializeField] private TMP_Text _name, _powerValue;

    [SerializeField] private List<Card> _enemyCards;

    [SerializeField] private BattleConfirmWindow _battleConfirmWindow;

    [SerializeField] private RandomPrize[] _randomPrizes;

    [SerializeField] private GameObject _selectionFrame;
    [SerializeField] private EnemyBattle[] _otherEenemies;

    private Button _button;

    public GameObject SelectionFrame => _selectionFrame;
    public List<Card> Cards => _enemyCards;
    public RandomPrize[] RandomPrizes => _randomPrizes;

    public Sprite Avatar => _avatar.sprite;
    public string Name => _name.text;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(SelectEnemy);
        ShowPowerValue();
        _selectionFrame.SetActive(false);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void SelectEnemy()
    {
        UnSelectOtherEnemy();
        _selectionFrame.SetActive(true);
        _battleConfirmWindow.SelectEnemyCards(this);
    }

    private void UnSelectOtherEnemy()
    {
        foreach (var enemy in _otherEenemies)
            enemy.SelectionFrame.SetActive(false);
    }

    private void ShowPowerValue()
    {
        int amountPower = 0;

        foreach (var card in _enemyCards)
        {
            amountPower += card.Attack + card.Health;
        }

        _powerValue.text = amountPower.ToString();
    }
}