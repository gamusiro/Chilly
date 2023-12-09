using Cinemachine;
using DG.Tweening;
using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CS_Player : MonoBehaviour
{
    #region �C���X�y�N�^�p�ϐ�

    [Header("�v���C���[�p�����[�^")]

    // �v���C���[�̉e���W
    [SerializeField, CustomLabel("�v���C���[�̉e���W")]
    Transform m_shadowTransform;

    // �v���C���[�̉��ړ����x
    [SerializeField, CustomLabel("���ړ��̃X�s�[�h")]
    float m_side;

    // �W�����v��
    [SerializeField, CustomLabel("�W�����v��")]
    float m_jump;

    // ���G����
    [SerializeField, CustomLabel("���G����")]
    float m_invalidTime;

    // �_�b�V����
    [SerializeField, CustomLabel("�_�b�V���̗�")]
    [Range(2.0f, 10.0f)]
    float m_dashStrength;

    // �^���d��
    [SerializeField, Header("�d�͉e���x")]
    [Range(1.0f, 1000.0f)]
    float m_gravity;

    // �v���C���[�A�j���[�V����
    [SerializeField, CustomLabel("�_���[�W����")]
    HP m_hp;

    // �v���C���[�A�j���[�V����
    [SerializeField, CustomLabel("�A�j���[�^")]
    Animator m_animator;

    [SerializeField, Header("�u���C���J����")]
    CinemachineBrain m_brain;

    //���ݎg�p���̃J�����̏��
    [SerializeField, CustomLabel("�J�����}�l�[�W���[")] 
    CameraPhaseManager m_mainGameCameraManager;

    [Header("�p�[�t�F�N�g�^�C�~���O")]

    // ���e�p�[�t�F�N�g�^�C�~���O
    [SerializeField, CustomLabel("�p�[�t�F�N�g�^�C�~���O(�b)")]
    [Range(0.1f, 1.0f)]
    float m_perfectTimeRange;

    [Header("�X�s���G�t�F�N�g")]
    [SerializeField] private SpinEffect _spinEffectPrefab;

   // [SerializeField, CustomLabel("��납��")]
   // CS_EnemyAttackFromEnemy m_enemyAttackFromEnemy;

    [Header("�G�t�F�N�g�I�u�W�F�N�g")]
    [SerializeField, CustomLabel("�p�[�t�F�N�g�^�C�~���O�G�t�F�N�g")]
    GameObject m_perfectEffectObject;

    [Header("�v���C���[���f��")]
    [SerializeField, CustomLabel("�v���C���[���f��")]
    private GameObject _playerModel;

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

    // �����萔
    const float c_sideMax = 69.0f;

    // �W�����v�����ǂ����̃t���O
    public bool m_isFlying;

    // �Փ˂�����
    bool m_damaged;

    // �F
    float m_degree;

    // �Ō�ɃW�����v��������
    float m_timeLastJumped;

    //��]�t���O
    private bool _isRotate;

    #endregion

    #region ���J�p�ϐ�

    // �W�����v�����ǂ���
    public float timeLastJumped 
    { 
        get { return m_timeLastJumped; }
    }


    #endregion

    /// <summary>
    /// ����������
    /// </summary>
    void Start()
    {
        m_inputAction = new IA_Player();
        m_inputAction.Enable();

        m_rigidBody = GetComponent<Rigidbody>();
        m_material = gameObject.transform.GetChild(0).Find("mesh_Character").GetComponent<Renderer>().material;

        m_mainVirtualCamera = m_mainGameCameraManager.GetCurCamera();
        m_isFlying = false;
        m_damaged = false;
        m_degree = 0.0f;
        _isRotate = false;
    }

    /// <summary>
    /// �X�V����(���t���[��)
    /// </summary>
    void Update()
    {
        // �e�̍��W���X�V
        Vector3 pos = transform.localPosition;
        pos.y = 0.0f;
        m_shadowTransform.localPosition = pos;

        // �J�����̈ړ����̏ꍇ�͑��삳���Ȃ�
        if (m_brain.ActiveBlend != null)
            return;

        Vector2 direction = m_inputAction.Player.Move.ReadValue<Vector2>();

        // �ړ�����
        Vector3 currentPos = transform.position;
        Vector3 move = new Vector3(direction.x * m_side, 0.0f, 0.0f);
        move.x *= m_mainVirtualCamera.transform.right.x;

        currentPos += move * Time.deltaTime;
        currentPos.x = Mathf.Clamp(currentPos.x, -c_sideMax, c_sideMax);

        //currentPos.y = Mathf.Max(0.0f, currentPos.y);
        transform.position = currentPos;

        // ������ݒ�
        Vector3 rotate = Vector3.zero;
        rotate.y = -30.0f * direction.x * m_mainVirtualCamera.gameObject.transform.right.x;

        transform.localEulerAngles = rotate;

        //�X�s��
        if (Input.GetKeyDown(KeyCode.V))
        {
            _playerModel.transform.DOComplete();
            Vector3 modelRotate = Vector3.zero;
            modelRotate.y = 360.0f+180.0f;
            var tweener=_playerModel.transform.DOLocalRotate(modelRotate, 0.5f, RotateMode.FastBeyond360)
                .OnComplete(() => _isRotate = false)
                .SetLink(this.gameObject);
            _isRotate = true;

            //Instantiate(_spinEffectPrefab, this.transform.position, Quaternion.identity, this.transform);
        }

        // �W�����v
        if (m_inputAction.Player.Jump.triggered && !m_isFlying)
        {
            // float subFromEnemy = m_enemyAttackFromEnemy.GetPerfectTime() - CS_AudioManager.Instance.TimeBGM;

            //// ��납��̃^�C�~���O(perfectTiming)
            //if (subFromEnemy <= m_perfectTimeRange && subFromEnemy > 0.0f)
            //{
            //    GameObject obj = Instantiate(m_perfectEffectObject);
            //    obj.transform.parent = transform;

            //    Vector3 localPos = transform.localPosition;
            //    localPos.y = 0.0f;
            //    obj.transform.localPosition = Vector3.zero;
            //    obj.transform.localScale = new Vector3(3.0f, 2.0f, 3.0f);

            //    // �G�t�F�N�g�̍Đ�
            //    foreach (ParticleSystem ps in obj.GetComponentsInChildren<ParticleSystem>())
            //        ps.Play();

            //    CS_AudioManager.Instance.PlayAudio("PerfectJump");

            //    Destroy(obj, 1.0f);
            //}
            //else
            //{
            //    // �ʏ�W�����v
            //    CS_AudioManager.Instance.PlayAudio("Jump");
            //    m_animator.Play("Jumping", 0, 0.0f);        // �ŏ����痬�������̂ł���Ȋ����̐ݒ�
            //}

            m_isFlying = true;
            m_rigidBody.constraints = RigidbodyConstraints.None;

            // ��]�͂��Ȃ� | Z�̒l�Œ�
            m_rigidBody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;


            m_rigidBody.AddForce(new Vector3(0, m_jump, 0), ForceMode.Impulse);
        }

        Vector3 currentVel = m_rigidBody.velocity;
        if (m_inputAction.Player.SlideL.triggered)
        {
            CS_AudioManager.Instance.PlayAudio("Dash");

            // ���ړ��̑��x���~�߂�
            currentVel.x = 0.0f;
            m_rigidBody.velocity = currentVel;

            m_rigidBody.AddForce(new Vector3(-m_side * m_mainVirtualCamera.transform.right.x * m_dashStrength, 0.0f, 0.0f), ForceMode.VelocityChange);
            m_animator.Play("DashR", 0, 0.0f);        // �ŏ����痬�������̂ł���Ȋ����̐ݒ�
        }
        else if (m_inputAction.Player.SlideR.triggered)
        {
            CS_AudioManager.Instance.PlayAudio("Dash");

            // ���ړ��̑��x���~�߂�
            currentVel.x = 0.0f;
            m_rigidBody.velocity = currentVel;

            m_rigidBody.AddForce(new Vector3(m_side * m_mainVirtualCamera.transform.right.x * m_dashStrength, 0.0f, 0.0f), ForceMode.VelocityChange);
            m_animator.Play("DashL", 0, 0.0f);        // �ŏ����痬�������̂ł���Ȋ����̐ݒ�
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
        string tag = collision.gameObject.tag;

        if (tag == "Field")
        {
            m_isFlying = false;
            m_rigidBody.constraints |= RigidbodyConstraints.FreezePositionY;
            m_animator.Play("Running", 0, 0.0f);    // �ŏ�����
        }

        Debug.Log(tag);

        // �_���[�W���󂯕t�����Ԃ�
        if (!m_damaged)
        {
            if (tag == "Enemy")
            {
                m_damaged = true;
                CS_AudioManager.Instance.PlayAudio("Damage");
                m_hp.Hit();
                m_degree = 0.0f;
                Invoke(nameof(UnlockInvincibility), m_invalidTime);
            }
        }
    }

    /// <summary>
    /// ������΂��I�u�W�F�N�g�Ƃ̐ڐG
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Scattering")
        {
            if(IsDashing)
            {
                Vector3 dir = Vector3.zero;
                dir.x = m_rigidBody.velocity.x;

                var rb = other.gameObject.GetComponent<Rigidbody>();
                rb.AddForce(dir * 5.0f, ForceMode.Impulse);
            }
        }
    }

    /// <summary>
    /// �J�����̐ݒ�
    /// </summary>
    public void SetUsingCamera()
    {
        if(m_mainGameCameraManager)
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

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool IsDashing
    {
        get { return Mathf.Abs(m_rigidBody.velocity.x) > 0.0f; }
    }

    public bool GetSpin()
    {
        return _isRotate;
    }
}
