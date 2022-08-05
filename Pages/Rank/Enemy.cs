using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RankPage
{ 
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Image _avatar;
        [SerializeField] private TMP_Text _name, _amountPoints, _placeInRank;

        public void Render(Sprite avatar, string name, int amountPoints, int placeInRank)
        {
            _avatar.sprite = avatar;
            _name.text = name;
            _amountPoints.text = amountPoints.ToString(); 
            _placeInRank.text = placeInRank.ToString();
        }
    }
}
