using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class AbstractFriend : MonoBehaviour
{
    protected Vector3 standardScale;

    private void Start()
    {
        //�ՂɂՂ�
        Scale();
    }

    protected async void Scale()
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
}
