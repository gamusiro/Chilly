using Cinemachine;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameCameraPhaseManager : AbstractCameraPhaseManager
{
    [SerializeField] private CS_Player _player;
    private float _elapsedTime = 0.0f;

    private async void Start()
    {
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        // カメラのアセットを設定する
        string audioName = CS_LoadNotesFile.GetFolderName();
        _transTimeList = GetAssets(audioName);

        //カメラの優先度をリセットする
        foreach (var virtualCamera in _virtualCamera) { virtualCamera.Priority = 0; }
        _cameraIndex = 0;
        _virtualCamera[_cameraIndex].Priority = 1;

        //カメラの遷移処理
        while (_cameraIndex < _virtualCamera.Count - 1) 
        {
            //開始から何秒で切り替わるか
            _elapsedTime = _transTimeList[_cameraIndex];
            await UniTask.WaitUntil(() => CS_AudioManager.Instance.TimeBGM > _elapsedTime, cancellationToken: token);
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
        _player.SetUsingCamera();
    }

    /// <summary>
    /// カメラの秒数設定用(直打ち実装)
    /// </summary>
    /// <param name="audioName"></param>
    /// <returns></returns>
    private List<float> GetAssets(string audioName)
    {
        Dictionary<string, List<float>> pairs = new Dictionary<string, List<float>>();
        pairs.Add("WeMadeIt", new List<float> { 1, 73, 126 });
        pairs.Add("IWillBeHere", new List<float> { 1, 68, 132 });

        if(!pairs.ContainsKey(audioName))
        {
            Debug.LogError(audioName + "のデータが見つかりません");
            return null;
        }

        return pairs[audioName];
    }
}