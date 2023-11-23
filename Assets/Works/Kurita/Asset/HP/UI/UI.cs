using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : ChangeMeshRenderAlpha
{
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
                if (ChangeAlpha(0.0f, 1.0f, 0.8f) >= 1.0f)
                {
                    _colorComplement = 0.0f;
                    _phase = Phase.Waiting;
                }
                break;
            case Phase.Waiting:
                _time += Time.deltaTime;
                if (_time > 4.0f)
                    _phase = Phase.Destroy;
                break;
            case Phase.Destroy://”Z’W‚ð”–‚­‚·‚é
                if (ChangeAlpha(1.0f, 0.0f, 0.8f) >= 1.0f)
                    Destroy(this.gameObject);
                break;
        }
    }
}
