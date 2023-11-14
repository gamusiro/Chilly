using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    private Vector3 _standardPosition;
    private float _radian;
    [SerializeField] float _range = 20.0f;

    private void Start()
    {
        _standardPosition = this.gameObject.transform.position;
        _radian = Random.Range(0.0f, 359.0f);
    }

    private void Update()
    {
        float speed = 2.0f;
        _radian += speed * Time.deltaTime;
        Vector3 position = _standardPosition;
        position.y = _standardPosition.y + Mathf.Sin(_radian) * _range;
        this.gameObject.transform.position = position;
    }
}
