using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisapperEyes : ChangeMeshRenderAlpha
{
    private enum Eye
    {
        Left,
        Right
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        Upd();
    }

    protected override void Upd()
    {
        switch (_phase)
        {
            case Phase.Appear://”Z’W‚ð”Z‚­‚·‚é
                if (ChangeAlpha(0.0f, 1.0f, 0.02f) >= 1.0f)
                {
                    _colorComplement = 0.0f;
                    _phase = Phase.Waiting;
                }
                break;
            case Phase.Waiting:
                break;
        }
    }

    public void SetAngle(Vector3 left, Vector3 right)
    {
        _meshRendererList[(int)Eye.Right].transform.eulerAngles = right;
        _meshRendererList[(int)Eye.Left].transform.eulerAngles = left;
    }
}
