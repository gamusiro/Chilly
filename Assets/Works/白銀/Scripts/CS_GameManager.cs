using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_GameManager : MonoBehaviour
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
        m_fade.FadeIn(m_setFadeTime, 
            () => { 
                CS_AudioManager.Instance.MasterVolume = 1.0f;
                CS_MoveController.MoveStart();
            });

        CS_AudioManager.Instance.PlayAudio("GameAudio", true);
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void FixedUpdate()
    {
    }
}
