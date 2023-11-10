using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
class SignBoardData
{
    public Material     m_material;
    public Vector2      m_displayTime;
}


public class CS_Signboard : MonoBehaviour
{
    [SerializeField, CustomLabel("チュートリアル看板データ")]
    List<SignBoardData> m_boardDatasList;

    int m_usedCount;

    float m_p = 0.0f;
    bool m_destroyed;

    Vector3 m_originLocalPos;
    Vector3 m_subScale;

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        m_usedCount = 0;

        if(m_boardDatasList.Count == 0)
            Destroy(gameObject);

        m_originLocalPos = transform.localPosition;

        m_destroyed = false;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        if (m_usedCount < m_boardDatasList.Count)
        {
            if (m_boardDatasList[m_usedCount].m_displayTime.x <= CS_AudioManager.Instance.GetAudioSource("GameAudio").time)
            {
                // 指定した時間になったらテクスチャの張替
                Renderer renderer = gameObject.GetComponent<Renderer>();

                renderer.enabled = true;
                renderer.material = m_boardDatasList[m_usedCount].m_material;
            }

            if (m_boardDatasList[m_usedCount].m_displayTime.y <= CS_AudioManager.Instance.GetAudioSource("GameAudio").time)
            {
                m_usedCount++;
            }
        }
        else
        {
            Destroy(gameObject, 10.0f);
            m_destroyed = true;

            m_subScale = transform.localScale / 10.0f;
        }
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void FixedUpdate()
    {
        if(m_destroyed)
        {
            transform.localScale -= m_subScale;
        }
        else
        {
            m_p += Time.deltaTime * 2.0f;

            Vector3 addPos = Vector3.zero;
            addPos.y = Mathf.Cos(m_p) * 10.0f;

            transform.localPosition = addPos + m_originLocalPos;
        }
    }
}
