using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Scattering : MonoBehaviour
{
    #region �����p�ϐ�

    // ���W�b�h�{�f�B
    Rigidbody m_rigidbody;

    #endregion

    /// <summary>
    /// ����������
    /// </summary>
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
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
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log(collision.gameObject.GetComponent<Rigidbody>().velocity);

            if (collision.gameObject.GetComponent<CS_Player>().IsDashing)
            {

                // �����x�N�g�������߂�
                Vector3 dir = collision.gameObject.transform.position - transform.position;
                dir.y += 10.0f;
                dir = Vector3.Normalize(dir);
                dir.x *= -1.0f;

                // �L�l�}�e�B�b�N
                m_rigidbody.isKinematic = false;

                // ������΂�
                m_rigidbody.AddForce(dir * 1000.0f, ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector3 dir = other.gameObject.GetComponent<Rigidbody>().velocity;
            Debug.Log(dir);

            // �L�l�}�e�B�b�N
            m_rigidbody.isKinematic = false;

            // ������΂�
            m_rigidbody.AddForce(dir * 100000.0f, ForceMode.Impulse);
        }
    }
}
