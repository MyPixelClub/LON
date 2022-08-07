using FarmPage.Quest;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPopup : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _value;
    [SerializeField] private Vector3 _offset;
    private Enemy _enemy;

    private float _currentTime, _maxTime;

    private void Update()
    {
        _currentTime -= Time.deltaTime;

        if (_currentTime <= 0.0f)
        {
            _image.gameObject.SetActive(false);
            _value.gameObject.SetActive(false);
        }
        else
        {
            var imageColor = _image.color;
            var valueColor = _value.color;
            imageColor.a = _currentTime / _maxTime;
            valueColor.a = _currentTime / _maxTime;
            _image.color = imageColor;
            _value.color = valueColor;
            

            MoveText();
        }
    }

    public void StartMovement(Enemy enemy)
    {
        _image.gameObject.SetActive(true);
        _value.gameObject.SetActive(true);

        _enemy = enemy;
        _currentTime = 2.0f;
        _maxTime = 2.0f;
    }

    private void MoveText()
    {
        float delta = 1.0f - (_currentTime - _maxTime);
        Vector3 position = _enemy.transform.position - _offset + new Vector3(delta, delta, 0.0f);
        _image.transform.position = position;
        _value.transform.position = position - new Vector3(0, 0.15f);
    }
}
