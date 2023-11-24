using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Friend : MonoBehaviour
{
    [SerializeField] private float _positionY;
    [SerializeField] private float _time;

    void Start()
    {
       transform.DOMoveY(_positionY, _time).SetLink(gameObject);
    }
}
