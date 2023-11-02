using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class CS_NegativePiece : MonoBehaviour
{
    GameObject m_player;
    Rigidbody m_rigidBody;
    bool m_clap;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        
    }

    private void Awake()
    {
        m_player = CS_MoveController.Instance.GetPlayer();
        m_rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        //if(m_clap)
        //{
        //    if(m_rigidBody.velocity.z >= 0.0)
        //    {
        //        if (transform.position.z >= m_player.transform.position.z)
        //        {
        //            gameObject.GetComponent<AudioSource>().Play();
        //            m_clap = false;
        //        }
        //    }
        //    else
        //    {
        //        if (transform.position.z <= m_player.transform.position.z)
        //        {
        //            gameObject.GetComponent<AudioSource>().Play();
        //            m_clap = false;
        //        }
        //    }
        //}
    }

    /// <summary>
    /// 速度の設定
    /// </summary>
    public void SetVelocity(Vector3 vel, float destroyTime)
    {
        m_rigidBody.velocity = vel;
        m_clap = true;
        Destroy(gameObject, destroyTime);
    }
}
