using System.Collections;
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

public class Conversation : ScreenTextParent
{
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
        foreach (var conversationInfo in _conversationInfoList) { Draw(conversationInfo); }
    }

    private async void Draw(ConversationInfo conversationInfo)
    {
        ////�o�ߎ���
        //await UniTask.WaitForFixedUpdate();
        //_time += Time.deltaTime;

        //�����̕\�����J�n
        await UniTask.WaitUntil(() => _time > conversationInfo.StartTime);

        //���O�̕\��
        _nameTMP.text = conversationInfo.Name;


        //��b���̕\��
        float timeSpan = 0.5f;
        await UniTask.Delay(TimeSpan.FromSeconds(timeSpan));
        string content = conversationInfo.Content;
        int len = 0;

        //�����̕\��
        while (len <= content.Length)
        {
            //������\������
            _contentTMP.text = content.Substring(0, len);
            len++;

            //n�b���Ƃɕ������\������Ă���
            timeSpan = 1.0f / content.Length;
            await UniTask.Delay(TimeSpan.FromSeconds(timeSpan));

            Debug.Log("aa");
        }

        //���ꂠ�܂��낵���Ȃ��B
        while (true)
        {
            //������\�����I���
            if (_time > conversationInfo.StartTime + conversationInfo.DisplayTime)
            {
                _nameTMP.text = null;
                _contentTMP.text = null;
                return;
            }
            await UniTask.WaitForFixedUpdate();
        }
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
