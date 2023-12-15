using System.Collections;
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine.UI;
using DG.Tweening;

public class Conversation : ScreenTextParent
{
    public enum IconList { Player, Friend, Enemy }

    [SerializeField] private TextPanel _textPanel;
    [SerializeField] private TMPro.TextMeshProUGUI _nameTMP;//���O��\������TMP
    [SerializeField] private TMPro.TextMeshProUGUI _contentTMP;//��b����\������TMP
    [SerializeField] private Image _icon;
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
        this.HideIcon();

        //��b�J�n
        foreach (var conversationInfo in _conversationInfoList) { Display(conversationInfo); }
    }

    private async void Display(ConversationInfo conversationInfo)
    {
        //�L�����Z��
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        //�w�肵�����Ԃ܂őҋ@
        await UniTask.WaitUntil(() => _time > conversationInfo.StartTime, cancellationToken: token);

        //�e�L�X�g�p�l���̕\��
        _textPanel.Show();

        //���O�̕\��
        _nameTMP.text = conversationInfo.Name;

        //�A�C�R���̕\���ʒu��ݒ�
        ShowIcon(conversationInfo, token);

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
        this.HideIcon();

        _textPanel.Show();//�e�L�X�g�p�l���̔�\��
    }

    private async void ShowIcon(ConversationInfo conversationInfo, CancellationToken token)
    {
        //�A�C�R�����ݒ肳��Ă��Ȃ���΃��^�[��
        if (conversationInfo.Icon == null)
        {
            this.HideIcon();
            return;
        }

        //�A�C�R���̕\���ʒu��TMP�̏������Ƃɐݒ肷��B
        //TMP�̏��X�V�ɂ̓��O������̂őҋ@����B
        await UniTask.WaitUntil(() => _nameTMP.text.Length == _nameTMP.textInfo.characterCount);

        //�X�v���C�g��ݒ肷��
        _icon.sprite = conversationInfo.Icon;
        float alpha = 1.0f;
        float time = 0.0f;
        _icon.material.DOFade(alpha, time)
            .SetLink(this.gameObject);

        //���W��ݒ肷��
        float half = 0.5f;
        float correct = 50.0f;
        Vector3 position = Vector3.zero;
        position += _nameTMP.transform.position;
        position += _nameTMP.textInfo.characterInfo[0].topLeft * half;
        position += _nameTMP.textInfo.characterInfo[0].bottomRight * half;
        position.x -= correct;
        _icon.transform.position = position;
    }

    private void FixedUpdate()
    {
        _time += Time.deltaTime;
    }

    private void HideIcon()
    {
        float alpha = 0.0f;
        float time = 0.0f;
        _icon.material.DOFade(alpha, time)
            .SetLink(this.gameObject);
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
    [CustomLabel("�A�C�R��")]
    public Sprite Icon;
}