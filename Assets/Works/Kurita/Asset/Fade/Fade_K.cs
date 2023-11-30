using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Fade_K : MonoBehaviour
{
    [SerializeField, CustomLabel("true：画面がクリアな状態で開始する")]private bool _isClear = true;
    [SerializeField] private Image _fadeImage;

    void Start()
    {
        _fadeImage.gameObject.SetActive(true);
        if (_isClear)
            FadeOut(0.0f);
    }

    //フェードイン
    public void FadeIn(float time)
    {
        float alpha = 1.0f;
        _fadeImage.DOFade(alpha, time).SetLink(this.gameObject);
    }

    //フェードアウト
    public void FadeOut(float time)
    {
        float alpha = 0.0f;
        _fadeImage.DOFade(alpha, time).SetLink(this.gameObject);
    }
}
