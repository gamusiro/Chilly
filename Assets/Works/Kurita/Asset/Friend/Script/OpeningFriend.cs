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

    //�H�ׂ���
    public async void Ate(Transform start, Transform end)//�����F�ǂ��Ɍ�������
    {
        //�ړ�
        _time = 0.0f;
       
        while (true)
        {
            if (this.transform == null) 
                return;

            //�z�����܂��
            this.transform.position = Vector3.Lerp(start.position, end.position, _time * 0.1f);

            //��]
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