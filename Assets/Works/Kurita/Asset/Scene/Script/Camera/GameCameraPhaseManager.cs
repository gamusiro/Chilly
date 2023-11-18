using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameCameraPhaseManager : AbstructCameraPhaseManager
{
    [SerializeField]
    CinemachineBrain brain;

    //�t�F�[�Y�̐ݒ�
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
            case Phase.Stay:        // ������Ԃ̃J����
                if (NextPhase(1.0f))
                {
                    NextCamera();
                    _phaseIndex = Phase.Front;
                }
                    
                break;
            case Phase.Front:       // �v���C���[��O���猩��J����
                if (NextPhase(73.0f))   // 1��13�b
                {
                    NextCamera();
                    CS_MoveController.GetObject("Player").GetComponent<CS_Player>().SetUsingCamera();
                    _phaseIndex = Phase.Back;
                }
                break;
            case Phase.Back:       // �v���C���[����납�猩��J����
                if (NextPhase(126.0f))  // 2��06�b
                {
                    NextCamera();
                    CS_MoveController.GetObject("Player").GetComponent<CS_Player>().SetUsingCamera();
                }
                break;
        }
    }

    //n�b��Ɏ��̃t�F�[�Y�ɑJ��
    protected bool NextPhase(float transTime)
    {
        if (CS_AudioManager.Instance.TimeBGM < transTime)//�J�ڎ��ԂɒB���Ă��Ȃ���ΏI��
            return false;

        return true;
    }
}
