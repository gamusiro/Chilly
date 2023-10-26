using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movie1Fase1 : FaseParent
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _cameraTransform;
    //[SerializeField ]private PostProcessVolume _postProcessVolume;

    //カメラのアングル操作
    private float _speed;
    private float _curPoint;
    private float _startAngle;
    private float _endAngle;

    protected void Awake()
    {
        //カメラの優先度設定
        _cinemachineVirtualCamera.Priority = 1;

        //フェード
        //_fadeImageAlphaLerpSO = new LerpSO();
        //_fadeImageAlphaLerpSO.StartValue = 1.0f;
        //_fadeImageAlphaLerpSO.EndValue = 0.0f;
        //_fadeImageAlphaLerpSO.CurrentPoint = 0.9f;
        //_fadeImageAlphaLerpSO.Speed = 0.05f;
         ChangeAlpha(_fadeImage, 1.0f);
        Debug.Log("呼び出される");


        _speed = 0.9f;
        _curPoint = 0.0f;
        _startAngle = -45.0f;
        _endAngle = 0.0f;
    }

    protected void OnDestroy()
    {
        //カメラの優先度設定
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