using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine.Events;

public class OpeningFriend : AbstractFriend
{
    private float _time = 0.0f;

    void Start()
    {
        Scale();
    }

    //H‚×‚ç‚ê‚é
    public async void Ate(Transform start, Transform end)//ˆø”F‚Ç‚±‚ÉŒü‚©‚¤‚©
    {
        //ˆÚ“®
        _time = 0.0f;

        while (true) 
        {
            //‹z‚¢‚Ü‚ê‚é
            this.transform.position = Vector3.Lerp(start.position, end.position, _time * 0.1f);

            //‰ñ“]
            float speed = 10.0f;
            this.transform.Rotate(0.0f, 0.0f, speed);

            await UniTask.Delay(1);
            _time += Time.deltaTime;
            Mathf.Clamp(_time, 0.0f, 1.0f);

            if (_destroyFlag == true)
            {
                if (this.gameObject)
                    Destroy(this.gameObject);
                return;
            }
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
