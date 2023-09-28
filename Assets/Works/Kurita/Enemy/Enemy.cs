using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseObject
{
    [SerializeField] private List<Transform> _transformEyes;
    [SerializeField] List<Transform> _transformStandardPosition;
    private List<float> _radius = new();
    private static float _plusOrMinus;

    public override void Initialized()
    {
        foreach (var transformEyes in _transformEyes)
        {
            _radius.Add(Random.Range(0.0f, 2.0f) * Mathf.PI);
        } 
    }

    public override void Updated()
    {
        float flipFlop = 1.0f;
        for (int i = 0; i < _transformEyes.Count; i++)
        {
            _radius[i] += 0.01f * AlternatingPlusAndMinus();
            Debug.Log(flipFlop);
            Vector3 addValue = new Vector3(Mathf.Sin(_radius[i]), Mathf.Cos(_radius[i]), 0.0f);
            addValue *= 0.5f;
            _transformEyes[i].position = _transformStandardPosition[i].position + addValue;
        }
    }
    private float AlternatingPlusAndMinus()
    {
        if (_plusOrMinus > 0.0f)
        {
            _plusOrMinus = -1.0f;
            return _plusOrMinus;
        }
        else
        {
            _plusOrMinus = 1.0f;
            return _plusOrMinus;
        }
    }
}
