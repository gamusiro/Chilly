using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CS_HeartPiece : MonoBehaviour
{
    [SerializeReference]
    GameObject m_enemyObject;

    [SerializeReference]
    GameObject m_cameraObject;

    Vector3 m_enemyPos;
    Vector3 m_camPos;
    Vector3 m_curPos;
    float m_work;
    bool m_get;

    // Start is called before the first frame update
    void Start()
    {
        m_enemyPos = m_enemyObject.transform.position;
        m_camPos = m_cameraObject.transform.position;
        m_curPos = gameObject.transform.position;
        m_get = false;
        m_work = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_get)
        {
            m_work += 1.0f * Time.deltaTime;

            // ÉxÉWÉGã»ê¸
            Vector3 a = Vector3.Lerp(m_curPos, m_camPos, m_work);
            Vector3 b = Vector3.Lerp(m_camPos, m_enemyPos, m_work);
            gameObject.transform.position = Vector3.Lerp(a, b, m_work);
        }
    }

    /// <summary>
    /// è’ìÀÇµÇΩÇÁ
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        m_get = true;
    }
}
