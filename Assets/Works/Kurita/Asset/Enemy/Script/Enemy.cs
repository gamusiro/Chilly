using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //�ڂ̓����ւ������
    private enum Eye   
    {
        Left,
        Right
    }
    [SerializeField] protected List<Transform> _eyeTransform = new List<Transform>();
    protected List<float> _eyeRadian = new List<float>();//��]�p�x
    protected float _eyeSpeed;
    protected float _subEyeSpeed;

    //���Ɋւ������
    [SerializeField] protected Transform _mouthTransform;
    protected float _mouthScaleeRadian;//���̑傫���ύX�����肷��l
    protected Vector3 _standardMouthScale;
    protected float _mouthSpeed;
    protected float _subMouthSpeed;

    //�̂̃p�[�e�B�N���Ɋւ������
    [SerializeField] protected ParticleSystem _particleSystem;
     protected float _frameHit;//�q�b�g���Ă���̃t���[��

    [SerializeField] protected Gradient _gradientOriginal;//���̐F
    [SerializeField] protected Gradient _gradientHit;//�q�b�g���̐F
    [SerializeField] protected Color _defeatColor;//�|�����Ƃ��̐F

    //���
    private bool _defeated;

    public void Start()
    {
        //�ڂƌ��̃��W�A��
        _eyeTransform[(int)Eye.Left].eulerAngles = new Vector3(0.0f, 0.0f, 45.0f);
        _eyeTransform[(int)Eye.Right].eulerAngles = new Vector3(0.0f, 0.0f, 225.0f);
        _subEyeSpeed = _eyeSpeed = 4.0f;
        _subEyeSpeed *= 0.1f;
        _mouthScaleeRadian = 0.0f;
        _subMouthSpeed = _mouthSpeed = 0.04f;
        _subMouthSpeed *= 0.1f;
        _standardMouthScale = _mouthTransform.localScale;

        //���̑��ϐ�
        _frameHit = 999.0f;//�q�b�g�t���[��
        _defeated = false;
    }


    public void FixedUpdate()
    {
        if (!_defeated)
        {
            HitAnimation();
        }
        Eyes();
        Mouth();
    }

    //�ڂ̓���
    protected void Eyes()
    {
        _eyeTransform[(int)Eye.Left].Rotate(0.0f, 0.0f, _eyeSpeed);
        _eyeTransform[(int)Eye.Right].Rotate(0.0f, 0.0f, -_eyeSpeed); 
    }

    //���̓���
    protected void Mouth()
    {
        //�ς��ς�
        float cosRange = 6.0f;
        float sinRangege = _standardMouthScale.y * 0.5f;

        _mouthScaleeRadian += _mouthSpeed;
        Vector3 addValue = Vector3.zero;
        addValue.y = Mathf.Sin(Mathf.Cos(_mouthScaleeRadian) * cosRange) * sinRangege;
        _mouthTransform.localScale = _standardMouthScale + addValue;
    }

    //�����蔻��
    protected void OnParticleCollision(GameObject other)
    {
        //�C���X�y�N�^�[���Ń��C���[�̐ݒ��
        Hit();
        SlowSpeed();
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
        if (_frameHit < 10.0f)
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

    private void ChangeColor()
    {
        //�F�ύX
        ParticleSystem.ColorOverLifetimeModule colorOverLifetime = _particleSystem.colorOverLifetime;
        colorOverLifetime.enabled = true;
        colorOverLifetime.color = _gradientHit;

        ParticleSystem.MainModule main = _particleSystem.main;
        main.startColor = _defeatColor;
        _frameHit = 0;
    }

    //---�A�j���[�^�[����̌Ăяo��---
    [SerializeField] public void ChangeColorByAnimator()
    {
        _defeated = true;
    }

    private void SlowSpeed()
    {
        _eyeSpeed -= _subEyeSpeed * Time.deltaTime;
        _eyeSpeed = Mathf.Clamp(_eyeSpeed, 0.0f, _eyeSpeed);
        _mouthSpeed -= _subMouthSpeed * Time.deltaTime;
        _mouthSpeed = Mathf.Clamp(_mouthSpeed, 0.0f, _mouthSpeed);
    }
}
