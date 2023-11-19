using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[Serializable]
class SignBoardData
{
    public Material m_material;
    public float    m_changeTime;
}


public class CS_Signboard : MonoBehaviour
{
    // �X�e�[�g���
    enum STATE
    {
        NONE,
        EXPAND,
        SHRINK
    }

    #region �C���X�y�N�^�p�ϐ�

    // �����X�P�[��
    [SerializeField, CustomLabel("�����X�P�[��")]
    Vector3 m_startScale;

    // �j������܂ł̎���
    [SerializeField, CustomLabel("�����鎞��")]
    [Range(1.0f, 10.0f)]
    float m_workOffTime;

    // �Ŕp�}�e���A���f�[�^
    [SerializeField, CustomLabel("�`���[�g���A���Ŕf�[�^")]
    List<SignBoardData> m_boardDatasList;

    #endregion

    #region �����p�ϐ�

    // �x���^�C��
    const float c_delayTime = 1.0f;

    // �g�p�����f�[�^�̐�
    int m_usedCount;

    // ���[�N�ϐ�
    float m_tmp;
    float m_rad;

    // �����ʒu���擾
    Vector3 m_originLocalPosition;

    // �����X�P�[�����擾
    Vector3 m_baseScale;

    // �X�e�[�g���
    STATE m_state;

    // �����_���[�̎擾
    Renderer m_renderer;

    #endregion




    /// <summary>
    /// ����������
    /// </summary>
    void Start()
    {
        m_usedCount = 0;

        // �Ŕf�[�^���Ȃ���Α����ɔj��
        if(m_boardDatasList.Count == 0)
        {
            Destroy(gameObject);
            return;
        }

        // �����_���[�̎擾
        m_renderer = transform.Find("Screen").gameObject.GetComponent<Renderer>();
        m_renderer.material = m_boardDatasList[0].m_material;

        // ���[�J�����W���擾
        m_originLocalPosition = transform.localPosition;

        // �X�P�[���̎擾�E�ύX
        m_baseScale = transform.localScale;
        transform.localScale = m_startScale;

        // ��ԕϐ�
        m_rad = 0.0f;
        m_tmp = 0.0f;

        // ��Ԃ��Ăяo���܂�
        Invoke(nameof(SetStateExpand), 8.0f);
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    void Update()
    {
        // �k�����Ȃ�߂�
        if (m_state == STATE.SHRINK)
            return;

        // ���݂̃I�[�f�B�I�^�C�����擾
        float currentlyAudioTime = CS_AudioManager.Instance.TimeBGM;

        // �}�e���A����؂�ւ���^�C�~���O�ł����
        if(currentlyAudioTime >= m_boardDatasList[m_usedCount].m_changeTime)
        {
            // �V�������N���Ăяo��
            if ((m_usedCount + 1) >= m_boardDatasList.Count)
            {
                SetStateShrink();
                return;
            }

            // �}�e���A����ύX
            m_renderer.material = m_boardDatasList[m_usedCount].m_material;
            m_usedCount++;
        }
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    private void FixedUpdate()
    {
        Idle();

        switch(m_state)
        {
            case STATE.EXPAND:
                {
                    // ���Ԃ̍X�V
                    m_tmp += Time.deltaTime;

                    // �X�P�[���̌v�Z
                    Vector3 localScale = transform.localScale;
                    localScale.x = Mathf.Lerp(m_startScale.x, m_baseScale.x, Mathf.Clamp01(m_tmp - c_delayTime));
                    localScale.y = Mathf.Lerp(m_startScale.y, m_baseScale.y, Mathf.Clamp01(m_tmp));
                    localScale.z = Mathf.Lerp(m_startScale.z, m_baseScale.z, Mathf.Clamp01(m_tmp - c_delayTime));

                    // �X�P�[���̐ݒ�
                    transform.localScale = localScale;

                    // ���`��Ԃ̒l + 1.0f�ȏ�ɂȂ������Ԃ�߂�
                    if (m_tmp >= c_delayTime + 1.0f)
                        m_state = STATE.NONE;
                }
                break;
            case STATE.SHRINK:
                {
                    // ���Ԃ̍X�V
                    m_tmp += Time.deltaTime;

                    // �X�P�[���̌v�Z
                    Vector3 perScale = m_baseScale / m_workOffTime;

                    // �X�P�[���̐ݒ�
                    transform.localScale = perScale * (m_workOffTime - m_tmp);

                    // �f�[�^��j������
                    if (m_tmp > m_workOffTime)
                        Destroy(gameObject);
                }
                break;
        }
    }

    /// <summary>
    /// �Ŕ̏㉺����
    /// </summary>
    void Idle()
    {
        m_rad += Time.deltaTime;
        Vector3 addPosition = Vector3.up;
        addPosition.y *= Mathf.Cos(m_rad) * 20.0f;

        transform.localPosition = m_originLocalPosition + addPosition;
    }

    /// <summary>
    /// ��Ԃ�؂�ւ���
    /// </summary>
    void SetStateExpand()
    {
        m_tmp = 0.0f;
        m_state = STATE.EXPAND;
    }

    /// <summary>
    /// ��Ԃ�؂�ւ���
    /// </summary>
    void SetStateShrink()
    {
        m_tmp = 0.0f;
        m_state = STATE.SHRINK;
    }
}
