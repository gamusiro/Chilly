using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpectrum : MonoBehaviour
{
    public AudioSpectrum spectrum;
    //�I�u�W�F�N�g�̔z��
    public Transform[] cubes;
    //�X�y�N�g�����̍����{��
    public float scale;

    private List<Vector3> _standardCubePosition = new List<Vector3>();


    private void Start()
    {
        foreach (var cube in cubes)
        {
            _standardCubePosition.Add(cube.position);
        }
    }
    private void Update()
    {
        int i = 0;

        foreach (var cube in cubes)
        {
            //�I�u�W�F�N�g�̃X�P�[�����擾
            var localScale = cube.localScale;
            //�X�y�N�g�����̃��x�����X�P�[����Y�X�P�[���ɒu��������
            localScale.y = spectrum.Levels[i] * scale;
            cube.localScale = localScale;
            cube.position = _standardCubePosition[i] + localScale * 0.5f;

            i++;
        }
    }
}