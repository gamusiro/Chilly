using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    [Header("�����ɒǉ����Ă���")]
    [SerializeField] private List<GameObject> _gameObject;
    private List<BaseObject> _baseObject = new List<BaseObject>();

    //�q�G�����L�[����I�u�W�F�N�g��ǉ�
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

    //�X�N���v�g����C���X�^���X��ǉ�
    public void SetObject(GameObject gameObject)
    {
        _baseObject.Add(gameObject.GetComponent<BaseObject>());
    }

    //�X�V
    private void Update()
    {
        foreach (BaseObject baseObject in _baseObject)
        {
            if (baseObject)
                baseObject.Updated();
        }
    }
}
