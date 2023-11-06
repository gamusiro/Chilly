using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFloorCube : MonoBehaviour
{
    private enum Phase
    {
        Appear,
        Waiting,
        Destroy,
    }

    [SerializeField] private List<MeshRenderer> _meshRendererList = new List<MeshRenderer>();
    private Phase _phase;
    private Rigidbody _rigidBody;
    private float _colorComplement;

    private void Start()
    {
        _phase = Phase.Appear;
        _rigidBody = GetComponent<Rigidbody>();
        _colorComplement = 0.0f;
        foreach (var renderer in _meshRendererList)
        {
            renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }
    }

    private void Update()
    {
        switch (_phase)
        {
            case Phase.Appear://濃淡を濃くする
                if (ChangeAlpha(0.0f, 0.5f) >= 1.0f) 
                {
                    _colorComplement = 0.0f;
                    _phase = Phase.Waiting;
                }
                break;
            case Phase.Waiting:
                _rigidBody.useGravity = true;
                _rigidBody.velocity += new Vector3(Random.Range(-20.0f,20.0f), 0.0f, Random.Range(-20.0f, 20.0f));
                _phase = Phase.Destroy;
                break;
            case Phase.Destroy://濃淡を薄くする
                if (ChangeAlpha(0.5f, 0.0f) >= 1.0f)
                {
                    Debug.Log("オブジェクトが消えました");
                    Destroy(this.gameObject);
                }
                break;
        }
    }

    private float ChangeAlpha(float from ,float to)
    {
        float speed = 0.8f;
        _colorComplement += Time.deltaTime * speed;;
     
        //色の変更
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
