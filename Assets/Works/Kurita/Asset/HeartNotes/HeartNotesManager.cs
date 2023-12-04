using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class HeartNotesManager : CS_LoadNotesFile
{
    // 生成するオブジェクト
    [SerializeField, CustomLabel("生成オブジェクト")]
    GameObject _heartPrefab;

    private void Start()
    {
        this.Load();// 読み込み処理
        HeartNotes();//ハートノーツ
    }

    private async void HeartNotes()
    {
        float mae = 2.0f;
        foreach (var info in m_perNoteInfos)
        {
            await UniTask.WaitUntil(() => CS_AudioManager.Instance.TimeBGM >= info.time - mae);

            // 生成する座標を計算する
            Vector3 createPos = Vector3.zero;
            createPos.x = -60.0f + info.lane * 30.0f;
            float time = info.time - CS_AudioManager.Instance.TimeBGM;
            createPos.y = (9.81f / 2.0f) * Mathf.Pow(time, 2.0f);
            createPos.y = 0.0f;
            createPos.z = info.time * CS_MoveController.GetMoveVel() * -1.0f - 10.0f;//(少し手前に置く)

            //生成する
            Instantiate(_heartPrefab, createPos, Quaternion.identity, this.transform);
            Debug.Log("生成されました");
        }
    }

    private async void Create()
    {
    
    }
}
