using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Player : MonoBehaviour
{
    #region �C���X�y�N�^�p�ϐ�

    // �v���C���[�̉��ړ����x
    [SerializeField, CustomLabel("���ړ��̃X�s�[�h")]
    float m_side;

    // �W�����v��
    [SerializeField, CustomLabel("�W�����v��")]
    float m_jump;

    // �^���d��
    [SerializeField, Header("�d�͉e���x")]
    [Range(1.0f, 1000.0f)]
    float m_gravity;

    #endregion

    #region �����p�ϐ�

    // InputSystem
    IA_Player m_inputAction;
    
    // ���W�b�h�{�f�B
    Rigidbody m_rigidBody;

    // �W�����v�����ǂ����̃t���O
    bool m_isFlying;

    #endregion

    /// <summary>
    /// ����������
    /// </summary>
    void Start()
    {
        m_inputAction = new IA_Player();
        m_inputAction.Enable();

        m_rigidBody = GetComponent<Rigidbody>();
        m_isFlying = false;
    }

    /// <summary>
    /// �X�V����(���t���[��)
    /// </summary>
    void Update()
    {
        Vector2 direction = m_inputAction.Player.Move.ReadValue<Vector2>();

        // �ړ�����
        Vector3 move = new Vector3(direction.x * m_side, 0.0f, 0.0f);
        transform.Translate(move * Time.deltaTime);

        // �W�����v
        if (m_inputAction.Player.Jump.triggered && !m_isFlying)
        {
            CS_AudioManager.Instance.PlayAudio("Jump");

            m_isFlying = true;
            m_rigidBody.AddForce(new Vector3(0, m_jump, 0), ForceMode.Impulse);
        }
    }

    /// <summary>
    /// FPS����
    /// </summary>
    private void FixedUpdate()
    {
        Vector3 force = new Vector3(0.0f, -m_gravity, 0.0f);
        m_rigidBody.AddForce(force);
    }

    /// <summary>
    /// �����蔻��
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        m_isFlying = false;

        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("�G�ƂԂ�����");
        }
    }
}
