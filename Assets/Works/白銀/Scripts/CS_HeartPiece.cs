using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CS_HeartPiece : MonoBehaviour
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
    private void FixedUpdate()
    {
        
    }

    /// <summary>
    /// Õ“Ë‚µ‚½‚ç
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Õ“Ë‚µ‚½
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
