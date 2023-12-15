using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CS_GameManager : MonoBehaviour
{
    #region インスペクタ用変数

    // フェード
    [SerializeField, CustomLabel("フェード")]
    Fade m_fade;

    // フェード
    [SerializeField, CustomLabel("フェード時間")]
    float m_setFadeTime;

    // HPデータ
    [SerializeField, CustomLabel("HP")]
    HP m_hp;

    // フェード
    [SerializeField, CustomLabel("ゴールのシーン名")]
    string m_nextSceneName;

    // ゲームオーバーシーン
    [SerializeField, CustomLabel("ゲームオーバーのシーン名")]
    string m_gameoverSceneName;



    #endregion


    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        CS_AudioManager.Instance.PlayAudio("GameAudio", true);

        m_fade.FadeIn(m_setFadeTime,
          () =>
          {
              CS_AudioManager.Instance.MasterVolume = 1.0f;
              CS_MoveController.MoveStart();
          });
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {
        Fade.STATE state = m_fade.GetState();

        if (state == Fade.STATE.NONE)
            StateNone();
        else if (state == Fade.STATE.IN)
            StateIn();
    }

    /// <summary>
    /// フェード状態ではないとき
    /// </summary>
    void StateNone()
    {
        // ゲームクリア時の遷移
        if (CS_AudioManager.Instance.TimeBGM >= CS_AudioManager.Instance.LengthBGM - m_setFadeTime)
        {
            m_fade.FadeOut(m_setFadeTime, 
                () => {
                    CS_AudioManager.Instance.MasterVolume = 0.0f;
                    CS_AudioManager.Instance.StopBGM();
                    SceneManager.LoadScene(m_nextSceneName);
                });
        }

        // ゲームオーバー時の遷移
        if(m_hp.Health <= 0)
        {
            m_fade.FadeOut(m_setFadeTime,
                () => {
                    CS_AudioManager.Instance.MasterVolume = 0.0f;
                    CS_AudioManager.Instance.StopBGM();
                    SceneManager.LoadScene(m_gameoverSceneName);
                });
        }
    }

    /// <summary>
    /// フェードイン(音量の変更を行う)
    /// </summary>
    void StateIn()
    {
        //Debug.Log("フェードイン!");

        float vol = 1.0f - m_fade.GetRange();
        CS_AudioManager.Instance.MasterVolume = (vol);
    }
}
