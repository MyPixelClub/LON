using FarmPage.Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFlyAnimation : MonoBehaviour
{
    [SerializeField] private float _ampletud;

    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Enemy _enemy;

    [SerializeField] private Vector3[] _variationHealthSliderPosition;

    private float _freqtion;
    private float _time;
    private float _offset;
    private Vector3 _startPosition;

    private HorizontalLayoutGroup _horizontalLayoutGroup;

    private bool _isFirstFrame;

    private void OnEnable()
    {
        _isFirstFrame = true;
        _freqtion = Random.Range(1f, 2f);
    }

    public void SetStartPosition(HorizontalLayoutGroup horizontalLayoutGroup)
    {
        _horizontalLayoutGroup = horizontalLayoutGroup;
    }

    private void Update()
    {
        if (_isFirstFrame)
        {
            _startPosition = transform.position;
            _startPosition += new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            _healthSlider.transform.localPosition += _variationHealthSliderPosition[Random.Range(0, _variationHealthSliderPosition.Length)];
        }
        _isFirstFrame = false;
        _horizontalLayoutGroup.enabled = false;

        if (_enemy.IsShake == false)
        {
            _time += Time.deltaTime;
            _offset = _ampletud * Mathf.Sin(_time * _freqtion);

            transform.position = _startPosition + new Vector3(0, _offset);
        }
    }
}
