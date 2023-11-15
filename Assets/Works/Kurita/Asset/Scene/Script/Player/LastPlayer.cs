using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LastPlayer : MonoBehaviour
{ 
    //呼び出し用
    private bool _onJumpRamp = false;

    //専用のオブジェクトに触れたらjumpするようにする
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
