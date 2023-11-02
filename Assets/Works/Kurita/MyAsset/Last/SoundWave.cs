using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundWave : MonoBehaviour
{
    [SerializeField] private Vector3 _minScale;
    [SerializeField] private Vector3 _maxScale;
    [SerializeField] private Transform _thisTransform;
    [SerializeField] private List<Renderer> _rendererList = new List<Renderer>();
    private bool _startFlag = true;
    private float _frame = 0.0f;
 
    void Start()
    {
       
    }

    void Update()
    {
        if(_startFlag)
        {
            //ägëÂÇ∆ÉøílÇÃïœçX
            float speed = 0.5f;
            _frame += speed * Time.deltaTime;
            _thisTransform.localScale = Vector3.Lerp(_minScale, _maxScale, _frame);

            speed = 0.0025f;
            foreach (var renderer in _rendererList)
            {
                Color color = renderer.material.color;
                color.a -= speed;
                if (color.a < 0.0f)
                { 
                    color.a = 0.0f;
                    return;
                }

                renderer.material.color = color;
            }
        }
    }

    public void StartFlag()
    {
        _startFlag = true;
    }
}
