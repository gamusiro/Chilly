using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TextPanel : MonoBehaviour
{
    [SerializeField] private static Image _panelImage;

    void Start()
    {
        Hide();
    }

    public static void Show()
    {
        float alpha = 1.0f;
        float time = 0.0f;
        _panelImage.DOFade(alpha, time);
    }

    public static void Hide()
    {
        float alpha = 0.0f;
        float time = 0.0f;
        _panelImage.DOFade(alpha, time);
    }
}
