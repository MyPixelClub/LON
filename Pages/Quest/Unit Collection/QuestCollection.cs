using FarmPage.Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class QuestCollection : MonoBehaviour
{
    public event System.Action OnAllUnitsDeaded;

    [SerializeField] private Transform _unitContainer;
    [SerializeField] private Unit _unitPrefab;

    [SerializeField] private Shaking _shaking;
    [SerializeField] private ParticleSystem[] _attackEffect;    

    private Unit[] _units;

    public int UnitsDamage
    {
        get
        {
            StartCoroutine(FindRandomUnit().Selected());


            var damage = 0;

            foreach (var unit in _units)
                damage += unit.Damage();

            return damage / _units.Length;
        }
    }

    public bool IsUnitsAlive
    {
        get
        {
            foreach (var unit in _units)
                if (unit.IsAlive)
                    return true;

            return false;
        }
    }
    public void Render()
    {
        _units = GetArrayType();

        foreach (Transform unit in _unitContainer)
            Destroy(unit.gameObject);

        for (int i = 0; i < _units.Length; i++)
        {
            _units[i] = Instantiate(_unitPrefab, _unitContainer);
            InitUnit(_units[i], i);
        }
    }

    public IEnumerator TakeDamage(int amountDamage)
    {
        yield return new WaitForSeconds(0.45f);

        if (amountDamage < 0)
            throw new System.AggregateException();

        var unit = FindRandomUnit();

        if (unit != null)
        {
            unit.TakeDamage(amountDamage);

            var effect = Instantiate(_attackEffect[Random.Range(0, _attackEffect.Length)], unit.transform.position, Quaternion.identity);
            effect.transform.localScale = new(0.2f, 0.2f);
            _shaking.Shake(0.5f, 10);
            Destroy(effect.gameObject, 1);
        }
    }

    private Unit FindRandomUnit()
    {
        var randomTarget = _units[Random.Range(0, _units.Length)];

        if (randomTarget.IsAlive)
            return randomTarget;
        else
            foreach (var enemy in _units)
                if (enemy.IsAlive)
                    return FindRandomUnit();

        OnAllUnitsDeaded?.Invoke();
        return null;
    }

    protected abstract Unit[] GetArrayType();
    protected abstract void InitUnit(Unit unit, int position);
}
