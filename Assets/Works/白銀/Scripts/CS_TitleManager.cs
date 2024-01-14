using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CS_TitleManager : MonoBehaviour
{
    #region インスペクタ用変数

    // フェード
    [SerializeField, CustomLabel("フェード")]
    Fade m_fade;

    // フェード
    [SerializeField, CustomLabel("フェード時間")]
    float m_setFadeTime;

    // 読み込むシーンの名前
    [SerializeField, CustomLabel("シーン名")]
    string m_sceneName;

    [SerializeField, CustomLabel("カメラマネージャ")]
    TitleCameraPhaseManager m_manager;

    //// ファーストテイクパネルUI
    //[SerializeField, CustomLabel("FirstTake")]
    //GameObject m_firstTakePanel;

    //// メニューテイクパネルUI
    //[SerializeField, CustomLabel("MenuTake")]
    //GameObject m_menuTakePanel;

    #endregion

    #region 内部用変数

    // InputSystem
    PlayerInput m_inputAction;


    enum TAKE
    {
        FIRST,
        MENU
    };

    TAKE m_take;

    #endregion

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        Cursor.visible = false;
        CS_AudioManager.Instance.PlayAudio("TitleAudio", true);

        m_inputAction = GetComponent<PlayerInput>();

        // フェードインの後、音量をマックスにする
        m_fade.FadeIn(1.0f, () => { CS_AudioManager.Instance.MasterVolume = 1.0f; });
    
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        Fade.STATE state = m_fade.GetState();

        if (state == Fade.STATE.NONE)
            StateNone();
        else
        {
            float vol = 1.0f - m_fade.GetRange();
            CS_AudioManager.Instance.MasterVolume = (vol);
        }
    }

    /// <summary>
    /// フェード状態ではないとき
    /// </summary>
    void StateNone()
    {
        // 決定ボタンが押されたら
        if (m_inputAction.currentActionMap["Commit"].triggered)
        {
            CS_AudioManager.Instance.PlayAudio("Commit");
            m_manager.NextCamera();
        } 
    }

    /// <summary>
    /// 次のシーンへ移動する処理
    /// </summary>
    void NextScene()
    {
        m_fade.FadeOut(m_setFadeTime, () =>
        {
            CS_AudioManager.Instance.MasterVolume = 0.0f;
            CS_AudioManager.Instance.StopBGM();
            SceneManager.LoadScene(m_sceneName);
        });
    }
}
