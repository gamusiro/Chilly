using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    [Header("‚±‚±‚É’Ç‰Á‚µ‚Ä‚¢‚­")]
    [SerializeField] private List<BaseObject> _baseObject;

    // Start is called before the first frame update
    void Start()
    {
        _baseObject[0].Initialized();
    }


    // Update is called once per frame
    void Update()
    {
        _baseObject[0].Updated();
    }
}
