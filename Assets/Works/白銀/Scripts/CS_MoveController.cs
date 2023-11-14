using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_MoveController : MonoBehaviour
{
    #region �C���X�y�N�^�p�ϐ�

    // �v���C���[�̐i�ޑ��x
    [SerializeField, CustomLabel("�O�i���x")]
    float m_moveVel;

    #endregion


    #region �����p�ϐ�

    static float m_getMoveVel;

    // ���x�x�N�g��
    static Vector3 m_vecVel;

    // �q�I�u�W�F�N�g�Ǘ�
    static Dictionary<string, GameObject> m_children = new Dictionary<string, GameObject>();

    #endregion

    /// <summary>
    /// ����������
    /// </summary>
    void Start()
    {
        m_getMoveVel = m_moveVel;

        // �q�I�u�W�F�N�g�擾
        int count = gameObject.transform.childCount;

        for(int i = 0; i < count; ++i)
        {
            GameObject obj = gameObject.transform.GetChild(i).gameObject;
            m_children.Add(obj.name, obj);
        }
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    private void FixedUpdate()
    {
        transform.position = m_vecVel * CS_AudioManager.Instance.TimeBGM;
    }

    /// <summary>
    /// �ړ����x�̎擾
    /// </summary>
    /// <returns></returns>
    static public float GetMoveVel()
    {
        return m_getMoveVel;
    }

    /// <summary>
    /// �q�I�u�W�F�N�g�̎擾
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    static public GameObject GetObject(string name)
    {
        if(!m_children.ContainsKey(name))
        {
            Debug.Log(name + "�̃I�u�W�F�N�g�͑��݂��܂���");
            return null;
        }

        return m_children[name];
    }

    /// <summary>
    /// �q�I�u�W�F�N�g�̎擾
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>

    /// <summary>
    /// �ړ��X�^�[�g
    /// </summary>
    static public void MoveStart()
    {
        m_vecVel = new Vector3(0.0f, 0.0f, -m_getMoveVel);
    }
}
