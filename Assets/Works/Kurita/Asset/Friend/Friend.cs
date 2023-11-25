using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Friend : MonoBehaviour
{
    [SerializeField] private float _positionY;
    [SerializeField] private float _time;
    [SerializeField] private GameObject _light;

    void Start()
    {
       transform.DOMoveY(_positionY, _time).SetLink(gameObject);
        _light.transform.DOScale(new Vector3(0.0f, 0.0f, 0.0f), 1.0f);
    }
}
