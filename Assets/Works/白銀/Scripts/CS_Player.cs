using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Player : MonoBehaviour
{
    #region �C���X�y�N�^�p�ϐ�
    //���ݎg�p���̃J�����̏��
    [SerializeField] private MainGameCameraManager m_mainGameCameraManager;

    // �v���C���[�̉��ړ����x
    [SerializeField, CustomLabel("���ړ��̃X�s�[�h")]
    float m_side;

    // �W�����v��
    [SerializeField, CustomLabel("�W�����v��")]
    float m_jump;

    // ���G����
    [SerializeField, CustomLabel("���G����")]
    float m_invalidTime;

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

    // �}�e���A��
    Material m_material;

    // �g�p���̃J�����I�u�W�F�N�g
    GameObject m_mainVirtualCamera;

    const float c_sideMax = 69.0f;

    // �W�����v�����ǂ����̃t���O
    bool m_isFlying;

    // �Փ˂�����
    bool m_damaged;

    // �F
    float m_degree;

    #endregion

    /// <summary>
    /// ����������
    /// </summary>
    void Start()
    {
        m_inputAction = new IA_Player();
        m_inputAction.Enable();

        m_rigidBody = GetComponent<Rigidbody>();
        m_material = gameObject.transform.GetChild(0).GetComponent<Renderer>().material;

        m_mainVirtualCamera = m_mainGameCameraManager.GetCurCamera();
        m_isFlying = false;
        m_damaged = false;
        m_degree = 0.0f;
    }

    /// <summary>
    /// �X�V����(���t���[��)
    /// </summary>
    void Update()
    {
        Vector2 direction = m_inputAction.Player.Move.ReadValue<Vector2>();

        // �ړ�����
        Vector3 currentPos = transform.position;
        Vector3 move = new Vector3(direction.x * m_side, 0.0f, 0.0f);
        move.x *= m_mainVirtualCamera.transform.right.x;

        currentPos += move * Time.deltaTime;
        currentPos.x = Mathf.Clamp(currentPos.x, -c_sideMax, c_sideMax);

        transform.position = currentPos;

        // ������ݒ�
        Vector3 rotate = Vector3.zero;
        rotate.y = -30.0f * direction.x * m_mainVirtualCamera.gameObject.transform.right.x;

        transform.localEulerAngles = rotate;

        // �W�����v
        if (m_inputAction.Player.Jump.triggered && !m_isFlying)
        {
            CS_AudioManager.Instance.PlayAudio("Jump");

            m_isFlying = true;
            m_rigidBody.AddForce(new Vector3(0, m_jump, 0), ForceMode.Impulse);
        }

        // �X���C�h����
        if(m_inputAction.Player.SlideL.triggered)
        {
            m_rigidBody.AddForce(move * 2.0f, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// FPS����
    /// </summary>
    private void FixedUpdate()
    {
        Vector3 force = new Vector3(0.0f, -m_gravity, 0.0f);
        m_rigidBody.AddForce(force);

        m_degree++;

        // �Ԃ����Ă�����
        if(m_damaged)
        {
            float c = Mathf.Cos(m_degree);

            Color color = m_material.color;
            color.r = c; color.g = c; color.b = c;
            m_material.color = color;
        }
    }

    /// <summary>
    /// �����蔻��
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Field")
            m_isFlying = false;

        // �_���[�W���󂯕t�����Ԃ�
        if (!m_damaged)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                m_damaged = true;
                m_degree = 0.0f;
                Invoke(nameof(UnlockInvincibility), m_invalidTime);
            }
        }
    }

    /// <summary>
    /// �J�����̐ݒ�
    /// </summary>
    public void SetUsingCamera()
    {
        m_mainVirtualCamera =  m_mainGameCameraManager.GetCurCamera();
    }

    /// <summary>
    /// ���G���Ԃ̖�����
    /// </summary>
    void UnlockInvincibility()
    {
        m_damaged = false;
        m_material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }
}
