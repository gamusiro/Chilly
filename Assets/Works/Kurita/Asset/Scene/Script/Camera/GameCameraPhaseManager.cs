using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class GameCameraPhaseManager : CameraPhaseManager
{
    private float _elapsedTime = 0.0f;

    private async void Start()
    {
        //カメラの優先度をリセットする
        foreach (var virtualCamera in _virtualCamera) { virtualCamera.Priority = 0; }
        _cameraIndex = 0;
        _virtualCamera[_cameraIndex].Priority = 1;

        //カメラの遷移処理
        while (_cameraIndex + 1 < _virtualCamera.Count) 
        {
            //開始から何秒で切り替わるか
            _elapsedTime = _transTimeList[_cameraIndex];
            await UniTask.WaitUntil(() => CS_AudioManager.Instance.TimeBGM > _elapsedTime);
                NextCamera();      
        }
    }

    //カメラを切り替える
    protected new void NextCamera()
    {
        _virtualCamera[_cameraIndex].Priority = 0;
        _cameraIndex++;
        _virtualCamera[_cameraIndex].Priority = 1;

        // 操作カメラの設定
        CS_MoveController.GetObject("Player").GetComponent<CS_Player>().SetUsingCamera();
    }
}