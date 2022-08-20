using Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace FarmPage.Quest
{
    public class ChapterList : MonoBehaviour
    {
        [SerializeField] private Chapter[] _chapters;

        private int _lastPassedChapterId;

        private void OnEnable()
        {
            InitAllChapter();
        }

        public void SetCountQuestPased(int currentPassedChapterId)
        {
            if(currentPassedChapterId == _lastPassedChapterId)
                _lastPassedChapterId = ++currentPassedChapterId;

            gameObject.SetActive(false);
            gameObject.SetActive(true);
        }

        private void InitAllChapter()
        {
            for (int i = 0; i < _chapters.Length; i++)
            {
                _chapters[i].Id = i;

                if (_lastPassedChapterId >= i)
                    _chapters[i].UnlockedChapter();

                if (_chapters[i].Id == _lastPassedChapterId)
                    _chapters[i].SetChapterAsLastest();
            }
        }
    }
}