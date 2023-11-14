using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LastCameraManager : AbstructAnimationManager
{
    //フェーズの設定
    protected new enum Phase { Wait,BellAttack, BellAttackaaaa, Max };
    protected new Phase _phaseIndex = Phase.Wait;

    void Start()
    {
        Init();
    }

    void Update()
    {
        switch (_phaseIndex)
        {
            case Phase.Wait:
                if (NextPhase(4.0f))
                    NextCamera();
                break;
            case Phase.BellAttack:
                if (NextPhase(0.0f))
                    NextCamera();
                break;
        }
    }
}
