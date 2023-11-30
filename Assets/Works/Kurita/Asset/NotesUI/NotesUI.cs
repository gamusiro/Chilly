using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NotesUI : MonoBehaviour
{
    [NonSerialized] public float Speed = 1.0f;

    private void Start()
    {
        
    }

    private void FixedUpdate()
    { 
        Vector3 position = this.transform.position;
        position.x += Speed * Time.deltaTime;
        this.transform.position = position;

        Debug.Log("ƒ|ƒWƒVƒ‡ƒ“"+this.transform.position);
    }
}
