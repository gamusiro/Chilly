using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CS_HeartPieceManager : CS_LoadNotesFile
{
    #region �C���X�y�N�^�p�ϐ�

    // ��������I�u�W�F�N�g
    [SerializeField, CustomLabel("�����I�u�W�F�N�g")]
    GameObject m_createObject;

    // �I�u�W�F�N�g�̐�����
    [SerializeField, CustomLabel("�I�u�W�F�N�g�̐�����")]
    [Range(20.0f, 100.0f)]
    int m_numMax = 20;

    // ���C���J�����I�u�W�F�N�g
    [SerializeField, CustomLabel("�J�����}�l�[�W���[")]
    GameCameraPhaseManager m_cameraManager;

    #endregion

    #region �����p�ϐ�

    // �Q�[���I�u�W�F�N�g�Ǘ��p
    GameObject[] m_objects;

    // ������
    int m_createCount = 0;

    #endregion


    /// <summary>
    /// ����������
    /// </summary>
    void Start()
    {
        m_objects = new GameObject[m_numMax];
        m_createCount = 0;

        this.Load();
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    void FixedUpdate()
    {
        if (!CS_MoveController.IsMoving())
            return;

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
        createPos.y = 4.0f;
        createPos.z = info.time * CS_MoveController.GetMoveVel() * -1.0f;

        GameObject obj = Instantiate(m_createObject, createPos, Quaternion.identity);
        obj.GetComponentInChildren<CS_HeartPiece>().SetMainGameCameraManager(m_cameraManager);
        m_objects[index] = obj;

        Destroy(obj, info.time - CS_AudioManager.Instance.TimeBGM + 0.5f);

        m_createCount++;
    }
}
