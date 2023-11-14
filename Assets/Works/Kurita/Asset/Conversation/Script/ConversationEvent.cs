using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// UI���g���Ă���̂ł�����L�����܂��傤
using UnityEngine.UI;
public class ConversationEvent : MonoBehaviour
{
    // nameText:�����Ă���l�̖��O
    // talkText:�����Ă�����e��i���[�V����
    [SerializeField] private TMPro.TextMeshProUGUI _nameText;
    [SerializeField] private TMPro.TextMeshProUGUI _conversationText;

    [SerializeField] public bool _playing = false;
    [SerializeField] private float _textSpeed = 0.1f;

    // �N���b�N�Ŏ��̃y�[�W��\�������邽�߂̊֐�
    public bool IsClicked()
    {
        if (Input.GetMouseButtonDown(0)) 
            return true;
        
        return false;
    }

    // �i���[�V�����p�̃e�L�X�g�𐶐�����֐�
    public void DrawText(string text)
    {
        _nameText.text = "";
        StartCoroutine("CoDrawText", text);
    }
    // �ʏ��b�p�̃e�L�X�g�𐶐�����֐�
    public void DrawText(string name, string text)
    {
        _nameText.text = name + "\n";
        StartCoroutine("CoDrawText", "�u" + text + "�v");
    }

    // �e�L�X�g���ꕶ�����o��
    IEnumerator CoDrawText(string text)
    {
        _playing = true;
        float time = 0;
        while (true)
        {
            yield return 0;
            time += Time.deltaTime;

            // �N���b�N�����ƈ�C�ɕ\��
            if (IsClicked()) break;

            int len = Mathf.FloorToInt(time / _textSpeed);
            if (len > text.Length) break;
            _conversationText.text = text.Substring(0, len);
        }
        _conversationText.text = text;
        yield return 0;
        _playing = false;
    }
}