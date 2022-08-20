using FarmPage.Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterInfo : MonoBehaviour
{
    [SerializeField] private Button _startQuestButton;
    [SerializeField] private QuestConfirmWindow _confirmWindow;

    private Chapter _selectedChapter;

    private void OnEnable()
    {
        _startQuestButton.onClick.AddListener(() =>
        {
            _confirmWindow.StartQuest(_selectedChapter);
            gameObject.SetActive(false);
        });
    }

    private void OnDisable()
    {
        _startQuestButton.onClick.RemoveAllListeners();
    }

    public void InitChapter(Chapter chapter)
    {
        gameObject.SetActive(true);
        _selectedChapter = chapter;
    }
}
