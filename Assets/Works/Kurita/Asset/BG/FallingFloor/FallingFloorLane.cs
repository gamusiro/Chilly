using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFloorLane : MonoBehaviour
{
    private void Update()
    {
        //�q�I�u�W�F�N�g�������Ȃ��������
        if (this.gameObject.transform.childCount <= 0) 
            Destroy(this.gameObject);
    }
}
