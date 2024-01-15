using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


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

    // �Ŋ��X�P�[��
    [SerializeField, CustomLabel("�Ŋ��X�P�[��")]
    Vector3 m_endScale;

    // �j������܂ł̎���
    [SerializeField, CustomLabel("�����鎞��")]
    [Range(1.0f, 10.0f)]
    float m_workOffTime;

    // �㉺�ɓ�����
    [SerializeField, CustomLabel("�ړ���")]
    float m_amount;

    // �Ŕ��傫���Ȃ�^�C�~���O
    [SerializeField, CustomLabel("�Ŕ̃X�P�[���ύX�J�n����")]
    float m_setTime = 8.0f;

    #endregion

    #region �����p�ϐ�

    // �x���^�C��
    const float c_delayTime = 1.0f;

    // ���[�N�ϐ�
    float m_tmp;
    float m_rad;

    // �����ʒu���擾
    Vector3 m_originLocalPosition;

    // �X�e�[�g���
    STATE m_state;
    bool m_startChange;

    #endregion


    /// <summary>
    /// ������
    /// </summary>
    private void Awake()
    {
        gameObject.SetActive(CS_GameManager.GetOnTutorial);
    }

    /// <summary>
    /// ����������
    /// </summary>
    void Start()
    {
        // ���[�J�����W���擾
        m_originLocalPosition = transform.localPosition;

        // �X�P�[���̎擾�E�ύX
        transform.localScale = m_startScale;

        // ��ԕϐ�
        m_rad = 0.0f;
        m_tmp = 0.0f;

        // ��Ԃ��Ăяo���܂�
        Invoke(nameof(SetStateExpand), m_setTime);
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    void Update()
    {
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
                    localScale.x = Mathf.Lerp(m_startScale.x, m_endScale.x, Mathf.Clamp01(m_tmp - c_delayTime));
                    localScale.y = Mathf.Lerp(m_startScale.y, m_endScale.y, Mathf.Clamp01(m_tmp));
                    localScale.z = Mathf.Lerp(m_startScale.z, m_endScale.z, Mathf.Clamp01(m_tmp - c_delayTime));

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
                    Vector3 perScale = m_endScale / m_workOffTime;

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
        addPosition.y *= Mathf.Cos(m_rad) * m_amount;

        transform.localPosition = m_originLocalPosition + addPosition;
    }

    /// <summary>
    /// ��Ԃ�؂�ւ���
    /// </summary>
    void SetStateExpand()
    {
        m_tmp = 0.0f;
        m_state = STATE.EXPAND;
        m_startChange = true;

        // �c�ɍL����̂��킩��悤�ɏ�������������^����
        m_startScale.x = 0.01f;
    }

    /// <summary>
    /// ��Ԃ�؂�ւ���
    /// </summary>
    public void SetStateShrink()
    {
        m_tmp = 0.0f;
        m_state = STATE.SHRINK;
    }

    public bool StartTutorial()
    {
        return (m_state == STATE.NONE) && m_startChange;
    }
}
