using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Scattering : MonoBehaviour
{
    #region 内部用変数

    // リジッドボディ
    Rigidbody m_rigidbody;

    #endregion

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        
    }

    /// <summary>
    /// 衝突したら
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log(collision.gameObject.GetComponent<Rigidbody>().velocity);

            if (collision.gameObject.GetComponent<CS_Player>().IsDashing)
            {

                // 方向ベクトルを求める
                Vector3 dir = collision.gameObject.transform.position - transform.position;
                dir.y += 10.0f;
                dir = Vector3.Normalize(dir);
                dir.x *= -1.0f;

                // キネマティック
                m_rigidbody.isKinematic = false;

                // 吹き飛ばす
                m_rigidbody.AddForce(dir * 1000.0f, ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector3 dir = other.gameObject.GetComponent<Rigidbody>().velocity;
            Debug.Log(dir);

            // キネマティック
            m_rigidbody.isKinematic = false;

            // 吹き飛ばす
            m_rigidbody.AddForce(dir * 100000.0f, ForceMode.Impulse);
        }
    }
}
