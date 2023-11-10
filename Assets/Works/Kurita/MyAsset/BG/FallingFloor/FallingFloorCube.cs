using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFloorCube : ChangeMeshRenderAlpha
{
    private Rigidbody _rigidBody;

    private void Start()
    {

        _rigidBody = GetComponent<Rigidbody>();
        Init();
    }

    private void Update()
    {
        switch (_phase)
        {
            case Phase.Appear://濃淡を濃くする
                if (ChangeAlpha(0.0f, 0.5f,0.8f) >= 1.0f) 
                {
                    _colorComplement = 0.0f;
                    _phase = Phase.Waiting;
                }
                break;
            case Phase.Waiting:
                _time += Time.deltaTime;
                if (_time > 1.0f)
                    _phase = Phase.Fall;
                break;
            case Phase.Fall:
                _rigidBody.useGravity = true;
                _rigidBody.velocity += new Vector3(Random.Range(-20.0f,20.0f), 0.0f, Random.Range(-20.0f, 20.0f));
                _phase = Phase.Destroy;
                break;
            case Phase.Destroy://濃淡を薄くする
<<<<<<< HEAD
                if (ChangeAlpha(0.5f, 0.0f,0.8f) >= 1.0f)
=======
                if (ChangeAlpha(0.5f, 0.0f) >= 1.0f)
                {
                    //Debug.Log("オブジェクトが消えました");
>>>>>>> gamusiro_09
                    Destroy(this.gameObject);
                break;
        }
    }
}
