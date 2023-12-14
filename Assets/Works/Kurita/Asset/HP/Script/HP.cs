using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HP : MonoBehaviour
{
    [SerializeField] private List<Image> _hpImageList;//�\���摜
    int _hp = 0;//���݂̗̑�

    private void Start()
    {
        _hp = _hpImageList.Count - 1;//HP�̒l��ݒ肷��

       
        Recover();
    }

    public void Hit()
    {
        //����Ă��Ȃ���Ώ����𑱍s
        if (_hp - 1 < 0)
            return;

        //�A�j���[�V����
        float alpha = 0.0f;
        float time = 1.0f;
        _hpImageList[_hp].DOFade(alpha, time).SetLink(this.gameObject);

        //�̗͂����炷
        _hp--;

        //Debug.Log("�_���[�W: " + _hp);
    }

    public void Recover()
    {
        //�̗͂�����ɂȂ��Ă��Ȃ���Ώ����𑱍s
        if (_hp >= _hpImageList.Count - 1)
            return;

        //�̗͂����炷
        _hp++;

        //�A�j���[�V����
        float alpha = 1.0f;
        float time = 1.0f;
        _hpImageList[_hp].DOFade(alpha, time).SetLink(this.gameObject);

        //Debug.Log("��: " + _hp);
    }
}
