using Cards;
using Collection;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CardCollectionCell : CardCell
{
    [SerializeField] protected Button _button;

    protected StatisticWindow _statisticWindow;

    public string Discription => Card.Discription;

    protected virtual void OnEnable()
    {
        _button.onClick.AddListener(OpenstatisticCard);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    public void InitStatisticCard(StatisticWindow statisticWindow)
    {
        _statisticWindow = statisticWindow;
    }

    private void OpenstatisticCard()    
    {
        _statisticWindow.Render(this);
    }
}
