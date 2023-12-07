using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
class TutorialData
{
    public float totalTime;

    [Tooltip("totalTime / ���[�v�J�E���g / ���� ������؂��l�ɂ��Ȃ��ƕςȂƂ���Ń��[�v���I��邗����")]
    public int loopCount;
    public List<Material> materials;
}

public class CS_TutorialAnimation : MonoBehaviour
{
    #region �C���X�y�N�^�p�ϐ�

    [SerializeField, CustomLabel("�`���[�g���A���J�n����")]
    float m_startTime;

    [SerializeField]
    List<TutorialData> m_data;

    #endregion

    #region �����p�ϐ�

    // �g�p���̃`���[�g���A���^�C�v(���ړ��Ƃ��A�E�ړ��Ƃ��������C���f�N�X)
    int m_typeIndex;

    // �g�p���̃A�j���[�V�����̃R�}������
    int m_pageIndex;

    // ���ꂼ��̐؂�ւ�����
    float m_changeTypeTime;
    float m_changePageTime;

    // ���ꂼ��̌o�ߎ���
    float m_curTypeTime;
    float m_curPageTime;

    // �����_���[
    Renderer m_renderer;

    // �T�C���{�[�h
    CS_Signboard m_signboard;

    #endregion


    /// <summary>
    /// ����������
    /// </summary>
    void Start()
    {
        m_typeIndex = 0;
        m_pageIndex = 0;
        m_signboard = GetComponent<CS_Signboard>();

        // �����Ȃ��ꍇ�͂����Ŕj��
        if (m_data.Count == 0)
        {
            Destroy(gameObject);
            return;
        }
            

        // �f�[�^��ݒ肷��
        TutorialData data = m_data[m_typeIndex];
        m_changeTypeTime = data.totalTime;
        m_changePageTime = (m_changeTypeTime  / data.loopCount) / data.materials.Count;

        m_curTypeTime = 0.0f;
        m_curPageTime = 0.0f;

        m_renderer = transform.Find("Screen").gameObject.GetComponent<Renderer>();

        // �����}�e���A���̓\��t��
        ChangeScreen();
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    void Update()
    {
        if (!m_signboard.StartTutorial())
            return;

        // �ύX���������ꍇ���݃}�e���A����؂�ւ���
        bool change = false;

        // �o�ߎ��Ԃ̍X�V
        m_curTypeTime += Time.deltaTime;
        m_curPageTime += Time.deltaTime;

        // �Ⴄ�`���[�g���A���ɐ؂�ւ���
        if(m_changeTypeTime <= m_curTypeTime)
        {
            m_curTypeTime = 0.0f;
            m_curPageTime = 0.0f;
            m_pageIndex = 0;
            m_typeIndex++;

            // �C���f�b�N�X���傫���ꍇ�͏I��
            if (m_typeIndex >= m_data.Count)
            {
                m_signboard.SetStateShrink();
                return;
            }
                
            // ���̃f�[�^�ɐݒ肷��
            TutorialData data = m_data[m_typeIndex];
            m_changeTypeTime = data.totalTime;
            m_changePageTime = (m_changeTypeTime  / data.loopCount) / data.materials.Count;

            // �ύX��������
            change = true;
        }

        // ���̉摜�ɐi�߂�
        if(m_changePageTime <= m_curPageTime)
        {
            m_curPageTime = 0.0f;
            m_pageIndex++;

            // �ύX��������
            change = true;
        }

        // �摜�̐؂�ւ������s����
        if(change)
            ChangeScreen();
    }

    /// <summary>
    /// �؂�ւ�
    /// </summary>
    void ChangeScreen()
    {
        if (m_typeIndex >= m_data.Count)
            return;

        int cnt = m_data[m_typeIndex].materials.Count;
        m_renderer.material = m_data[m_typeIndex].materials[m_pageIndex % cnt];
    }
}
