using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_EnemySample : MonoBehaviour
{
    [SerializeField, CustomLabel("弾丸")]
    GameObject m_bulletObject;


    CS_NotesManager m_manager;


    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        m_manager = GetComponent<CS_NotesManager>();

        // 時間と攻撃タイプ等を選択して、指定秒数後に実行する処理を登録しておく
        for(int i = 0; i < m_manager.m_noteNum; ++i)
        {
            StartCoroutine(Control(i));
        }
    }

    /// <summary>
    /// コルーチン関数の定義
    /// </summary>
    /// <param name="i">ノーツ情報番号</param>
    /// <returns></returns>
    private IEnumerator Control(int i)
    {

        yield return new WaitForSeconds(m_manager.m_notesTime[i]);

        // ノーツのタイプによって攻撃方法を変える
        switch(m_manager.m_noteType[i])
        {
            default:
                Shoot(m_manager.m_laneNum[i]);
                break;
        }
    }

    /// <summary>
    /// 弾丸を発射
    /// </summary>
    /// <param name="laneNum"></param>
    void Shoot(int laneNum)
    {
        // オブジェクトの生成ポジションの作成
        Vector3 createPos = gameObject.transform.position;
        createPos.x += (m_bulletObject.transform.localScale.x * 2.0f) * (laneNum - 1);

        // オブジェクト作成
        GameObject obj = Instantiate(m_bulletObject, createPos, Quaternion.identity);

        // 5秒後に破棄
        Destroy(obj, 5.0f);
    }
}
