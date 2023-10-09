using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Note
{
    public int type;
    public int num;
    public int block;
    public int LPB;
}

[Serializable]
public class Data
{
    public string name;
    public int maxBlock;
    public int BPM;
    public int offset;
    public Note[] notes;
}

public class NotesManager : BaseObject
{
    [Header("読み込むJsonデータ")]
    [SerializeField]　protected string m_songName;

    [NonSerialized] protected int m_noteNum;         　　　　　　　 　　　　 // ノーツの総数
    [NonSerialized] protected List<int> m_laneNum = new List<int>();         // 何番レーンにノーツが落ちるか
    [NonSerialized] protected List<int> m_noteType = new List<int>();        // 何ノーツか(ロングかどうか)
    [NonSerialized] protected List<float> m_notesTime = new List<float>();   // ノーツが判定線と重なる時間


    void OnEnable()
    {
        m_noteNum = 0;
        Load(m_songName);
    }

    private void Load(string SongName)
    {
        // Jsonファイルの読み込み
        string inputString = Resources.Load<TextAsset>(SongName).ToString();
        Data inputJson = JsonUtility.FromJson<Data>(inputString);

        // 総ノーツの数を取得
        m_noteNum = inputJson.notes.Length;

        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            // 1小節の長さ
            float kankaku = 60.0f / (inputJson.BPM * (float)inputJson.notes[i].LPB);
            
            // ノーツ間の長さ
            float beatSec = kankaku * (float)inputJson.notes[i].LPB;
            
            // ノーツの降ってくる時間
            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset;
            
            m_notesTime.Add(time);
            m_laneNum.Add(inputJson.notes[i].block);
            m_noteType.Add(inputJson.notes[i].type);
        }
    }
}