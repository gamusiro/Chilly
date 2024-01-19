using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_TutorialSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        gameObject.SetActive(CS_GameManager.GetOnTutorial);
    }
}
