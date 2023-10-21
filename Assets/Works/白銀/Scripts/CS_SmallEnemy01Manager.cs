using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SmallEnemy01Manager : CS_LoadNotesFile
{
    [SerializeField, CustomLabel("��������G�I�u�W�F�N�g")]
    GameObject m_negativePieceObject;

    [SerializeField, CustomLabel("��ɂ���I�[�f�B�I�f�[�^")]
    AudioSource m_audioSource;

    [SerializeField, CustomLabel("�I�u�W�F�N�g�̐�����")]
    [Range(5, 20)]
    int m_createCount;

    [SerializeField, CustomLabel("�I�t�Z�b�g")]
    public float m_offset;

    List<GameObject> m_gameObjects = new List<GameObject>();

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
        for(int i = m_destroyCount; i < m_createCount; ++i)
            CreateGameObject(i);
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    void Update()
    {
        for (int i = m_destroyCount; i < m_destroyCount + m_createCount; ++i)
        {
            if (i >= m_perNoteInfos.Count)
                break;

            // ���̏������^�ł���΁A�x�N�g���ɗ͂�������
            PerNoteInfo noteInfo = m_perNoteInfos[i];
            if (noteInfo.time <= m_audioSource.time)
            {
                if (noteInfo.lane == 0) // ��(�[���W)
                    m_gameObjects[i].GetComponent<CS_NegativePiece>().SetVelocity(new Vector3( 50.0f, 0.0f, -m_frontMoveVel), 5.0f);
                else                                                                           
                    m_gameObjects[i].GetComponent<CS_NegativePiece>().SetVelocity(new Vector3(-50.0f, 0.0f, -m_frontMoveVel), 5.0f);

                // �j�����𑝂₷
                m_destroyCount++;

                // �V���ȃI�u�W�F�N�g�̐������s��
                if (m_perNoteInfos.Count > m_gameObjects.Count)
                    CreateGameObject(m_gameObjects.Count);
            }
        }
    }

    /// <summary>
    /// �I�u�W�F�N�g�̐�������
    /// </summary>
    /// <param name="index">��������I�u�W�F�N�g�̃C���f�b�N�X</param>
    void CreateGameObject(int index)
    {
        Vector3 createPos = Vector3.zero;

        // �m�[�c�f�[�^�̃��[���ԍ��̎�ނ�2����
        PerNoteInfo noteInfo = m_perNoteInfos[index];
        if (noteInfo.lane == 0) // ��(�[���W)
            createPos.x = -30.0f;
        else
            createPos.x =  30.0f;

        createPos.y = m_negativePieceObject.transform.position.y;
        createPos.z = (m_frontMoveVel * noteInfo.time + m_offset) * -1.0f;

        // �I�u�W�F�N�g�̐�������
        GameObject obj = Instantiate(m_negativePieceObject, createPos, Quaternion.identity);
        m_gameObjects.Add(obj);
    }
}
