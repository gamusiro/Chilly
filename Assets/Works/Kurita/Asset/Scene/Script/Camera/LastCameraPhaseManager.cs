using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LastCameraPhaseManager : AbstructCameraPhaseManager
{
    //フェーズの設定
    protected new enum Phase { Wait,Wait2, BellAttackaaaa, Max };
    private Phase _phaseIndex = Phase.Wait;

    void Start()
    {
        Init();
    }

    void Update()
    {
        switch (_phaseIndex)
        {
            case Phase.Wait:
                if (NextPhase(2.0f))
                {
                    _phaseIndex++;
                    NextCamera();
                }
                    break;
            case Phase.Wait2:
                if (NextPhase(0.0f))
                {
                    _phaseIndex++;
                    NextCamera();
                }
                break;
            case Phase.BellAttackaaaa:
                if (NextPhase(0.0f))
                {
                    _phaseIndex++;
                    NextCamera();
                }
                break;
        }
    }
}


