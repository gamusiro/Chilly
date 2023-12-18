using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    //// ファーストテイクパネルUI
    //[SerializeField, CustomLabel("FirstTake")]
    //GameObject m_firstTakePanel;

    //// メニューテイクパネルUI
    //[SerializeField, CustomLabel("MenuTake")]
    //GameObject m_menuTakePanel;

    #endregion

    #region 内部用変数

    // InputSystem
    IA_Player m_inputAction;


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

        m_inputAction = new IA_Player();
        m_inputAction.Enable();

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
        else if (state == Fade.STATE.IN)
            StateIn();
        //else
        //    StateOut();
    }

    /// <summary>
    /// フェード状態ではないとき
    /// </summary>
    void StateNone()
    {
        // 決定ボタンが押されたら
        if (m_inputAction.Title.ToGameScene.triggered)
        {
            CS_AudioManager.Instance.PlayAudio("Commit");
            
            switch(m_take)
            {
                case TAKE.FIRST:
                    // メニュ―テイクへの切り替えを行う
                    FirstTakeUpdate();
                    break;
                case TAKE.MENU:
                    // ゲームの遷移を管理
                    //m_menuTakePanel.SetActive(true);
                    MenuTakeUpdate();
                    break;
            }
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

    /// <summary>
    /// フェードアウト
    /// </summary>
    void StateOut()
    {
        float vol = 1.0f - m_fade.GetRange();
        CS_AudioManager.Instance.MasterVolume = (vol);
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

    /// <summary>
    /// 初期処理
    /// </summary>
    void FirstTakeUpdate()
    {
        //m_take = TAKE.MENU;
        //m_firstTakePanel.SetActive(false);
        //m_menuTakePanel.SetActive(true);

        // 今はゲームシーンに直移動
        NextScene();
    }

    /// <summary>
    /// メニュー処理
    /// </summary>
    void MenuTakeUpdate()
    {

    }
}
