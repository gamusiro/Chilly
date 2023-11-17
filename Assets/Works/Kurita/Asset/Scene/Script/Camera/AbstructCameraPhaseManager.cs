using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public abstract class AbstructCameraPhaseManager : AbstructBasePhaseManager
{
    //バーチャルカメラの設定
    [SerializeField] protected List<CinemachineVirtualCamera> _virtualCamera = new List<CinemachineVirtualCamera>();
    protected int _cameraIndex;

    //フェーズの設定
    protected new enum Phase { Stay,ZoomIn,Max };
    protected new Phase _phaseIndex = Phase.Stay;

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
    protected new void Init()
    {
        //カメラの優先度をリセットする
        foreach (var virtualCamera in _virtualCamera) { virtualCamera.Priority = 0; }
        _cameraIndex = 0;
        _virtualCamera[_cameraIndex].Priority = 1;
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

    public GameObject GetCurCamera()
    {
        return _virtualCamera[_cameraIndex].gameObject;
    }
}
