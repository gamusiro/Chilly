using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextWriter : MonoBehaviour
{
    public ConversationEvent _conversationEvent;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Cotest");
    }

    // �N���b�N�҂��̃R���[�`��
    IEnumerator Skip()
    {
        while (_conversationEvent._playing) yield return 0;
        while (!_conversationEvent.IsClicked()) yield return 0;
    }

    // ���͂�\��������R���[�`��
    IEnumerator Cotest()
    {
        _conversationEvent.DrawText("�Ă�", "���ۂ̍ۍۂŗx��܂��傤�I");
        yield return StartCoroutine("Skip");

        _conversationEvent.DrawText("U�@R�@MY�@SPECIAL");
        yield return StartCoroutine("Skip");
    }
}
