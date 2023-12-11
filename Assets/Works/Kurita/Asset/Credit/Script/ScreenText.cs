using System.Collections;
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
using System.Threading;

public class ScreenText : ScreenTextParent
{
    [SerializeField] private TextPanel _textPanel;
    [SerializeField] private TMPro.TextMeshProUGUI _contentTMP;//������\������TMP
    [SerializeField] private List<ScreenTextInfo> _screenTextInfoList = new();
    private float _time = 0.0f;
    private float _standardFontSize;

    private void Start()
    {
        Text();
    }

    private void Text()
    {
        //�k���`�F�b�N
        if (_screenTextInfoList.Count == 0)
        {
            Debug.LogWarning("�N���W�b�g���X�g����ł�");
            return;
        }

        //������
        _standardFontSize = _contentTMP.fontSize;
        float alpha = 0.0f;
        foreach (var screenText in _screenTextInfoList){ _contentTMP.alpha = alpha; }

        //�N���W�b�g�\��
        foreach (var screenText in _screenTextInfoList){ Display(screenText); }
    }

    private async void Display(ScreenTextInfo screenText)
    {
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        float fadeTime = 0.5f;
        float alpha;

        //�����̃t�F�[�h�C��
        await UniTask.WaitUntil(() => _time > screenText.StartTime, cancellationToken: token);

        _contentTMP.text = screenText.Content;//�\�����e���X�V
        _contentTMP.fontSize = screenText.Size;
        alpha = 1.0f;//�t�F�[�h�̐ݒ�
        _contentTMP.DOFade(alpha, fadeTime)
            .SetLink(gameObject);

        if (screenText.ShowPanel)
            _textPanel.Show();//�e�L�X�g�p�l���̕\��

        //�����̃t�F�[�h�A�E�g
        await UniTask.WaitUntil(() => _time > screenText.StartTime + screenText.DisplayTime, cancellationToken: token);
        alpha = 0.0f;
        _contentTMP.DOFade(alpha, fadeTime)
            .OnComplete(_textPanel.Hide)
            .SetLink(gameObject);
    }

    private void FixedUpdate()
    {
        _time += Time.deltaTime;
    }
}

//��b��񃊃X�g
[Serializable]
public class ScreenTextInfo
{
    [CustomLabel("�\�����e"), TextArea]
    public string Content;
    [CustomLabel("�\���J�n����")]//(Instance��������v��)
    public float StartTime;
    [CustomLabel("�\������")]
    public float DisplayTime;
    [CustomLabel("�����T�C�Y")]
    public float Size;
    [CustomLabel("�e�L�X�g�p�l����\������")]
    public bool ShowPanel = true; 
}

//�C���X�^���V�G�C�g