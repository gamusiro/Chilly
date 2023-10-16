using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : BaseObject
{
    [SerializeField] private string _nextScene;

    public override void Updated()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            SceneManager.LoadScene(_nextScene);
    }
}
