using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameCameraPhaseManager : AbstructCameraPhaseManager
{
    [SerializeField]
    CinemachineBrain brain;

    //フェーズの設定
    protected new enum Phase { Stay, Front, Back, Max };
    protected new Phase _phaseIndex = Phase.Stay;

    void Start()
    {
        Init();
    }

    void Update()
    {
        var blend = brain.ActiveBlend;
        Debug.Log(brain.ActiveVirtualCamera.Name);

        switch (_phaseIndex)
        {
            case Phase.Stay:        // 初期状態のカメラ
                if (NextPhase(1.0f))
                {
                    NextCamera();
                    _phaseIndex = Phase.Front;
                }
                    
                break;
            case Phase.Front:       // プレイヤーを前から見るカメラ
                if (NextPhase(73.0f))   // 1分13秒
                {
                    NextCamera();
                    CS_MoveController.GetObject("Player").GetComponent<CS_Player>().SetUsingCamera();
                    _phaseIndex = Phase.Back;
                }
                break;
            case Phase.Back:       // プレイヤーを後ろから見るカメラ
                if (NextPhase(126.0f))  // 2分06秒
                {
                    NextCamera();
                    CS_MoveController.GetObject("Player").GetComponent<CS_Player>().SetUsingCamera();
                }
                break;
        }
    }

    //n秒後に次のフェーズに遷る
    protected bool NextPhase(float transTime)
    {
        if (CS_AudioManager.Instance.TimeBGM < transTime)//遷移時間に達していなければ終了
            return false;

        return true;
    }
}
