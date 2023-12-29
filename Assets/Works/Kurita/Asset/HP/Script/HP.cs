using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HP : MonoBehaviour
{
    [SerializeField] private List<Image> _hpImageList;//�\���摜
    int _hp = 0;//���݂̗̑�
    bool _isAlive;
    private Tweener tweener;

    public bool Die
    {
        get { return !_isAlive; }
    }

    private void Start()
    {
        _hp = _hpImageList.Count - 1;//HP�̒l��ݒ肷��
        _isAlive = true;
        tweener = null;


        Recover();
    }

    public void Hit()
    {
        //���ɃA�j���[�V�������ݒ肳��Ă���΍폜����
        if (tweener != null)
            tweener.Kill();

        //����Ă��Ȃ���Ώ����𑱍s
        if (_hp - 1 < 0)
        {
            _isAlive = false;
            return;
        }

        //�A�j���[�V����
        float alpha = 0.0f;
        float time = 1.0f;
        tweener = _hpImageList[_hp].DOFade(alpha, time).SetLink(this.gameObject);

        //�̗͂����炷
        _hp--;
    }

    public void Recover()
    {
        //���ɃA�j���[�V�������ݒ肳��Ă���΍폜����
        if (tweener != null)
            tweener.Kill();

        //�̗͂�����ɂȂ��Ă��Ȃ���Ώ����𑱍s
        if (_hp >= _hpImageList.Count - 1)
            return;

        //�̗͂����炷
        _hp++;

        //�A�j���[�V����
        float alpha = 1.0f;
        float time = 1.0f;
        tweener = _hpImageList[_hp].DOFade(alpha, time).SetLink(this.gameObject);
    }
}
