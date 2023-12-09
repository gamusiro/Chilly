using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine.Events;
using System.Threading;

public class OpeningFriend : AbstractFriend
{
    private float _time = 0.0f;

    void Start()
    {
        Scale();
    }

    //�H�ׂ���
    public async void Ate(Transform start, Transform end)//�����F�ǂ��Ɍ�������
    {
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        //�ړ�
        _time = 0.0f;

        while (true) 
        {
            //�z�����܂��
            this.transform.position = Vector3.Lerp(start.position, end.position, _time * 0.1f);

            //��]
            float speed = 10.0f;
            this.transform.Rotate(0.0f, 0.0f, speed);

            await UniTask.Delay(1, cancellationToken: token);
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
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        while (true)
        {
            float speed = 10.0f;
            this.transform.Rotate(0.0f, 0.0f, speed);
            await UniTask.WaitForFixedUpdate(cancellationToken: token);
        }
    }
}
