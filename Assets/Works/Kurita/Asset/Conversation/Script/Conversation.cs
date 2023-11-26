using System.Collections;
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

public class Conversation : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _nameTMP;//���O��\������TMP
    [SerializeField] private TMPro.TextMeshProUGUI _contentTMP;//��b����\������TMP
    [SerializeField] private List<ConversationInfo> _conversationInfoList = new();
    int _contentIndex = 0;

    private void Start()
    {
        Content();
    }

    private async void Content()
    {
        //�k���`�F�b�N
        if (_conversationInfoList.Count == 0)
        {
            Debug.LogWarning("��b��񃊃X�g����ł�");
            return;
        }    

        //���O�̕\��
        _nameTMP.text = _conversationInfoList[_contentIndex].Name;

        //��b���̕\��
        string content = _conversationInfoList[_contentIndex].Content;
        int len = 0;
        while (len <= content.Length)
        {
            //������\������
            _contentTMP.text = content.Substring(0, len);
            len++;

            //n�b���Ƃɕ������\������Ă���
            float timeSpan = 0.1f;
            await UniTask.Delay(TimeSpan.FromSeconds(timeSpan));
        }
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
    [CustomLabel("���̉�b�ɑJ��܂ł̕b��")]
    public float transTime;
}
