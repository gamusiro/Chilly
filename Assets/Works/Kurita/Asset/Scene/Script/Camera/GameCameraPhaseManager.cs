using Cysharp.Threading.Tasks;
using System;

public class GameCameraPhaseManager : CameraPhaseManager
{
    private void Start()
    {
        //カメラの優先度をリセットする
        foreach (var virtualCamera in _virtualCamera) { virtualCamera.Priority = 0; }
        _cameraIndex = 0;
        _virtualCamera[_cameraIndex].Priority = 1;

        //カメラの遷移処理
        while (_cameraIndex < _transTimeList.Count)
        {
            //開始から何秒で切り替わるか
            if(CS_AudioManager.Instance.TimeBGM < _transTimeList[_cameraIndex])
                NextCamera();      
        }
    }
}
