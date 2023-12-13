using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using DG.Tweening;

public class GameEnemy : Enemy
{
    //�̂̃p�[�e�B�N���Ɋւ������
    [SerializeField] protected ParticleSystem _particleSystem;
    protected float _frameHit;//�q�b�g���Ă���̃t���[��

    [SerializeField] protected Gradient _gradientOriginal;//���̐F
    [SerializeField] protected Gradient _gradientHit;//�q�b�g���̐F
    [SerializeField] protected Color _defeatColor;//�|�����Ƃ��̐F

    //����
    [SerializeField] protected Transform _standardPosition;
    protected float _moveSpeed;

    private void Start()
    {
        //�ڂƌ��̃��W�A��
        _eyeTransform[(int)Eye.Left].eulerAngles = new Vector3(0.0f, 0.0f, 45.0f);
        _eyeTransform[(int)Eye.Right].eulerAngles = new Vector3(0.0f, 0.0f, 225.0f);
        _subEyeSpeed = _eyeSpeed = 4.0f;
        _subEyeSpeed *= 0.1f;
        _mouthScaleeRadian = 0.0f;
        _subMouthSpeed = _mouthSpeed = 0.04f;
        _subMouthSpeed *= 0.4f;
        _standardMouthScale = _mouthTransform.localScale;
        _moveSpeed = 3.0f;
        //���̑��ϐ�
        _frameHit = 999.0f;//�q�b�g�t���[��
    }

    private void FixedUpdate()
    {
        HitAnimation();
        Eyes();
        Mouth();

        float range = 10.0f;
        Move(range);
    }

    //�����蔻��
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HeartNotes")
        {
            Hit();
            Debug.Log("�g���K�[�Ƀq�b�g");
        }
    }

    //�U�����ꂽ����
    protected void Hit()
    {
        _frameHit = 0;
    }

    protected void HitAnimation()
    {
        //�F�ύX
        ParticleSystem.ColorOverLifetimeModule colorOverLifetime = _particleSystem.colorOverLifetime;
        colorOverLifetime.enabled = true;
        Gradient gradient;
        gradient = _gradientOriginal;

        //�q�b�g�A�j���[�V�������쓮���鎞��
        if (_frameHit < 2.0f)
        {
            _frameHit += Time.deltaTime * 10.0f;

            //�_�ł�����
            if (((int)_frameHit) % 2 == 0)
                gradient = _gradientHit;
            else
                gradient = _gradientOriginal;
        }
        colorOverLifetime.color = gradient;
    }
}
