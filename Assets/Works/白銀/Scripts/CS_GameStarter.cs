using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_GameStarter : MonoBehaviour
{
    #region インスペクタ用変数

    // フェード
    [SerializeField, CustomLabel("フェード")]
    Fade m_fade;

    // フェード
    [SerializeField, CustomLabel("フェード時間")]
    float m_setFadeTime;

    #endregion

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        m_fade.FadeOut(m_setFadeTime);
        Invoke(nameof(GameStart), m_setFadeTime);
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
