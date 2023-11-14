using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MainGameCameraManager : AbstructCameraPhaseManager
{
    //フェーズの設定
    protected new enum Phase { Stay, Up, Max };
    protected new Phase _phaseIndex = Phase.Stay;

    void Start()
    {
        Init();
    }

    void Update()
    {
        switch (_phaseIndex)
        {
            case Phase.Stay:
                if (NextPhase(1.0f))
                    NextCamera();
                break;
            case Phase.Up:
                if (NextPhase(1.0f))
                    NextCamera();
                break;
        }
    }
}
