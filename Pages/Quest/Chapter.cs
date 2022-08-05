﻿using UnityEngine;
using UnityEngine.UI;

namespace FarmPage.Quest
{
    public class Chapter : MonoBehaviour
    {
        private const float OpenSize = 786f;
        private const float CloseSize = 178f;

        [SerializeField] private RectTransform _rectTransform;

        [SerializeField] private GameObject _info;

        [SerializeField] private VerticalLayoutGroup _verticalLayoutGroup;

        [SerializeField] private ChapterList _chapterList;

        [SerializeField] private Chapter _nextChapter;

        [SerializeField] private EnemyQuestData[] _enemyQuestsData;

        [SerializeField] private RandomPrize[] _posiblePrizes;

        [SerializeField] [Range(100, 2000)] private int _exp;

        public Chapter NextChapter => _nextChapter;

        [SerializeField]
        private bool _isLocked;

        [SerializeField]
        private GameObject _lockedImage;

        private bool _isOpen;
        public EnemyQuestData[] EnemyQuestsData => _enemyQuestsData;
        public RandomPrize[] PosiblePrizes => _posiblePrizes;
        public bool IsLocked => _isLocked;
        public ChapterList ChapterList => _chapterList;
        public int Exp => _exp;

        public int Id;

        private void OnEnable()
        {
            if (_isLocked == false)
            {
                _lockedImage.SetActive(false);

                _chapterList.CloseAllChapters();
                _chapterList.InitAllChapter();
                Open();
            }
        }

        public void UnlockedChapter()
        {
            _isLocked = false;
            _lockedImage.SetActive(false);

            // Toggle();
        }

        private void Toggle()
        {
            if (_isLocked) return;

            _isOpen = !_isOpen;

            if (_isOpen)
            {
                _chapterList.CloseAllChapters();
                Open();
            }
            else
            {
                Close();
            }
        }

        private void Open()
        {
            _info.SetActive(true);
            _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, OpenSize);
            _verticalLayoutGroup.spacing += 0.1f;
            _isOpen = true;
        }

        public void Close()
        {
            _info.SetActive(false);
            _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, CloseSize);
            _verticalLayoutGroup.spacing -= 0.01f;
            _verticalLayoutGroup.spacing = 17.9f;
            _isOpen = false;
        }
    }
}