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
    GameObject[] m_gameObjects;

    int m_createCount;

    AudioSource m_audioSource;

    #endregion


    /// <summary>
    /// ����������
    /// </summary>
    private void Start()
    {
        m_gameObjects = new GameObject[m_numMax];
        m_audioSource = CS_AudioManager.Instance.GetAudioSource("GameAudio");

        m_createCount = 0;

        // �ǂݍ��ݏ���
        this.Load();
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    void Update()
    {
        if (!m_audioSource.isPlaying)
            return;

        // �S�Ă����[�v����
        for(int i = 0; i < m_numMax; ++i)
        {
            if (m_gameObjects[i] == null)
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

        Vector3 createPos = Vector3.zero;

        float time = m_perNoteInfos[m_createCount].time - m_audioSource.time;

        createPos.x = -60.0f + m_perNoteInfos[m_createCount].lane * 30.0f;
        createPos.y = (9.81f / 2.0f) * Mathf.Pow(time, 2.0f);
        createPos.z = m_perNoteInfos[m_createCount].time * CS_MoveController.GetMoveVel() * -1.0f;

        GameObject obj = Instantiate(m_createObject, createPos, Quaternion.identity);
        m_gameObjects[index] = obj;
        m_createCount++;

        createPos.y = 0.1f;
        GameObject sdw = Instantiate(m_shadowObject, createPos, Quaternion.identity);

        Destroy(obj, time + m_graceTime);
        Destroy(sdw, time + m_graceTime);
    }
}
