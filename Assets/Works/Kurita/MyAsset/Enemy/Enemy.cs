using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseObject
{
    //�ڂ̓����ւ������
    [SerializeField] protected List<Transform> _transformEyes;
    [SerializeField] protected List<Transform> _transformEyesStandardPosition;//�ڂ̕W���ʒu

    protected List<float> _radianEyesRotate = new();//�ڂ̉�]�p�x
    protected List<float> _radianEyesCircleMotion = new();//�ڂ̉~�^���̊p�x

    //���Ɋւ������
    [SerializeField] protected Transform _transformMouth;
    [SerializeField] protected Transform _transformMouthStandardPosition;//���̕W���̈ʒu

    protected Vector3 _standardMouthScale = new Vector3();//���̕W���̑傫��
    protected float _radianMouthRescale = new();//���̑傫���ύX�̊p�x
    protected float _radianMouthRotate;//���̉�]�p�x
    protected float _radianMouthCircleMotion;//���̉~�^���̊p�x

    //�̂̃p�[�e�B�N���Ɋւ������
    [SerializeField] protected ParticleSystem _particleSystem;
     protected float _frameHit = new();//�q�b�g���Ă���̃t���[��

    [SerializeField] protected Gradient _gradientOriginal;//���̐F
    [SerializeField] protected Gradient _gradientHit;//�q�b�g���̐F

    public override void Initialized()
    {
        //�e�p�[�c�̊p�x�����߂�
        foreach (var transformEyes in _transformEyes)
        {
            float radian = Random.Range(0.0f, 2.0f) * Mathf.PI;
            _radianEyesRotate.Add(radian);
            _radianEyesCircleMotion.Add(radian);
        }
        _radianMouthRotate = Random.Range(0.0f, 2.0f) * Mathf.PI;
        _radianMouthCircleMotion = Random.Range(0.0f, 2.0f) * Mathf.PI;
        _radianMouthRescale = Random.Range(0.0f, 2.0f) * Mathf.PI;

        //���̑��ϐ�
        _standardMouthScale = _transformMouth.localScale;//���̕W���̑傫��
        _frameHit = 999.0f;//�q�b�g�t���[��
    }

    public override void Updated()
    {
        HitAnimation();
        Eyes();
        Mouth();
    }

    //�v���X�ƃ}�C�i�X�����݂ɏo�͂���B
    protected float AlternatingPlusAndMinus(int index)
    {
        if (index % 2 == 0)
            return 1.0f;
        else
            return -1.0f;
    }

    //�ڂ̓���
    protected void Eyes()
    {
        for (int i = 0; i < _transformEyes.Count; i++)
        {
            //�~�^��
            _radianEyesCircleMotion[i] +=  AddValue(AlternatingPlusAndMinus(i) * 0.04f, 20.0f);
            Vector3 addValue = new Vector3(Mathf.Sin(_radianEyesCircleMotion[i]), Mathf.Cos(_radianEyesCircleMotion[i]), 0.0f);
            addValue *= AddValue(0.5f,1.0f);
            _transformEyes[i].position = _transformEyesStandardPosition[i].position + addValue;

            //��]
            _transformEyes[i].Rotate(0.0f, 0.0f, AddValue(AlternatingPlusAndMinus(i) * 4.0f, 5.0f));
        }
    }

    //���̓���
    protected void Mouth()
    {
        //�~�^��
        _radianMouthCircleMotion += AddValue(0.01f, 1.0f);
        Vector3 addValue = new Vector3(Mathf.Sin(_radianMouthCircleMotion), Mathf.Cos(_radianMouthCircleMotion), 0.0f);
        addValue *= 0.5f;
        _transformMouth.position = _transformMouthStandardPosition.position + addValue;

        //��]
        _radianMouthRotate += AddValue(0.03f, 1.0f);
        Vector3 eulerAngle = _transformMouth.eulerAngles;
        eulerAngle.z = Mathf.Sin(_radianMouthRotate) * 13.0f;
        _transformMouth.eulerAngles = eulerAngle;

        //�ς��ς�
        _radianMouthRescale += AddValue(0.01f, 6.0f);
        Vector3 addScale = Vector3.zero;
        addScale.y = Mathf.Sin(Mathf.Cos(_radianMouthRescale * 3.0f) * 6.0f) * 4.0f;
        _transformMouth.localScale = _standardMouthScale + addScale;
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
        Gradient gradient = new();
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

    protected float AddValue(float value,float multiple)
    {
        if (_frameHit < 10.0f)
            return value * multiple;
        else
            return value;
    }
}
