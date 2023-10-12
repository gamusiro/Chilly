using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    [Header("ここに追加していく")]
    [SerializeField] private List<GameObject> _gameObject;
    private List<BaseObject> _baseObject = new List<BaseObject>();

    //ヒエラルキーからオブジェクトを追加
    private void Start()
    {
        foreach(GameObject gameObject in _gameObject)
        {
            _baseObject.Add(gameObject.GetComponent<BaseObject>());      
        }

        foreach (BaseObject baseObject in _baseObject)
        {
            baseObject.Initialized();
        }
    }

    //スクリプトからインスタンスを追加
    public void SetObject(GameObject gameObject)
    {
        _baseObject.Add(gameObject.GetComponent<BaseObject>());
    }

    //更新
    private void Update()
    {
        foreach (BaseObject baseObject in _baseObject)
        {
            if (baseObject)
                baseObject.Updated();
        }
    }
}
