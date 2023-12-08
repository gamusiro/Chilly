using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TextPanel : MonoBehaviour
{
    [SerializeField] private Image _panelImage;

    void Start()
    {
        float alpha = 0.0f;
        float time = 0.0f;
        _panelImage.DOFade(alpha, time).SetLink(this.gameObject);
    }

    public void Show()
    {
        float alpha = 120.0f / 255.0f;
        float time = 1.0f;
        _panelImage.DOFade(alpha, time).SetLink(this.gameObject);
    }

    public void Hide()
    {
        float alpha = 0.0f;
        float time = 1.0f;
        _panelImage.DOFade(alpha, time).SetLink(this.gameObject);
    }
}
