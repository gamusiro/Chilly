using System.Collections;
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;

public class Lyric : ScreenTextParent
{
    [SerializeField] private Image _panelImage;//�����̌��̃p�l��
    float _alpha = 0.0f;
    [SerializeField] private TMPro.TextMeshProUGUI _lyricTMP;//���O��\������TMP
    [SerializeField] private List<LyricInfo> _conversationInfoList = new();
    
    private void Start()
    {
        Text();
    }

    private void Text()
    {
        //�k���`�F�b�N
        if (_conversationInfoList.Count == 0)
        {
            Debug.LogWarning("��b��񃊃X�g����ł�");
            return;
        }

        //������
        _lyricTMP.text = null;
        _alpha = _panelImage.color.a;//�Z�������Ƃ��̐F
        float alpha = 0.0f;
        float time = 0.0f;
        _panelImage.DOFade(alpha, time) ;

        //��b�J�n
        foreach (var conversationInfo in _conversationInfoList) { Draw(conversationInfo); }
    }

    private async void Draw(LyricInfo luricInfo)
    {
        //�����̕\�����J�n
        await UniTask.WaitUntil(() => CS_AudioManager.Instance.TimeBGM >= luricInfo.StartTime);

        //�̎��̕\��
        _lyricTMP.text = luricInfo.Lyric;

        //�����̌��̃p�l���̕\���J�n
        if (luricInfo.Lyric != "")
        {
            float alpha = _alpha;
            float time = 0.0f;
            _panelImage.DOFade(alpha, time).SetLink(this.gameObject);
            TextPanel.Show();
        }
        else
        {
            float alpha = 0.0f;
            float time = 0.0f;
            _panelImage.DOFade(alpha, time).SetLink(this.gameObject);
            TextPanel.Show();
        }
    }
}

//��b��񃊃X�g
[Serializable]
public class LyricInfo
{
    [CustomLabel("�̎�")]
    public string Lyric;
    [CustomLabel("�\���J�n����")]
    public float StartTime;
}
