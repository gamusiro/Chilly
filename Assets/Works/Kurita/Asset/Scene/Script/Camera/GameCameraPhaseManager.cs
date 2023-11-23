using Cysharp.Threading.Tasks;
using System;

public class GameCameraPhaseManager : CameraPhaseManager
{
    private void Start()
    {
        //�J�����̗D��x�����Z�b�g����
        foreach (var virtualCamera in _virtualCamera) { virtualCamera.Priority = 0; }
        _cameraIndex = 0;
        _virtualCamera[_cameraIndex].Priority = 1;

        //�J�����̑J�ڏ���
        while (_cameraIndex < _transTimeList.Count)
        {
            //�J�n���牽�b�Ő؂�ւ�邩
            if(CS_AudioManager.Instance.TimeBGM < _transTimeList[_cameraIndex])
                NextCamera();      
        }
    }
}
