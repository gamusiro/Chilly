using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CS_SmallEnemy02Manager : CS_LoadNotesFile
{
    [SerializeField, CustomLabel("��������G�I�u�W�F�N�g")]
    GameObject m_negativePieceObject;

    [SerializeField, CustomLabel("��ɂ���I�[�f�B�I�f�[�^")]
    AudioSource m_audioSource;

    [SerializeField, CustomLabel("�X�g���C�h")]
    float m_stride;

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

        createPos.x = (2 - noteInfo.lane) * m_stride;
        createPos.y = m_negativePieceObject.transform.position.y;
        createPos.z = gameObject.transform.position.z;

        Vector3 setVel = new Vector3(0.0f, 0.0f, -(gameObject.transform.localPosition.z + m_frontMoveVel));

        // �I�u�W�F�N�g�̐�������
        GameObject obj = Instantiate(m_negativePieceObject, createPos, Quaternion.identity);
        obj.GetComponent<CS_NegativePiece>().SetVelocity(setVel, 2.0f);
    }
}
