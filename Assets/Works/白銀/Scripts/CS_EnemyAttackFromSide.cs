using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_EnemyAttackFromSide : CS_LoadNotesFile
{
    #region �C���X�y�N�^�p�ϐ�

    // �܂Ƃ߂�p�̐e�I�u�W�F�N�g
    [SerializeField, CustomLabel("�܂Ƃ߂�p�̐e�I�u�W�F�N�g")]
    Transform m_parent;

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
        GameObject obj = Instantiate(m_createObject);
        obj.transform.parent = gameObject.transform.parent;
        obj.AddComponent<CS_EnemyAttackSideBar>();
        obj.transform.parent = m_parent;

        CS_EnemyAttackSideBar bar = obj.GetComponent<CS_EnemyAttackSideBar>();
        bar.m_perfTime = info.time;

        // ����Ă�����������߂�
        bool right = false;
        if(info.type == 1)
            right = true;

        bar.SetLane(info.lane, m_offset, right);

        Destroy(obj, info.time - CS_AudioManager.Instance.TimeBGM + 1.0f);

        m_createCount++;

        m_objects[index] = obj;
    }
}
