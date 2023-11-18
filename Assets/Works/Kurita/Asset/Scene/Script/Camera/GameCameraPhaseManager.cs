using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameCameraPhaseManager : AbstructCameraPhaseManager
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

    //n秒後に次のフェーズに遷る
    protected bool NextPhase(float transTime)
    {
        // += Time.deltaTime; 個々を変更
        if (_time < transTime)//遷移時間に達していなければ終了
            return false;

        return true;
    }
}
