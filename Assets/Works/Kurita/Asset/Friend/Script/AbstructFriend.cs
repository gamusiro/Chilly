using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading;

public abstract class AbstractFriend : MonoBehaviour
{
    protected Vector3 standardScale;
    protected bool _destroyFlag = false;

    private void Start()
    {
        //‚Õ‚É‚Õ‚É
        Scale();
    }

    protected async void Scale()
    {
        var token = this.GetCancellationTokenOnDestroy();
  
        standardScale = transform.localScale;

        while (_destroyFlag == false) 
        {
            float speed = 10.0f;
            float range = 0.2f;
            Vector3 scale;
            scale.x = standardScale.x + Mathf.Sin(Time.time * speed) * range;
            scale.y = standardScale.y + Mathf.Cos(Time.time * speed) * range;
            scale.z = standardScale.z + Mathf.Sin(Time.time * speed) * range;

            if (_destroyFlag == false) 
                transform.localScale = scale;

            int timeSpan = 1;
            await UniTask.Delay(timeSpan);

            if (_destroyFlag == true)
            {
                if (this.gameObject)
                    Destroy(this.gameObject);
                return;
            }
        }
    }

    public void SetDestroy()
    {
        _destroyFlag = true;
    }
}
