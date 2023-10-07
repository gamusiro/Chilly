using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// UIを使っているのでこれを記入しましょう
using UnityEngine.UI;
public class ConversationEvent : MonoBehaviour
{
    // nameText:喋っている人の名前
    // talkText:喋っている内容やナレーション
    [SerializeField] private TMPro.TextMeshProUGUI _nameText;
    [SerializeField] private TMPro.TextMeshProUGUI _conversationText;

    [SerializeField] public bool _playing = false;
    [SerializeField] private float _textSpeed = 0.1f;

    // クリックで次のページを表示させるための関数
    public bool IsClicked()
    {
        if (Input.GetMouseButtonDown(0)) 
            return true;
        
        return false;
    }

    // ナレーション用のテキストを生成する関数
    public void DrawText(string text)
    {
        _nameText.text = "";
        StartCoroutine("CoDrawText", text);
    }
    // 通常会話用のテキストを生成する関数
    public void DrawText(string name, string text)
    {
        _nameText.text = name + "\n";
        StartCoroutine("CoDrawText", "「" + text + "」");
    }

    // テキストを一文字ずつ出力
    IEnumerator CoDrawText(string text)
    {
        _playing = true;
        float time = 0;
        while (true)
        {
            yield return 0;
            time += Time.deltaTime;

            // クリックされると一気に表示
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