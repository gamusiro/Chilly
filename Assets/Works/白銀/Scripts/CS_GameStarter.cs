using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_GameStarter : MonoBehaviour
{
    /// <summary>
    /// ����������
    /// </summary>
    void Start()
    {
        Invoke(nameof(GameStart), 1.0f);
        //GameStart();
    }

    /// <summary>
    /// �Q�[���X�^�[�g
    /// </summary>
    void GameStart()
    {
        CS_AudioManager.Instance.PlayAudio("GameAudio");
        CS_MoveController.Instance.MoveStart();
        Destroy(gameObject);
    }
}
