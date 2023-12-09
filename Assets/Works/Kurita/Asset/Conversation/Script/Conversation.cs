using System.Collections;
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

public class Conversation : ScreenTextParent
{
    [SerializeField] private TextPanel _textPanel;
    [SerializeField] private TMPro.TextMeshProUGUI _nameTMP;//���O��\������TMP
    [SerializeField] private TMPro.TextMeshProUGUI _contentTMP;//��b����\������TMP
    [SerializeField] private List<ConversationInfo> _conversationInfoList = new();
    private float _time = 0.0f;

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
        _nameTMP.text = null;
        _contentTMP.text = null;

        //��b�J�n
        foreach (var conversationInfo in _conversationInfoList) { Display(conversationInfo); }
    }

    private async void Display(ConversationInfo conversationInfo)
    {
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        //�w�肵�����Ԃ܂őҋ@
        await UniTask.WaitUntil(() => _time > conversationInfo.StartTime, cancellationToken: token);

        _textPanel.Show();//�e�L�X�g�p�l���̕\��

        _nameTMP.text = conversationInfo.Name;//���O�̕\��

        //��b���̕\��
        float timeSpan = 0.5f;
        await UniTask.Delay(TimeSpan.FromSeconds(timeSpan), cancellationToken: token);
        string content = conversationInfo.Content;
        int len = 0;

        while (len <= content.Length)
        {
            _contentTMP.text = content.Substring(0, len);
            len++;

            //n�b���Ƃɕ������\������Ă���
            timeSpan = 1.0f / content.Length;
            await UniTask.Delay(TimeSpan.FromSeconds(timeSpan), cancellationToken: token);
        }

        //�\���̏I��
        await UniTask.WaitUntil(()=> _time > conversationInfo.StartTime + conversationInfo.DisplayTime, cancellationToken: token);
        _nameTMP.text = null;
        _contentTMP.text = null;

        _textPanel.Show();//�e�L�X�g�p�l���̔�\��
    }

    private void FixedUpdate()
    {
        _time += Time.deltaTime;
    }
}

//��b��񃊃X�g
[Serializable]
public class ConversationInfo 
{
    [CustomLabel("���O")]
    public string Name;
    [CustomLabel("��b���e")]
    public string Content;
    [CustomLabel("�\���J�n����")]
    public float StartTime;
    [CustomLabel("�\������")]
    public float DisplayTime;
}