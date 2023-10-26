using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(menuName = "LerpSO")]
public class LerpSO : ScriptableObject
{
    public float StartValue = 0.0f;
    public float EndValue = 0.0f;
    [NonSerialized] public float CurrentPoint = 0.0f;
    public float Speed = 0.0f;
}
