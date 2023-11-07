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
    [Range(5.0f, 20.0f)]
    int m_numMax = 5;

    // �W�����v�^�C�~���O�ɍ��킹�ăI�t�Z�b�g����������
    [SerializeField, CustomLabel("�I�t�Z�b�g")]
    [Range(0.0f, 2.0f)]
    float m_offset = 0.0f;

    #endregion

    #region �����p�ϐ�

    // �Q�[���I�u�W�F�N�g�Ǘ��p
    List<GameObject> m_gameObjects;

    // �J�����̃I�u�W�F�N�g
    GameObject m_cameraObject;

    // �j�������f�[�^�̃J�E���g
    int m_numDestroy;

    #endregion


    /// <summary>
    /// ����������
    /// </summary>
    private void Start()
    {
        m_gameObjects = new List<GameObject>();
        m_numDestroy = 0;

        // �ǂݍ��ݏ���
        this.Load();

        m_cameraObject = CS_MoveController.Instance.GetVirtualCamera("Front");

        // �e�X�g�p�I�u�W�F�N�g����
        for (int i = 0; i < m_numMax; ++i)
            CreateGameObject(i);
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    void FixedUpdate()
    {
        // ��Ɨp�ϐ�
        int numDestroy = m_numDestroy;

        for (int i = numDestroy; i < numDestroy + m_numMax; ++i)
        {
            // �m�[�c�J�E���g���f�[�^���������
            if (i >= m_perNoteInfos.Count)
                break;                          // �T���I��

            if (m_gameObjects[i] == null)
                continue;

            // �Q�[���I�u�W�F�N�g�̃|�W�V���������[�u�I�u�W�F�N�g�̍��W���傫���Ȃ�����
            if (m_gameObjects[i].transform.position.z < m_cameraObject.gameObject.transform.position.z)
            {
                // �Ώۂ̃I�u�W�F�N�g��j������
                Destroy(m_gameObjects[i]);

                // �j�����𑝂₷
                m_numDestroy++;

                // ���̃I�u�W�F�N�g�̐���
                if (m_gameObjects.Count < m_perNoteInfos.Count)
                    CreateGameObject(m_gameObjects.Count);
            }

        }
    }

    /// <summary>
    /// �m�[�c�I�u�W�F�N�g�̐���
    /// </summary>
    /// <param name="index"></param>
    void CreateGameObject(int index)
    {
        Vector3 createPos = Vector3.zero;
        createPos.x = -60.0f + m_perNoteInfos[index].lane * 30.0f;
        createPos.y = 2.5f;
        createPos.z = (m_perNoteInfos[index].time - m_offset) * CS_MoveController.Instance.GetMoveVel();

        GameObject obj = Instantiate(m_createObject, createPos, Quaternion.identity);
        obj.AddComponent<CS_EnemyAttackNotes>();
        obj.GetComponent<CS_EnemyAttackNotes>().m_perfTime = m_perNoteInfos[index].time;

        m_gameObjects.Add(obj);
    }
}
