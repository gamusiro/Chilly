using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_FallObject : MonoBehaviour
{
    AudioSource m_clapSound;

    /// <summary>
    /// ‰Šú‰»ˆ—
    /// </summary>
    void Start()
    {
        m_clapSound = GetComponent<AudioSource>();
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
        m_clapSound.Play();
    }
}
