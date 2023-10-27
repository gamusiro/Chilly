using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class CS_NegativePiece : MonoBehaviour
{
    Vector3 m_velocity;
    GameObject m_player;
    bool m_clap;

    private void Start()
    {
        m_player = CS_MoveController.Instance.GetPlayer();
    }

    /// <summary>
    /// çXêVèàóù
    /// </summary>
    void Update()
    {
        transform.position += m_velocity * Time.deltaTime;

        if(m_clap)
        {
            if(m_velocity.z >= 0.0)
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
    /// ë¨ìxÇÃê›íË
    /// </summary>
    public void SetVelocity(Vector3 vel, float destroyTime)
    {
        m_velocity = vel;
        m_clap = true;
        Destroy(gameObject, destroyTime);
    }
}
