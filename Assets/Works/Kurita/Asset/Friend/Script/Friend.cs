using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Friend : MonoBehaviour
{
    [SerializeField] private Vector3 _endPosition;
    [SerializeField] private float _time;
    [SerializeField] private GameObject _light;

    private Vector3 standardScale;

    private void Start()
    {
        //ˆÚ“®
       transform.DOMove(_endPosition, _time).SetLink(gameObject);

        //‚Õ‚É‚Õ‚É
        Scale();

        //ƒ‰ƒCƒg
        Light();
    }

    private async void Scale()
    {
        standardScale = transform.localScale;

        while (true)
        {
            float speed = 10.0f;
            float range = 0.2f;
            Vector3 scale;
            scale.x = standardScale.x + Mathf.Sin(Time.time * speed) * range;
            scale.y = standardScale.y + Mathf.Cos(Time.time * speed) * range;
            scale.z = standardScale.z + Mathf.Sin(Time.time * speed) * range;
            transform.localScale = scale;

            int timeSpan = 1;
            await UniTask.Delay(timeSpan);
        }
    }

    private async void Light()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(5.0f));
         _light.transform.DOScale(new Vector3(0.0f, 0.0f, 0.0f), 2.0f).SetLink(gameObject);
    }
}
