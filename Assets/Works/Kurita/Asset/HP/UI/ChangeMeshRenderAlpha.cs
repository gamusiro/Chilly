using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChangeMeshRenderAlpha : MonoBehaviour
{
    protected enum Phase
    {
        Appear,
        Waiting,
        Fall,
        Destroy,
    }

    [SerializeField] protected List<MeshRenderer> _meshRendererList = new List<MeshRenderer>();
    protected Phase _phase;
    protected float _colorComplement;
    protected float _time;

    protected void Init()
    {
        _phase = Phase.Appear;
        _colorComplement = 0.0f;
        foreach (var renderer in _meshRendererList)
        {
            renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }
        _time = 0.0f;
    }

    protected virtual void Upd()
    {
        
    }

    protected float ChangeAlpha(float from, float to,float speed)
    {
        _colorComplement += Time.deltaTime * speed; ;

        //F‚Ì•ÏX
        Color fromColor = new Color(1.0f, 1.0f, 1.0f, from);
        Color toColor = new Color(1.0f, 1.0f, 1.0f, to);

        if (_colorComplement >= 1.0f)
            _colorComplement = 1.0f;

        foreach (var renderer in _meshRendererList)
        {
            renderer.material.color = Color.Lerp(fromColor, toColor, _colorComplement);
        }

        return _colorComplement;
    }
}
