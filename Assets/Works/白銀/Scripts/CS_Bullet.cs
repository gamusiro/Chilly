using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Bullet : MonoBehaviour
{
    [SerializeField, CustomLabel("’e‚Ì‘¬“x")]
    float m_bulletSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * m_bulletSpeed;
    }
}
