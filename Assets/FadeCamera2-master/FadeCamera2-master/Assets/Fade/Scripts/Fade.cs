using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class Fade : MonoBehaviour
{
    [SerializeField] FadeImage fade;
    [SerializeField] bool startFade;

    STATE m_state;

    public enum STATE
    {
        NONE,
        IN,
        OUT
    }

    void Awake()
    {
        Init();
        fade.Range = cutoutRange;
    }

    float cutoutRange;

    void Init()
    {
        if (startFade)
        {
            cutoutRange = 1;
        }
    }

    //void OnValidate()
    //{
    //    Init();
    //    fade.Range = cutoutRange;
    //}

    IEnumerator FadeInCoroutine(float time, System.Action action)
    {
        float endTime = Time.timeSinceLevelLoad + time * (cutoutRange);

        var endFrame = new WaitForEndOfFrame();

        while (Time.timeSinceLevelLoad <= endTime)
        {
            cutoutRange = (endTime - Time.timeSinceLevelLoad) / time;
            fade.Range = cutoutRange;
            yield return endFrame;
        }
        cutoutRange = 0;
        fade.Range = cutoutRange;
        m_state = STATE.NONE;

        if (action != null)
        {
            action();
        }
    }

    IEnumerator FadeOutCoroutine(float time, System.Action action)
    {
        float endTime = Time.timeSinceLevelLoad + time * (1 - cutoutRange);

        var endFrame = new WaitForEndOfFrame();

        while (Time.timeSinceLevelLoad <= endTime)
        {
            cutoutRange = 1 - ((endTime - Time.timeSinceLevelLoad) / time);
            fade.Range = cutoutRange;
            yield return endFrame;
        }
        cutoutRange = 1;
        fade.Range = cutoutRange;
        m_state = STATE.NONE;

        if (action != null)
        {
            action();
        }
    }

    public Coroutine FadeIn(float time, System.Action action)
    {
        m_state = STATE.IN;
        

        StopAllCoroutines();
        if (fade == null)
        {
            Debug.LogError("フェードがないよ～:" + time);
            return null;
        }
            
        //Debug.Log("スタートフェード");
        return StartCoroutine(FadeInCoroutine(time, action));
    }

    public Coroutine FadeIn(float time)
    {
        return FadeIn(time, null);
    }

    public Coroutine FadeOut(float time, System.Action action)
    {
        m_state = STATE.OUT;

        StopAllCoroutines();
        return StartCoroutine(FadeOutCoroutine(time, action));
    }

    public Coroutine FadeOut(float time)
    {
        return FadeOut(time, null);
    }

    public STATE GetState()
    {
        return m_state;
    }

    public float GetRange()
    {
        if (fade == null) 
            return 0.0f;

        //Debug.Log("レンジ:" + fade.Range);
        return fade.Range;
    }

    public void OnDestroy()
    {
        StopAllCoroutines();
    }
}