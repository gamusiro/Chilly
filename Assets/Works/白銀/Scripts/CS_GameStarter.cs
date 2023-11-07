using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_GameStarter : MonoBehaviour
{
    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        Invoke(nameof(GameStart), 1.0f);
        //GameStart();
    }

    /// <summary>
    /// ゲームスタート
    /// </summary>
    void GameStart()
    {
        CS_AudioManager.Instance.PlayAudio("GameAudio");
        CS_MoveController.Instance.MoveStart();
        Destroy(gameObject);
    }
}
