using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS_FadeoutChangeScene : MonoBehaviour
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

    #endregion

    #region 内部用変数

    // InputSystem
    IA_Player m_inputAction;

    // シーン読み込み状況
    AsyncOperation m_sceneLoadState;

    // シーン遷移中かどうか
    bool m_fadeRun = false;

    #endregion

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        m_inputAction = new IA_Player();
        m_inputAction.Enable();

        m_sceneLoadState = SceneManager.LoadSceneAsync(m_sceneName);
        m_sceneLoadState.allowSceneActivation = false;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        if (m_inputAction.Title.ToGameScene.triggered)
        {
            if(!m_fadeRun)
                FadeStart();
        }
    }

    /// <summary>
    /// フェード開始
    /// </summary>
    void FadeStart()
    {
        m_fadeRun = true;
        m_fade.FadeIn(m_setFadeTime);
        Invoke(nameof(ChangeScene), m_setFadeTime);
    }

    /// <summary>
    /// シーンが切り替え
    /// </summary>
    private void ChangeScene()
    {
        m_sceneLoadState.allowSceneActivation = true;
    }


}
