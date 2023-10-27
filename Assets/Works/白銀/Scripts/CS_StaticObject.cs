using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_StaticObject : CS_LoadNotesFile
{
    [SerializeField, CustomLabel("��������G�I�u�W�F�N�g")]
    GameObject m_negativePieceObject;

    [SerializeField, CustomLabel("��ɂ���I�[�f�B�I�f�[�^")]
    AudioSource m_audioSource;

    [SerializeField, CustomLabel("�I�u�W�F�N�g�̐�����")]
    [Range(5, 20)]
    int m_createCount;

    float m_frontMoveVel;   // �O�i���x
    int m_destroyCount;     // �j�������f�[�^�̐�

    /// <summary>
    /// ����������
    /// </summary>
    void Start()
    {
        m_frontMoveVel = gameObject.transform.parent.gameObject.GetComponent<CS_MoveController>().GetMoveVel();
        m_destroyCount = 0;

        // �m�[�c�f�[�^�̓ǂݍ���
        this.Load();

        // �I�u�W�F�N�g�̐�������
        for (int i = m_destroyCount; i < m_createCount; ++i)
            CreateGameObject(i);
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    void Update()
    {
        for (int i = m_destroyCount; i < m_perNoteInfos.Count; ++i)
        {
            if (i >= m_perNoteInfos.Count)
                break;

            // ��b�O�ɐ������A�v���C���[�̂���ʒu�Ń^�C�~���O���d�Ȃ�悤�ɂ���
            PerNoteInfo noteInfo = m_perNoteInfos[i];
            if ((noteInfo.time - 1.0f) <= m_audioSource.time)
            {
                CreateGameObject(i);

                // �j���f�[�^�𑝂₷
                m_destroyCount++;
            }
            else
                break;
        }
    }

    /// <summary>
    /// �I�u�W�F�N�g�̐�������
    /// </summary>
    /// <param name="index">��������I�u�W�F�N�g�̃C���f�b�N�X</param>
    void CreateGameObject(int index)
    {
        Vector3 createPos = Vector3.zero;
        PerNoteInfo noteInfo = m_perNoteInfos[index];

        const float stride = 30.0f;

        createPos.x = (2 - noteInfo.lane) * stride;
        createPos.y = m_negativePieceObject.transform.position.y;
        createPos.z = (m_frontMoveVel * noteInfo.time) * -1.0f;

        // �I�u�W�F�N�g�̐�������
        GameObject obj = Instantiate(m_negativePieceObject, createPos, Quaternion.identity);
        obj.GetComponent<CS_NegativePiece>().SetVelocity(Vector3.zero, 10.0f);
    }
}
