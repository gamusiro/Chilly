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

    // クリック待ちのコルーチン
    IEnumerator Skip()
    {
        while (_conversationEvent._playing) yield return 0;
        while (!_conversationEvent.IsClicked()) yield return 0;
    }

    // 文章を表示させるコルーチン
    IEnumerator Cotest()
    {
        _conversationEvent.DrawText("てき", "今際の際際で踊りましょう！");
        yield return StartCoroutine("Skip");

        _conversationEvent.DrawText("U　R　MY　SPECIAL");
        yield return StartCoroutine("Skip");
    }
}
