using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_MoveControl : MonoBehaviour
{
    [SerializeField]
    GameObject m_playerObject;

    [SerializeField]
    List<GameObject> m_objectList = new List<GameObject>();

    [SerializeField]
    float m_moveVel;


    /// <summary>
    /// çXêVèàóù
    /// </summary>
    void Update()
    {
        Vector3 playerDir = m_playerObject.transform.forward * -1.0f;

        m_playerObject.transform.position += playerDir * m_moveVel * Time.deltaTime;

        foreach (GameObject o in m_objectList)
            o.transform.position += playerDir * m_moveVel * Time.deltaTime;
    }
}
