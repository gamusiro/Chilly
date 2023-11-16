using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LastPlayer : MonoBehaviour
{ 
    //�Ăяo���p
    private bool _onJumpRamp = false;

    //��p�̃I�u�W�F�N�g�ɐG�ꂽ��jump����悤�ɂ���
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "JumpRamp")
        {
            _onJumpRamp = true;
        }
    }

    public bool GetOnJumpRamp()
    {
        return _onJumpRamp;
    }
}
