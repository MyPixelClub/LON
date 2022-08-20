using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FarmPage.Quest
{
    [RequireComponent(typeof(Button))]
    public class Chapter : MonoBehaviour
    {
        [SerializeField] private Image _frame;
        [SerializeField] private Sprite _defurltFrame, _lastestChapterFrame;

        [SerializeField] private ChapterInfo _info;

        [SerializeField] private ChapterList _chapterList;

        [SerializeField] private TMP_Text _chapterNumberText;

        [Space]
        [SerializeField] private EnemyQuestData[] _enemyQuestsData;
        [SerializeField] private RandomPrize[] _posiblePrizes;
        [SerializeField] [Range(100, 2000)] private int _exp;
        [Space]

        [SerializeField] private bool _isLocked;
        [SerializeField] private GameObject _lockedImage;

        private Button _button;

        private int _id;

        public int Id 
        { 
            get 
            {
                return _id;
            } 
            set 
            {
                _id = value;
                _chapterNumberText.text = (_id + 1).ToString();
            }
        }
        public EnemyQuestData[] EnemyQuestsData => _enemyQuestsData;
        public RandomPrize[] PosiblePrizes => _posiblePrizes;
        public ChapterList ChapterList => _chapterList;
        public int Exp => _exp;


        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _lockedImage.SetActive(_isLocked);
            _button.onClick.AddListener(OpenChapterInfo);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        public void UnlockedChapter()
        {
            _isLocked = false;
            _lockedImage.SetActive(false);
            _frame.sprite = _defurltFrame;
        }

        public void SetChapterAsLastest()
        {
            _frame.sprite = _lastestChapterFrame;
        }

        private void OpenChapterInfo()
        {
            if (_isLocked) return;

            _info.InitChapter(this);
        }
    }
}