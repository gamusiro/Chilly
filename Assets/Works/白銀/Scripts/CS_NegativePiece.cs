using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class CS_NegativePiece : MonoBehaviour
{
    GameObject m_player;
    Rigidbody m_rigidBody;
    bool m_clap;

    /// <summary>
    /// ‰Šú‰»ˆ—
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
    /// XVˆ—
    /// </summary>
    void Update()
    {
        if(m_clap)
        {
            if(m_rigidBody.velocity.z >= 0.0)
            {
                if (transform.position.z >= m_player.transform.position.z)
                {
                    gameObject.GetComponent<AudioSource>().Play();
                    m_clap = false;
                }
            }
            else
            {
                if (transform.position.z <= m_player.transform.position.z)
                {
                    gameObject.GetComponent<AudioSource>().Play();
                    m_clap = false;
                }
            }
        }
    }

    /// <summary>
    /// ‘¬“x‚Ìİ’è
    /// </summary>
    public void SetVelocity(Vector3 vel, float destroyTime)
    {
        m_rigidBody.velocity = vel;
        m_clap = true;
        Destroy(gameObject, destroyTime);
    }
}
