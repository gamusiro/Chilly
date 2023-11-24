using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFloorManager : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _fallingFloorCubeTransform;
    [SerializeField] private GameObject _fallingFloorLanePrefab;

    private Vector3 _standardPosition;
    void Start()
    {
        _standardPosition = _playerTransform.position;
    }

    void Update()
    {
        if (_playerTransform && _fallingFloorCubeTransform && _fallingFloorLanePrefab) 
        CreateObject("z", _playerTransform.position.z, _standardPosition.z, _fallingFloorCubeTransform.localScale.z);
    }

    private void CreateObject(string direction, float playerPosition, float standardPosition, float fallingObjectScale)
    {
        //プレイヤーと基準点間の距離を求める
        float distance = Mathf.Abs(playerPosition - standardPosition);

        //落下オブジェクトひとつ分移動したらそこを基準点とする。
        if (distance >= fallingObjectScale)
        {
            float nextStandardPosition = 0.0f;//次の基準点
            nextStandardPosition = standardPosition - fallingObjectScale * 2.0f;

                if (playerPosition < standardPosition)
                {  
                    if (direction == "x")
                        _standardPosition.x = nextStandardPosition;
                    if (direction == "z")
                        _standardPosition.z = nextStandardPosition;
                }

                if (playerPosition > standardPosition)
                {
                    if (direction == "x")
                        _standardPosition.x = nextStandardPosition;
                    if (direction == "z")
                        _standardPosition.z = nextStandardPosition;
                }

            Instantiate(_fallingFloorLanePrefab, _standardPosition, Quaternion.identity, this.transform);      
        }
    }
}
