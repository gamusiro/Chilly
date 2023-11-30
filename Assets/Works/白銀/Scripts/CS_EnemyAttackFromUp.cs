using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_EnemyAttackFromUp : CS_LoadNotesFile
{
    #region �C���X�y�N�^�p�ϐ�

    // ��������I�u�W�F�N�g
    [SerializeField, CustomLabel("�����I�u�W�F�N�g")]
    GameObject m_createObject;

    // �e�I�u�W�F�N�g
    [SerializeField, CustomLabel("�e�I�u�W�F�N�g")]
    GameObject m_shadowObject;

    // �I�u�W�F�N�g�̐�����
    [SerializeField, CustomLabel("�I�u�W�F�N�g�̐�����")]
    [Range(20.0f, 100.0f)]
    int m_numMax = 20;

    // �n�ʂɗ����Ă���A�j������܂ł̕b���w��
    [SerializeField, CustomLabel("�Փˌォ��j���܂ł̎���")]
    [Range(0.0f, 5.0f)]
    float m_graceTime = 0.0f;

    #endregion

    #region �����p�ϐ�

    // �Q�[���I�u�W�F�N�g�Ǘ��p
    GameObject[] m_fallObjects;
    GameObject[] m_shadowObjects;

    int m_createCount;

    #endregion


    /// <summary>
    /// ����������
    /// </summary>
    private void Start()
    {
        m_fallObjects = new GameObject[m_numMax];
        m_shadowObjects = new GameObject[m_numMax];

        m_createCount = 0;

        // �ǂݍ��ݏ���
        this.Load();
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    void FixedUpdate()
    {
        if (!CS_MoveController.IsMoving())
            return;

        // �S�Ă����[�v����
        for(int i = 0; i < m_numMax; ++i)
        {
            if (m_fallObjects[i] == null)
                CreateGameObject(i);
        }
    }

    /// <summary>
    /// �m�[�c�I�u�W�F�N�g�̐���
    /// </summary>
    /// <param name="index"></param>
    void CreateGameObject(int index)
    {
        if (m_createCount >= m_perNoteInfos.Count)
            return;

        // ������̂����炾�����疳������
        PerNoteInfo info = m_perNoteInfos[m_createCount];

        // ����������W���o��
        Vector3 createPos = Vector3.zero;
        float time = info.time - CS_AudioManager.Instance.TimeBGM;

        createPos.x = -60.0f + info.lane * 30.0f;
        createPos.y = (9.81f / 2.0f) * Mathf.Pow(time, 2.0f);    // ���������Ȃ����̂� offset 2.4f
        createPos.z = info.time * CS_MoveController.GetMoveVel() * -1.0f;

        // ������I�u�W�F�N�g�̕ϐ��𐶐�
        GameObject fall = Instantiate(m_createObject, createPos, Quaternion.identity);
        
        // �e�I�u�W�F�N�g�̉e�𐶐�
        createPos.y = 0.01f;
        GameObject sdw = Instantiate(m_shadowObject, createPos, Quaternion.identity);
        sdw.GetComponent<CS_Shadow>().SetPerfectTime(info.time);

        m_fallObjects[index] = fall;
        m_shadowObjects[index] = sdw;

        Destroy(fall, time + m_graceTime);
        Destroy(sdw, time + m_graceTime);

        m_createCount++;
    }
}
