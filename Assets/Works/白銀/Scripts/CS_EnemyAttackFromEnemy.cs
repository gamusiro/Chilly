using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_EnemyAttackFromEnemy : CS_LoadNotesFile
{
    #region �C���X�y�N�^�p�ϐ�

    // ��������I�u�W�F�N�g
    [SerializeField, CustomLabel("�����I�u�W�F�N�g")]
    GameObject m_createObject;

    // ��������I�u�W�F�N�g
    [SerializeField, CustomLabel("�W�����v���C���I�u�W�F�N�g")]
    GameObject m_jumpLineObject;

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

    // ���������f�[�^�̃J�E���g
    int m_createCount;

    // �Q�Ƃ���m�[�c�f�[�^�̃C���f�b�N�X���Ǘ�
    int m_refInfoIndex;

    #endregion


    /// <summary>
    /// ����������
    /// </summary>
    private void Start()
    {
        m_objects = new GameObject[m_numMax];
        m_refInfoIndex = 0;

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

        if (m_perNoteInfos[m_refInfoIndex].time <= CS_AudioManager.Instance.TimeBGM)
        {
            m_refInfoIndex++;
            m_refInfoIndex = Mathf.Min(m_refInfoIndex, m_perNoteInfos.Count - 1);
        }

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
        createPos.x = 0.0f;
        createPos.y = 2.5f;
        createPos.z = (info.time - m_offset) * CS_MoveController.GetMoveVel();

        GameObject obj = Instantiate(m_createObject, createPos, Quaternion.identity);
        obj.AddComponent<CS_EnemyAttackNotes>();
        obj.GetComponent<CS_EnemyAttackNotes>().m_perfTime = info.time;

        // �W�����v�^�C�~���O�p�̐�
        createPos.x = 0.0f;
        createPos.y = 2.41f;
        createPos.z = info.time * CS_MoveController.GetMoveVel() * -1.0f;
        GameObject lin = Instantiate(m_jumpLineObject, createPos, Quaternion.identity);

        Destroy(obj, info.time - CS_AudioManager.Instance.TimeBGM + 0.5f);
        Destroy(lin, info.time - CS_AudioManager.Instance.TimeBGM + 0.5f);
        m_createCount++;

        m_objects[index] = obj;
    }

    /// <summary>
    /// �f�[�^�Z�b�g
    /// </summary>
    /// <returns></returns>
    public float GetPerfectTime()
    {
        return m_perNoteInfos[m_refInfoIndex].time;
    }
}
