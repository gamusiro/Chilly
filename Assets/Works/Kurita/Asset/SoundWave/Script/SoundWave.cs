using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SoundWave : MonoBehaviour
{
    [SerializeField] private float _maxScale;
    [SerializeField] private float _maxDistance;
    [SerializeField] List<Renderer> _rendererList = new();
    [SerializeField] private float _time;
 
    void Start()
    {
        //サイズ変更
        this.transform.DOScale(_maxScale, _time).SetLink(gameObject);
        //移動
        this.transform.DOLocalMoveZ(_maxDistance, _time).SetLink(gameObject);
        //透明化
        foreach(var renderer in _rendererList)
        {
            float alpha = 0.0f;
            renderer.material.DOFade(alpha, _time);
        }
    }
}
