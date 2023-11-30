using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesUIManager : MonoBehaviour
{
    [SerializeField] private List<float> _noetsTimeList = new();
    [SerializeField] private NotesUI _notesPrefab;
    [SerializeField] private RectTransform _spawnTransform;
    [SerializeField] private RectTransform _centerTransform;

    int _index = 0;
    float _keikaJikan = 0.0f;
    void Start()
    {
        _keikaJikan = _noetsTimeList[_index];
    }

    // Update is called once per frame
    void Update()
    {
        //生成から何秒後に真ん中に来るか
        float delay = .0f;

        if (_index + 1 < _noetsTimeList.Count)
        {
           // if (CS_AudioManager.Instance.TimeBGM >= _keikaJikan - delay)
            if (CS_AudioManager.Instance.TimeBGM >= _keikaJikan)
            {
                var notes = Instantiate(_notesPrefab, _spawnTransform.position, Quaternion.identity, this.transform);

                //速度の計算
                float distance = _spawnTransform.position.x - _centerTransform.position.x;
                distance = Mathf.Abs(distance);
                float speed = distance / delay;
                //notes.Speed = speed;


               // if (_index + 1 < _noetsTimeList.Count)
                {
                    _index++;
                    _keikaJikan += _noetsTimeList[_index];
                }
            }
        }
    }
}
