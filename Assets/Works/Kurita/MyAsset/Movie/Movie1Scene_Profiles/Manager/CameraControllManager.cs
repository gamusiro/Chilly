using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControllManager : MonoBehaviour
{
    [Header("最初に映すカメラのインデックス")]
    [SerializeField] private int _index = 0;
    [SerializeField] private List<CinemachineVirtualCamera> _cinemachineVirtualCameraList;

    private void Start()
    {
        foreach (var cinemachineVirtualCamera in _cinemachineVirtualCameraList)
        {
            cinemachineVirtualCamera.Priority = 0;
        }
        _cinemachineVirtualCameraList[_index].Priority = 1;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _index < _cinemachineVirtualCameraList.Count - 1)
        {
            _cinemachineVirtualCameraList[_index].Priority = 0;
            _index++;
            _cinemachineVirtualCameraList[_index].Priority = 1;
        }

        Debug.Log(_index);
    }

}