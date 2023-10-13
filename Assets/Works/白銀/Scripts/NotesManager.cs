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

[Serializable]
public class NotesTime
{
   public int m_laneNum;// 何番レーンにノーツが落ちるか
   public int m_noteType;// 何ノーツか(ロングかどうか)
   public float m_noteTime;// ノーツが判定線と重なる時間
}

public class NotesManager : BaseObject
{
    [Header("読み込むJsonデータ")]
    [SerializeField]　protected string m_songName;

    [SerializeField] protected int m_noteNum;// ノーツの総数
    [SerializeField] protected List<NotesTime> m_notesTimesList = new List<NotesTime>();


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
            
            //ノーツの情報を追加
            NotesTime notesTime = new NotesTime();
            notesTime.m_laneNum = inputJson.notes[i].block;
            notesTime.m_noteType = inputJson.notes[i].type;
            notesTime.m_noteTime = time;
            m_notesTimesList.Add(notesTime);
        }
        //ソート
        m_notesTimesList.Sort((a, b) => a.m_noteTime.CompareTo(b.m_noteTime)) ;
    }
}