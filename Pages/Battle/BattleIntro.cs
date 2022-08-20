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

        public IEnumerator PlayRoundIntro(int roundNumber)
        {
            gameObject.SetActive(true);
            _turnText.text = $"Round {roundNumber}";
            yield return new WaitForSeconds(1f);
            gameObject.SetActive(false);
        }
    }
}