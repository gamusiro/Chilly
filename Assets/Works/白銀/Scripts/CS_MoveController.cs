using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_MoveController : CS_SingletonMonoBehaviour<CS_MoveController>
{
    #region �C���X�y�N�^�p�ϐ�

    // �v���C���[�̐i�ޑ��x
    [SerializeField, CustomLabel("�O�i���x")]
    float m_moveVel;

    #endregion


    #region �����p�ϐ�

    // ���x�x�N�g��
    Vector3 m_vecVel;

    // �q�I�u�W�F�N�g�Ǘ�
    Dictionary<string, GameObject> m_children = new Dictionary<string, GameObject>();
    
    // ���z�J�����I�u�W�F�N�g
    Dictionary<string, GameObject> m_cameraList = new Dictionary<string, GameObject>();

    #endregion

    /// <summary>
    /// ����������
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        m_vecVel = Vector3.zero;

        // �q�I�u�W�F�N�g�擾
        int count = gameObject.transform.childCount;

        for(int i = 0; i < count; ++i)
        {
            GameObject obj = gameObject.transform.GetChild(i).gameObject;
            m_children.Add(obj.name, obj);
        }

        // �J�������牼�z�J�����̃Q�[���I�u�W�F�N�g���擾����
        foreach (CinemachineVirtualCamera cam in m_children["Camera"].GetComponentsInChildren<CinemachineVirtualCamera>())
        {
            // �������g�̏ꍇ�͏������X�L�b�v����
            if (cam.gameObject == gameObject) 
                continue;

            m_cameraList.Add(cam.gameObject.name, cam.gameObject);
        }
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    private void FixedUpdate()
    {
        transform.position = m_vecVel * CS_AudioManager.Instance.GetAudioSource("GameAudio").time;
    }

    /// <summary>
    /// �ړ����x�̎擾
    /// </summary>
    /// <returns></returns>
    public float GetMoveVel()
    {
        return m_moveVel;
    }

    /// <summary>
    /// �q�I�u�W�F�N�g�̎擾
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject GetObject(string name)
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
    public GameObject GetVirtualCamera(string name)
    {
        if (!m_cameraList.ContainsKey(name))
        {
            Debug.Log(name + "�̃I�u�W�F�N�g�͑��݂��܂���");
            return null;
        }

        return m_cameraList[name];
    }

    /// <summary>
    /// �ړ��X�^�[�g
    /// </summary>
    public void MoveStart()
    {
        m_vecVel = new Vector3(0.0f, 0.0f, -m_moveVel);
    }
}
