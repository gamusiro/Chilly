using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Starter : MonoBehaviour
{
    [SerializeField]
    GameObject m_MoveObject;

    [SerializeField]
    AudioSource m_sourceVoice;


    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(GameStart), 3.0f);
    }

    void GameStart()
    {
        m_MoveObject.GetComponent<CS_MoveController>().MoveStart();
        m_sourceVoice.Play();
    }
}
