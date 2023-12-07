using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
class TutorialData
{
    public float totalTime;

    [Tooltip("totalTime / ループカウント / 枚数 ※割り切れる値にしないと変なところでループが終わるｗｗｗ")]
    public int loopCount;
    public List<Material> materials;
}

public class CS_TutorialAnimation : MonoBehaviour
{
    #region インスペクタ用変数

    [SerializeField, CustomLabel("チュートリアル開始時間")]
    float m_startTime;

    [SerializeField]
    List<TutorialData> m_data;

    #endregion

    #region 内部用変数

    // 使用中のチュートリアルタイプ(左移動とか、右移動とかを示すインデクス)
    int m_typeIndex;

    // 使用中のアニメーションのコマを示す
    int m_pageIndex;

    // それぞれの切り替え時間
    float m_changeTypeTime;
    float m_changePageTime;

    // それぞれの経過時間
    float m_curTypeTime;
    float m_curPageTime;

    // レンダラー
    Renderer m_renderer;

    // サインボード
    CS_Signboard m_signboard;

    #endregion


    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        m_typeIndex = 0;
        m_pageIndex = 0;
        m_signboard = GetComponent<CS_Signboard>();

        // 何もない場合はここで破棄
        if (m_data.Count == 0)
        {
            Destroy(gameObject);
            return;
        }
            

        // データを設定する
        TutorialData data = m_data[m_typeIndex];
        m_changeTypeTime = data.totalTime;
        m_changePageTime = (m_changeTypeTime  / data.loopCount) / data.materials.Count;

        m_curTypeTime = 0.0f;
        m_curPageTime = 0.0f;

        m_renderer = transform.Find("Screen").gameObject.GetComponent<Renderer>();

        // 初期マテリアルの貼り付け
        ChangeScreen();
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        if (!m_signboard.StartTutorial())
            return;

        // 変更があった場合飲みマテリアルを切り替える
        bool change = false;

        // 経過時間の更新
        m_curTypeTime += Time.deltaTime;
        m_curPageTime += Time.deltaTime;

        // 違うチュートリアルに切り替える
        if(m_changeTypeTime <= m_curTypeTime)
        {
            m_curTypeTime = 0.0f;
            m_curPageTime = 0.0f;
            m_pageIndex = 0;
            m_typeIndex++;

            // インデックスが大きい場合は終了
            if (m_typeIndex >= m_data.Count)
            {
                m_signboard.SetStateShrink();
                return;
            }
                
            // 次のデータに設定する
            TutorialData data = m_data[m_typeIndex];
            m_changeTypeTime = data.totalTime;
            m_changePageTime = (m_changeTypeTime  / data.loopCount) / data.materials.Count;

            // 変更があった
            change = true;
        }

        // 次の画像に進める
        if(m_changePageTime <= m_curPageTime)
        {
            m_curPageTime = 0.0f;
            m_pageIndex++;

            // 変更があった
            change = true;
        }

        // 画像の切り替えを実行する
        if(change)
            ChangeScreen();
    }

    /// <summary>
    /// 切り替え
    /// </summary>
    void ChangeScreen()
    {
        if (m_typeIndex >= m_data.Count)
            return;

        int cnt = m_data[m_typeIndex].materials.Count;
        m_renderer.material = m_data[m_typeIndex].materials[m_pageIndex % cnt];
    }
}
