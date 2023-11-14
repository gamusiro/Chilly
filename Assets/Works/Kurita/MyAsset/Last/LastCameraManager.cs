using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LastCameraManager : CameraManager
{
    //フェーズの設定
    protected new enum Phase { BellAttack, BellAttackaaaa, Max };
    protected new Phase _phaseIndex = Phase.BellAttack;

    void Start()
    {
        Init();
    }

    void Update()
    {
        switch (_phaseIndex)
        {
            case Phase.BellAttack:
                Debug.Log(_audioSource.time);
                if (NextPhase(0.0f))
                    NextCamera();
                break;
        }
    }
}
