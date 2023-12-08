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

    // フェード
    [SerializeField, CustomLabel("次のシーン名")]
    string m_nextSceneName;

    #endregion

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        //ラストシーンと合わせたい
        //そのためにはGameを削除する必要あり
        //コルーチンを途中で止めてしまうためバグが起きる
        //Debug.Log("エラー箇所");

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
        if (CS_AudioManager.Instance.TimeBGM >= CS_AudioManager.Instance.LengthBGM - m_setFadeTime)
        {
            m_fade.FadeOut(m_setFadeTime, 
                () => {
                    CS_AudioManager.Instance.MasterVolume = 0.0f;
                    CS_AudioManager.Instance.StopBGM();
                    SceneManager.LoadScene(m_nextSceneName);
                });
        }
    }

    /// <summary>
    /// フェードイン(音量の変更を行う)
    /// </summary>
    void StateIn()
    {
        float vol = 1.0f - m_fade.GetRange();
        CS_AudioManager.Instance.MasterVolume = (vol);
    }
}
