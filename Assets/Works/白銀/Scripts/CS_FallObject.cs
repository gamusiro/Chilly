using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_FallObject : MonoBehaviour
{
    /// <summary>
    /// ����������
    /// </summary>
    void Start()
    {
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
        if(collision.gameObject.tag == "Player"
            || collision.gameObject.tag == "Field")
        {
            CS_AudioManager.Instance.PlayAudio("Clap");
        }
    }
}
