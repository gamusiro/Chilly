using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class GameCameraPhaseManager : CameraPhaseManager
{
    private float _elapsedTime = 0.0f;

    private async void Start()
    {
        //�J�����̗D��x�����Z�b�g����
        foreach (var virtualCamera in _virtualCamera) { virtualCamera.Priority = 0; }
        _cameraIndex = 0;
        _virtualCamera[_cameraIndex].Priority = 1;

        //�J�����̑J�ڏ���
        while (_cameraIndex + 1 < _virtualCamera.Count) 
        {
            //�J�n���牽�b�Ő؂�ւ�邩
            _elapsedTime = _transTimeList[_cameraIndex];
            await UniTask.WaitUntil(() => CS_AudioManager.Instance.TimeBGM > _elapsedTime);
                NextCamera();      
        }
    }

    //�J������؂�ւ���
    protected new void NextCamera()
    {
        _virtualCamera[_cameraIndex].Priority = 0;
        _cameraIndex++;
        _virtualCamera[_cameraIndex].Priority = 1;

        // ����J�����̐ݒ�
        CS_MoveController.GetObject("Player").GetComponent<CS_Player>().SetUsingCamera();
    }
}