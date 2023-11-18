using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public abstract class AbstructBasePhaseManager : MonoBehaviour
{
    //フェーズの設定
    protected enum Phase { Max };
    private Phase _phaseIndex = Phase.Max;
    protected float _time;

    //初期化
    protected void Init()
    {
        //フェーズを初期化する
        _phaseIndex = 0;
        _time = 0.0f;
    }

    //n秒後に次のフェーズに遷る
    protected bool NextPhase(float transTime)
    {
        _time+=Time.deltaTime;
        if (_time < transTime)//遷移時間に達していなければ終了
            return false;

        return true;
    }
}
