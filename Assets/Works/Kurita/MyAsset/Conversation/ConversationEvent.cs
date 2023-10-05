using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// UI���g���Ă���̂ł�����L�����܂��傤
using UnityEngine.UI;
public class ConversationEvent : MonoBehaviour
{
    // nameText:�����Ă���l�̖��O
    // talkText:�����Ă�����e��i���[�V����
    [SerializeField] private TMPro.TextMeshProUGUI nameText;
    [SerializeField] private TMPro.TextMeshProUGUI conversationText;

    [SerializeField] public bool playing = false;
    [SerializeField] private float textSpeed = 0.1f;

    void Start() { }

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
        nameText.text = "";
        StartCoroutine("CoDrawText", text);
    }
    // �ʏ��b�p�̃e�L�X�g�𐶐�����֐�
    public void DrawText(string name, string text)
    {
        nameText.text = name + "\n";
        StartCoroutine("CoDrawText", "�u" + text + "�v");
    }

    // �e�L�X�g���ꕶ�����o��
    IEnumerator CoDrawText(string text)
    {
        playing = true;
        float time = 0;
        while (true)
        {
            yield return 0;
            time += Time.deltaTime;

            // �N���b�N�����ƈ�C�ɕ\��
            if (IsClicked()) break;

            int len = Mathf.FloorToInt(time / textSpeed);
            if (len > text.Length) break;
            conversationText.text = text.Substring(0, len);
        }
        conversationText.text = text;
        yield return 0;
        playing = false;
    }
}