using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class GameCameraPhaseManager : CameraPhaseManager
{
    private async void Start()
    {
        //カメラの優先度をリセットする
        foreach (var virtualCamera in _virtualCamera) { virtualCamera.Priority = 0; }
        _cameraIndex = 0;
        _virtualCamera[_cameraIndex].Priority = 1;

        //カメラの遷移処理
        while (_cameraIndex < _virtualCamera.Count)
        {
            //開始から何秒で切り替わるか
            await UniTask.WaitUntil(() => CS_AudioManager.Instance.TimeBGM > _transTimeList[_cameraIndex]);
                NextCamera();      
        }
    }
}