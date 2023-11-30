using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class OpeningFriend : AbstractFriend
{

    private float _time = 0.0f;

    void Start()
    {
        Scale();
        if (this.transform)
            return;
    }

    //食べられる
    public async void Ate(Transform start, Transform end)//引数：どこに向かうか
    {
        //移動
        _time = 0.0f;
       
        while (true)
        {
            if (this.transform == null) 
                return;

            //吸い込まれる
            this.transform.position = Vector3.Lerp(start.position, end.position, _time * 0.1f);

            //回転
            float speed = 10.0f;
            this.transform.Rotate(0.0f, 0.0f, speed);

            await UniTask.Delay(1);
            _time += Time.deltaTime;
            Mathf.Clamp(_time, 0.0f, 1.0f);
        }
    }

    private async void Rotate()
    {
        while(true)
        {
            float speed = 10.0f;
            this.transform.Rotate(0.0f, 0.0f, speed);
            await UniTask.WaitForFixedUpdate();
        }
    }
}
