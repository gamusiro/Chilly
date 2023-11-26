using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;

public class Bell : MonoBehaviour
{
    [SerializeField] private GameObject _root;
    private bool RingFlag = false;
    private float radian = 0.0f;

    void Start()
    {
        Ring();
    }

    private async void Ring()
    {
        await UniTask.WaitUntil(() => RingFlag);

        while (true)
        {
            //�x����h�炷
            radian += Time.deltaTime;
            float range = 40.0f;
            Vector3 rotate = _root.transform.eulerAngles;
            rotate.z = Mathf.Sin(Mathf.PI * 6.0f / 6.0f + radian) * range;
            _root.transform.eulerAngles = rotate;

            int timeSpan = 1;
            await UniTask.Delay(timeSpan);

            //�E�o
            if (RingFlag)
                continue;

            rotate = _root.transform.eulerAngles;
            rotate.z = 0.0f;
            _root.transform.eulerAngles = rotate;
            return;
        }
    }

    public void SetRing(bool flag)
    {
        RingFlag = flag;
    }

    public Vector3 GetPosition()
    {
        return _root.transform.position;
    }
}
