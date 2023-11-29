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
    }

    //�H�ׂ���
    public async void Ate(Transform start, Transform end)//�����F�ǂ��Ɍ�������
    {
        //�ړ�
        _time = 0.0f;
        while (true)
        {
            //�z�����܂��
            this.transform.position = Vector3.Lerp(start.position, end.position, _time * 0.1f);

            //��]
            float speed = 10.0f;
            this.transform.Rotate(0.0f, 0.0f, speed);

            await UniTask.WaitForFixedUpdate();
            _time += Time.deltaTime;
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
