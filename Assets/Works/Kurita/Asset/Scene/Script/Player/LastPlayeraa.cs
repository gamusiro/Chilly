using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class LastPlayeraa : MonoBehaviour
{
    // �W�����v��
    [SerializeField, CustomLabel("�W�����v��")]
    float _jump;
    [SerializeField] private Rigidbody _rigidbody;

    // �^���d��
    [SerializeField]
    [Range(1.0f, 1000.0f)]
    float m_gravity;

    //�Ăяo���p
    private bool _onJumpRamp = false;
    public bool OnBell = false;

    private void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        //�d��
        Vector3 force = new Vector3(0.0f, -m_gravity, 0.0f);
        _rigidbody.AddForce(force);
    }

    private async void Jump()
    {
        //�W�����v���肪True�ɂȂ�����W�����v
        await UniTask.WaitUntil(() => _onJumpRamp);
        _rigidbody.AddForce(new Vector3(0, _jump, 0), ForceMode.Impulse);
        _onJumpRamp = false;
    }

    //��p�̃I�u�W�F�N�g�ɐG�ꂽ��jump����悤�ɂ���
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "JumpRamp")
            _onJumpRamp = true;

        if (other.tag == "Bell")
            OnBell = true;
    }
}