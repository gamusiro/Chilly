using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movie1Fase1 : FaseParent
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _cameraTransform;
    //[SerializeField ]private PostProcessVolume _postProcessVolume;

    //�J�����̃A���O������
    private float _speed;
    private float _curPoint;
    private float _startAngle;
    private float _endAngle;

    protected void Awake()
    {
        //�J�����̗D��x�ݒ�
        _cinemachineVirtualCamera.Priority = 1;

        //�t�F�[�h
        //_fadeImageAlphaLerpSO = new LerpSO();
        //_fadeImageAlphaLerpSO.StartValue = 1.0f;
        //_fadeImageAlphaLerpSO.EndValue = 0.0f;
        //_fadeImageAlphaLerpSO.CurrentPoint = 0.9f;
        //_fadeImageAlphaLerpSO.Speed = 0.05f;
         ChangeAlpha(_fadeImage, 1.0f);
        Debug.Log("�Ăяo�����");


        _speed = 0.9f;
        _curPoint = 0.0f;
        _startAngle = -45.0f;
        _endAngle = 0.0f;
    }

    protected void OnDestroy()
    {
        //�J�����̗D��x�ݒ�
        _cinemachineVirtualCamera.Priority = 0;
    }

    private void Update()
    {
        LerpCameraAngle();
      //  FadeOut();
    }

    private void LerpCameraAngle()
    {
        _curPoint += Time.deltaTime * _speed;
        Vector3 angle = _cameraTransform.eulerAngles;
        angle.x = Mathf.Lerp(_startAngle, _endAngle, _curPoint);
        _cameraTransform.eulerAngles = angle;
    }
    public override bool InstantaneousTransition()
    {
        return true;
    }
}