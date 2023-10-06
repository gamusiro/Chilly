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

    IA_Player m_inputActions;   // ����
    Rigidbody m_rigidbody;      // ����
    bool m_isFlying;

    // Start is called before the first frame update
    void Start()
    {
        m_inputActions = new IA_Player();
        m_inputActions.Enable();

        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerJump();
    }

    /// <summary>
    /// �t�B�[���h�ƐڐG�����
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        m_isFlying = true;
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
        Vector3 dirVec = new Vector3(inputVec.x * -1.0f, 0.0f, 0.0f);
        Vector3 nexPos = curPos + dirVec * m_moveVel * Time.deltaTime;

        transform.position = nexPos;
    }

    /// <summary>
    /// �v���C���[�̃W�����v����
    /// </summary>
    void PlayerJump()
    {
        if (m_inputActions.Player.Jump.triggered && m_isFlying)
        {
            m_rigidbody.AddForce(Vector3.up * m_jumpVel, ForceMode.Impulse);
            m_isFlying = false;
        }
    }
}
