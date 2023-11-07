using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_FallObject : MonoBehaviour
{
    AudioSource m_clapSound;

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        m_clapSound = GetComponent<AudioSource>();
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
        m_clapSound.Play();
    }
}
