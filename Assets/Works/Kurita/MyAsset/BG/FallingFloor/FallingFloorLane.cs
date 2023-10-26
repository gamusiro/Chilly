using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFloorLane : MonoBehaviour
{
    [SerializeField] List<GameObject> _fallingFloorSectionList = new List<GameObject>();
    private int _sectionIndex;
    private float _cubeIndex;
    private bool _fallFlag;
    void Start()
    {
        _sectionIndex = 0;
        _cubeIndex = 0;
        _fallFlag = false;
    }

    void Update()
    {
        if (_fallFlag)
            Fall();
    }

    void Fall()
    {
        if (_cubeIndex < 1)
        {
            Transform child = _fallingFloorSectionList[(int)_cubeIndex].transform.GetChild((int)_cubeIndex);
            UseGravity(child);

            _cubeIndex += Time.deltaTime*10.0f;
            return;
        }

        //左右のオブジェクトのリジットボディを操作する
        for (int i = 1; i < _fallingFloorSectionList.Count; i++) 
        {
            //キューブがまだ残っていれば落下開始
            if ((int)_cubeIndex - 1 < _fallingFloorSectionList[i].transform.childCount)
            {
                Transform child = _fallingFloorSectionList[i].transform.GetChild((int)_cubeIndex - 1) ;
                UseGravity(child);
            }

            Debug.Log(_fallingFloorSectionList[0].transform.childCount);
        }

        _cubeIndex += Time.deltaTime * 10.0f;

        //全て落としきったら落下開始処理をストップ
        if (_cubeIndex - 1 > _fallingFloorSectionList[1].transform.childCount)
            _fallFlag = false;

       
    }

    private void UseGravity(Transform child)
    {
        Rigidbody rigidbody = child.GetComponent<Rigidbody>();

        //重力をつける
        if (rigidbody)
        {
            rigidbody.useGravity = true;

            rigidbody.AddForce(Random.Range(-300.0f, 300.0f), 0.0f, Random.Range(-300.0f, 300.0f));
        }
        else
            Debug.LogWarning("リジッドボディがついていません");
    }

    public void SetFallFlag()
    {
        _fallFlag = true;

    }
}
