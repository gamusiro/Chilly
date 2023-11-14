using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_FallObject : MonoBehaviour
{
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
        CS_AudioManager.Instance.PlayAudio("Clap");
    }
}
