using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_EnemyAttackFromEnemy : CS_LoadNotesFile
{
    #region �C���X�y�N�^�p�ϐ�

    // ��������I�u�W�F�N�g
    [SerializeField, CustomLabel("�����I�u�W�F�N�g")]
    GameObject m_createObject;

    // �I�u�W�F�N�g�̐�����
    [SerializeField, CustomLabel("�I�u�W�F�N�g�̐�����")]
    [Range(20.0f, 60.0f)]
    int m_numMax = 20;

    // �W�����v�^�C�~���O�ɍ��킹�ăI�t�Z�b�g����������
    [SerializeField, CustomLabel("�I�t�Z�b�g")]
    [Range(0.0f, 2.0f)]
    float m_offset = 0.0f;

    #endregion

    #region �����p�ϐ�

    // �Q�[���I�u�W�F�N�g�Ǘ��p
    GameObject[] m_objects;

    // �J�����̃I�u�W�F�N�g
    [SerializeField] private GameObject m_cameraObject;

    // ���������f�[�^�̃J�E���g
    int m_createCount;

    #endregion


    /// <summary>
    /// ����������
    /// </summary>
    private void Start()
    {
        m_objects = new GameObject[m_numMax];
        

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
        for (int i = 0; i < m_numMax; ++i)
        {
            if (m_objects[i] == null)
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

        PerNoteInfo info = m_perNoteInfos[m_createCount];

        // �����|�W�V�����̎w��
        Vector3 createPos = Vector3.zero;
        createPos.x = -60.0f + info.lane * 30.0f;
        createPos.y = 2.5f;
        createPos.z = (info.time - m_offset) * CS_MoveController.GetMoveVel();

        GameObject obj = Instantiate(m_createObject, createPos, Quaternion.identity);
        obj.AddComponent<CS_EnemyAttackNotes>();
        obj.GetComponent<CS_EnemyAttackNotes>().m_perfTime = info.time;

        Destroy(obj, info.time - CS_AudioManager.Instance.TimeBGM + 0.5f);
        m_createCount++;

        m_objects[index] = obj;
    }
}
