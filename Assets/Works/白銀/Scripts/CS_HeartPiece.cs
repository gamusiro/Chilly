using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CS_HeartPiece : MonoBehaviour
{
    GameObject m_enemyObject;
    GameObject m_cameraObject;

    Vector3 m_rootRight;
    Vector3 m_rootLeft;
    Vector3 m_useRoot;

    bool m_getted;
    float m_work;

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        m_work = 0.0f;

        m_rootRight = new Vector3(200.0f, 100.0f, 0.0f);
        m_rootLeft = new Vector3(-200.0f, 100.0f, 0.0f);
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void FixedUpdate()
    {
        if (m_getted)
        {
            m_work += 0.01f;

            Vector3 offset = new Vector3(0.0f, 0.0f, 10.0f);    // エネミーの後ろまで行くようにしたいので

            Vector3 a = Vector3.Lerp(gameObject.transform.localPosition, m_cameraObject.transform.localPosition, m_work);
            Vector3 b = Vector3.Lerp(a, m_useRoot, m_work);
            Vector3 c = Vector3.Lerp(b, m_enemyObject.transform.localPosition + offset, m_work);

            gameObject.transform.localPosition = c;
        }
    }

    /// <summary>
    /// 衝突したら
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameObject.transform.parent = other.gameObject.transform.parent;

            // 経由するルートを確定する
            if (gameObject.transform.position.x > 0)
                m_useRoot = m_rootRight;
            else
                m_useRoot = m_rootLeft;

            m_getted = true;
            gameObject.transform.localPosition = other.gameObject.transform.localPosition;

            m_enemyObject = CS_MoveController.GetObject("Enemy");
            m_cameraObject = CS_MoveController.GetVirtualCamera("Front");
        }
    }

    /// <summary>
    /// 衝突した
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.transform.parent = collision.gameObject.transform.parent;

            // 経由するルートを確定する
            if (gameObject.transform.position.x > 0)
                m_useRoot = m_rootRight;
            else
                m_useRoot = m_rootLeft;

            m_getted = true;
            gameObject.transform.localPosition = collision.gameObject.transform.localPosition;

            m_enemyObject = CS_MoveController.GetObject("Enemy");
            m_cameraObject = CS_MoveController.GetVirtualCamera("Front");
        }
    }
}
