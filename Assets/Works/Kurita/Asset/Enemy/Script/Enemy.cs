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

    //���Ɋւ������
    [SerializeField] protected Transform _mouthTransform;
    protected float _MouthScaleeRadian;//���̑傫���ύX�����肷��l
    protected Vector3 _standardMouthScale;

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
        _MouthScaleeRadian = 0.0f;
        _standardMouthScale = _mouthTransform.localScale;

        //���̑��ϐ�
        _frameHit = 999.0f;//�q�b�g�t���[��
        _defeated = false;
    }

    public void Update()
    {
        if (!_defeated)
        {
            HitAnimation();
        }
        Eyes();
        Mouth();
        //ChangeColor();
    }


    //�ڂ̓���
    protected void Eyes()
    {
        float speed = 2.0f;
        _eyeTransform[(int)Eye.Left].Rotate(0.0f, 0.0f, speed);
        _eyeTransform[(int)Eye.Right].Rotate(0.0f, 0.0f, -speed); 
    }

    //���̓���
    protected void Mouth()
    {
        //�ς��ς�
        float speed = 0.04f;
        float cosRange = 6.0f;
        float sinRangege = _standardMouthScale.y * 0.5f;

        _MouthScaleeRadian += speed;
        Vector3 addValue = Vector3.zero;
        addValue.y = Mathf.Sin(Mathf.Cos(_MouthScaleeRadian) * cosRange) * sinRangege;
        _mouthTransform.localScale = _standardMouthScale + addValue;
    }

    //�����蔻��
    protected void OnParticleCollision(GameObject other)
    {
        //�C���X�y�N�^�[���Ń��C���[�̐ݒ��
           Hit();
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
}
