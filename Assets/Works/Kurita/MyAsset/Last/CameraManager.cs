using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public abstract class CameraManager : MonoBehaviour
{
    //バーチャルカメラの設定
    [SerializeField] protected List<CinemachineVirtualCamera> _virtualCamera = new List<CinemachineVirtualCamera>();
    protected int _cameraIndex;

    //フェーズの設定
    protected enum Phase { Stay,ZoomIn,Max };
    protected Phase _phaseIndex = Phase.Stay;

    //BGM
    protected AudioSource _audioSource;

    void Start()
    {
        Init();
    }

    void Update()
    {
        switch(_phaseIndex)
        {
            case Phase.Stay:
                if (NextPhase(50.0f))
                    NextCamera();
                break;
            case Phase.ZoomIn:
                if (NextPhase(1.0f))
                    NextCamera();
                break;
        }
    }

    //初期化
    protected void Init()
    {
        //カメラの優先度をリセットする
        foreach (var virtualCamera in _virtualCamera) { virtualCamera.Priority = 0; }
        _cameraIndex = 0;
        _virtualCamera[_cameraIndex].Priority = 1;

        //フェーズを初期化する
        _phaseIndex = 0;

        //BGMの情報を取得する
        _audioSource = CS_AudioManager.Instance.GetAudioSource("MainBGM");
    }

    //n秒後に次のフェーズに遷る
    protected bool NextPhase(float transTime)
    {
        if (_audioSource.time < transTime)//遷移時間に達していなければ終了
            return false;

        _phaseIndex++;
        return true;
    }

    //カメラを切り替える
    protected void NextCamera()
    {
        if (_cameraIndex + 1 >= _virtualCamera.Count) 
            return;

        _virtualCamera[_cameraIndex].Priority = 0;
        _cameraIndex++;
        _virtualCamera[_cameraIndex].Priority = 1;
    }
}
