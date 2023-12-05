using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_FallObject : MonoBehaviour
{
    public float m_perfTime;

    /// <summary>
    /// ‰Šú‰»ˆ—
    /// </summary>
    void Start()
    {
    }

    /// <summary>
    /// XVˆ—
    /// </summary>
    void Update()
    {
        
    }

    /// <summary>
    /// Õ“Ë‚µ‚½‚ç
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player"
            || collision.gameObject.tag == "Field")
        {
            CS_AudioManager.Instance.PlayAudioMemoryTime("Fall", m_perfTime);
        }
    }
}
