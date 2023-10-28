using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Shadow : MonoBehaviour
{
    float m_work;

    Vector3 startedScale;
    Vector3 targetScale;

    bool m_run;

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        startedScale = transform.localScale;
        targetScale = new Vector3(1.5f, 1.5f, 1.5f);
        m_work = 0.0f;

        m_run = false;
    }

    /// <summary>
    /// çXêVèàóù
    /// </summary>
    void Update()
    {
        if(m_run)
        {
            m_work += 1.0f * Time.deltaTime;
            transform.localScale = Vector3.Lerp(startedScale, targetScale, m_work);
        }
    }

    public void SetWork()
    {
        m_run = true;
        Destroy(gameObject, 1.0f);
    }
}
