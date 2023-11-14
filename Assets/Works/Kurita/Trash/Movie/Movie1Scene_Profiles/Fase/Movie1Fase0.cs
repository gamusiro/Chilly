using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movie1Fase0 : FaseParent
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _cameraTransform;
    //[SerializeField ]private PostProcessVolume _postProcessVolume;

    private void Update()
    {
        MoveObject(_playerTransform, 0.21f);
        MoveObject(_cameraTransform, 0.2f);
    }
}