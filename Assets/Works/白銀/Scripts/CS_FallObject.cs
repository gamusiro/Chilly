using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_FallObject : MonoBehaviour
{
    public float m_perfTime;

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
            //Debug.Log(m_perfTime + "�@:�@" + CS_AudioManager.Instance.TimeBGM);
            CS_AudioManager.Instance.PlayAudioMemoryTime("Fall", m_perfTime);
        }
    }
}
