using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFloorLane : MonoBehaviour
{
    private void Update()
    {
        //子オブジェクトが無くなったら消す
        if (this.gameObject.transform.childCount <= 0) 
            Destroy(this.gameObject);
    }
}
