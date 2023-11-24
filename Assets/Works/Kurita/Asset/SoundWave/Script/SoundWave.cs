using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SoundWave : MonoBehaviour
{
    [SerializeField] private Vector3 _maxScale;
    [SerializeField] private float _time;
 
    void Start()
    {
        this.transform.DOScale(_maxScale, _time).SetLink(gameObject);
    }
}
