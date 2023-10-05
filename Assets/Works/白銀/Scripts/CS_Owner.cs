using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Owner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 curPos = transform.position;
        Vector3 nexPos = curPos;

        nexPos.z -= 1.0f;

        transform.position = nexPos;
    }
}
