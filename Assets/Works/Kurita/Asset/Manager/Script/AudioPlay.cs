using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    void Start()
    {
        CS_AudioManager.Instance.PlayAudio("GameAudio",true);
        CS_MoveController.MoveStart();
        Destroy(gameObject);
    }
}
