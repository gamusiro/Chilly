using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFloorManager : MonoBehaviour
{
    [SerializeField] GameObject _fallingFloorLane;
    private List<FallingFloorLane> _fallingFloorLaneList = new List<FallingFloorLane>();
    private float _index;

    private void Start()
    {
        //全てのレーンを取得する
        for (int i = 0; i < _fallingFloorLane.transform.childCount; i++)
        {
            Transform lane = _fallingFloorLane.transform.GetChild(i);
            _fallingFloorLaneList.Add(lane.GetComponent<FallingFloorLane>());
        }
        _index = 0.0f;
    }

    private void Update()
    {
        Debug.Log(_index);
        //取得したレーン順に処理を開始していく
        if ((int)_index < _fallingFloorLaneList.Count)
            _fallingFloorLaneList[(int)_index].SetFallFlag();

        _index += Time.deltaTime*10.0f;
    }
}
