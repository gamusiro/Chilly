using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_FallObject : MonoBehaviour
{
    AudioSource m_clapSound;

    /// <summary>
    /// ����������
    /// </summary>
    void Start()
    {
        m_clapSound = GetComponent<AudioSource>();
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    void Update()
    {
        
    }

    /// <summary>
    /// �Փ˂�����
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        m_clapSound.Play();
    }
}
