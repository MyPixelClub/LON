using Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace FarmPage.Quest
{
    public class ChapterList : MonoBehaviour
    {
        [SerializeField] private Chapter[] _chapters;

        [SerializeField] private Scrollbar _srollbar;

        private int _countQuestPassed; 

        public void InitAllChapter()
        {
            _srollbar.value = 1f;

            for (int i = 0; i < _chapters.Length; i++)
            {
                if (_countQuestPassed >= i)
                    _chapters[i].UnlockedChapter();

                if (_countQuestPassed > i)
                    _srollbar.value -= 0.2f;

                _chapters[i].Id = i;
            }
        }

        public void CloseAllChapters()
        {
            foreach (var chapter in _chapters)
                chapter.Close();
        }

        public void SetCountQuestPased(int lastPasedQuestId)
        {
            if(lastPasedQuestId > _countQuestPassed)
                _countQuestPassed = lastPasedQuestId;
        }
    }
}