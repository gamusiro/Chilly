using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemy : Enemy
{
    [Header("�X�|�[���n�_")]
    [SerializeField] private Transform _spawnTransform;
    [Header("�ړ��J�n����(�X�|�[�����Ă���v��)")]
    [SerializeField] private Transform _startTime;
    [Header("���݂��鎞��(�ړ��J�n���Ă���v��)")]
    [SerializeField] private Transform _lifeTime;


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

    private new void Eyes()
    {
        for (int i = 0; i < _transformEyes.Count; i++)
        {
            //�~�^��
            _radianEyesCircleMotion[i] += AddValue(AlternatingPlusAndMinus(i) * 0.04f, 20.0f);
            Vector3 addValue = new Vector3(Mathf.Sin(_radianEyesCircleMotion[i]), Mathf.Cos(_radianEyesCircleMotion[i]), 0.0f);
            addValue *= AddValue(0.5f, 1.0f);
            _transformEyes[i].position = _transformEyesStandardPosition[i].position + addValue;
        }
    }
}
