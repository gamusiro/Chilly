using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public abstract class AbstractBasePhaseManager : MonoBehaviour
{
    //‘JˆÚŠJŽnŽžŠÔ
    [SerializeField] protected List<float> _transTimeList;
    protected float _transTime = 0.0f;
}
