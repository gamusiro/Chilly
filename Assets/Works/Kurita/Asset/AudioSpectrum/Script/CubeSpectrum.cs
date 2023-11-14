using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpectrum : MonoBehaviour
{
    public AudioSpectrum spectrum;
    //オブジェクトの配列
    public Transform[] cubes;
    //スペクトラムの高さ倍率
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
            //オブジェクトのスケールを取得
            var localScale = cube.localScale;
            //スペクトラムのレベル＊スケールをYスケールに置き換える
            localScale.y = spectrum.Levels[i] * scale;
            cube.localScale = localScale;
            Vector3 position = cube.position;
            position.x = _standardCubePosition[i].x - localScale.x * 0.5f;
            cube.position = position;

            i++;
        }
    }
}