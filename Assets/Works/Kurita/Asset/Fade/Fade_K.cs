using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Fade_K : MonoBehaviour
{
    [SerializeField, CustomLabel("true�F��ʂ��N���A�ȏ�ԂŊJ�n����")]private bool _isClear = true;
    [SerializeField] private Image _fadeImage;

    void Start()
    {
        if (_isClear)
            FadeOut(0.0f);
    }

    //�t�F�[�h�C��
    public void FadeIn(float time)
    {
        float alpha = 1.0f;
        _fadeImage.DOFade(alpha, time).SetLink(this.gameObject);
    }

    //�t�F�[�h�A�E�g
    public void FadeOut(float time)
    {
        float alpha = 0.0f;
        _fadeImage.DOFade(alpha, time).SetLink(this.gameObject);
    }
}