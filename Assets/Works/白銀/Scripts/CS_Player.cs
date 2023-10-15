using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CS_Player : MonoBehaviour
{
    [SerializeField, CustomLabel("���ړ��̑��x")]
    float m_moveVel = 5.0f;

    [SerializeField, CustomLabel("�W�����v�̋���")]
    float m_jumpVel = 5.0f;

    [SerializeField, CustomLabel("�J�����I�u�W�F�N�g")]
    GameObject m_cameraObject;

    IA_Player m_inputActions;   // ����
    Rigidbody m_rigidbody;      // ����
    bool m_isFlying;            // ���ō��

    bool m_isDamaging;          // �_���[�W����

    /// <summary>
    /// ����������
    /// </summary>
    void Start()
    {
        m_inputActions = new IA_Player();
        m_inputActions.Enable();

        m_rigidbody = GetComponent<Rigidbody>();

        m_isFlying = false;
        m_isDamaging = false;
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    void Update()
    {
        PlayerMove();
        PlayerJump();
        PlayerDamage();
    }

    /// <summary>
    /// �t�B�[���h�ƐڐG�����
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        m_isFlying = false;
    }

    /// <summary>
    /// �G�ƐڐG������
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        m_isDamaging = true;
    }

    /// <summary>
    /// �v���C���[�̈ړ�����
    /// </summary>
    void PlayerMove()
    {
        // ���͒l���擾
        Vector2 inputVec = m_inputActions.Player.Move.ReadValue<Vector2>();

        // ���݃|�W�V�����̎擾
        Vector3 curPos = transform.position;

        // ���̈ړ��ʒu�v�Z ���J�����̌����𔽓]���Ă邽�߁A�����͂̒l�𔽓]������
        Vector3 dirCam = m_cameraObject.transform.right;
        Vector3 dirVec = new Vector3(inputVec.x * dirCam.x, 0.0f, 0.0f);
        Vector3 nexPos = curPos + dirVec * m_moveVel * Time.deltaTime;

        transform.position = nexPos;
    }

    /// <summary>
    /// �v���C���[�̃W�����v����
    /// </summary>
    void PlayerJump()
    {
        if (m_inputActions.Player.Jump.triggered && !m_isFlying)
        {
            m_rigidbody.AddForce(Vector3.up * m_jumpVel, ForceMode.Impulse);
            m_isFlying = true;
        }
    }

    /// <summary>
    /// �v���C���[�̃_���[�W
    /// </summary>
    void PlayerDamage()
    {
        if(m_isDamaging)
        {
            m_isDamaging = false;
            Debug.Log("�Ԃ���");
        }
    }
}
