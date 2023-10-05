using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseObject
{
    //�ڂ̓����ւ������
    [SerializeField] private List<Transform> _transformEyes;
    [SerializeField] List<Transform> _transformEyesStandardPosition;//�ڂ̕W���ʒu

    private List<float> _radianEyesRotate = new();//�ڂ̉�]�p�x
    private List<float> _radianEyesCircleMotion = new();//�ڂ̉~�^���̊p�x

    //���Ɋւ������
    [SerializeField] private Transform _transformMouth;
    [SerializeField] Transform _transformMouthStandardPosition;//���̕W���̈ʒu

    private Vector3 _standardMouthScale = new Vector3();//���̕W���̑傫��
    private float _radianMouthRescale = new();//���̑傫���ύX�̊p�x
    private float _radianMouthRotate;//���̉�]�p�x
    private float _radianMouthCircleMotion;//���̉~�^���̊p�x

    //�̂̃p�[�e�B�N���Ɋւ������
    [SerializeField] private ParticleSystem _particleSystem;
     private float _frameHit = new();//�q�b�g���Ă���̃t���[��

    [SerializeField] private Gradient _gradientOriginal;//���̐F
    [SerializeField] private Gradient _gradientHit;//�q�b�g���̐F

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
        Hit();//�f�o�b�O�p
    }

    public override void Updated()
    {
        HitAnimation();
        Eyes();
        Mouth();
    }

    //�v���X�ƃ}�C�i�X�����݂ɏo�͂���B
    private float AlternatingPlusAndMinus(int index)
    {
        if (index % 2 == 0)
            return 1.0f;
        else
            return -1.0f;
    }

    //�ڂ̓���
    private void Eyes()
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
    private void Mouth()
    {
        //�~�^��
        _radianMouthCircleMotion += AddValue(0.01f, 1.0f);
        Vector3 addValue = new Vector3(Mathf.Sin(_radianMouthCircleMotion), Mathf.Cos(_radianMouthCircleMotion), 0.0f);
        addValue *= 0.5f;
        _transformMouth.position = _transformMouthStandardPosition.position + addValue;

        //��]
        _radianMouthRotate += AddValue(0.03f, 1.0f);
        _transformMouth.eulerAngles = new Vector3(0.0f, 0.0f, Mathf.Sin(_radianMouthRotate) * 13.0f);

        //�ς��ς�
        _radianMouthRescale += AddValue(0.01f, 6.0f);
        Vector3 addScale = Vector3.zero;
        addScale.y = Mathf.Sin(Mathf.Cos(_radianMouthRescale * 3.0f) * 6.0f) * 4.0f;
        _transformMouth.localScale = _standardMouthScale + addScale;
    }

    //�����蔻��
    private void OnParticleCollision(GameObject other)
    {
        //�C���X�y�N�^�[���Ń��C���[�̐ݒ��
           Hit();
    }

    //�U�����ꂽ����
    private void Hit()
    {
        _frameHit = 0;
    }

    private void HitAnimation()
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

    private float AddValue(float value,float multiple)
    {
        if (_frameHit < 10.0f)
            return value * multiple;
        else
            return value;
    }

}
