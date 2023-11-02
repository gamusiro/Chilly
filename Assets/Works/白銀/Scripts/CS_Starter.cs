using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Starter : MonoBehaviour
{
    [SerializeField]
    GameObject m_MoveObject;

    // Start is called before the first frame update
    void Start()
    {
        // Ç±Ç±Ç≈âΩïbÇ©ë“ÇΩÇ»Ç¢Ç∆ÅAéÄÇ 
        Invoke(nameof(GameStart), 1.0f);
        //GameStart();
    }

    void GameStart()
    {
        CS_MoveController.Instance.MoveStart();
        CS_AudioManager.Instance.PlayAudio("MainBGM");
        Destroy(gameObject);
    }
}
