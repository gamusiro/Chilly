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
        Hide();
    }

    public void Show()
    {
        float alpha = 0.2f;
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
