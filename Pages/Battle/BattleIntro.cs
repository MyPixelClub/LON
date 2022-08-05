using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Battle
{
    public class BattleIntro : MonoBehaviour
    {

        [SerializeField]
        private TextMeshProUGUI _turnText;

        public IEnumerator PlayerTurn()
        {
            gameObject.SetActive(true);
            _turnText.text = "Round 1";
            yield return new WaitForSeconds(1f);
            gameObject.SetActive(false);
        }

        public IEnumerator OpponentTurn()
        {
            gameObject.SetActive(true);
            _turnText.text = "Round 2";
            yield return new WaitForSeconds(1f);
            gameObject.SetActive(false);
        }
    }
}