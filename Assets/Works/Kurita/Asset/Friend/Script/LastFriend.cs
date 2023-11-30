using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LastFriend : AbstractFriend
{
    [SerializeField] protected Vector3 _endPosition;
    [SerializeField] protected float _time;
    [SerializeField] protected GameObject _light;

    private void Start()
    {
        //�ړ�
        transform.DOMove(_endPosition, _time).SetLink(gameObject);

        //�ՂɂՂ�
        Scale();

        //���C�g
        Light();
    }

    protected async void Light()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(5.0f));
        _light.transform.DOScale(new Vector3(0.0f, 0.0f, 0.0f), 2.0f).SetLink(gameObject);
    }
}
