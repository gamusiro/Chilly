using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
public class PerNoteInfo
{
    public int lane;
    public int type;
    public float time;
}

public class CS_LoadNotesFile : MonoBehaviour
{
    static string m_folderName = "WeMadeIt";
    

    public List<PerNoteInfo> m_perNoteInfos = new List<PerNoteInfo>();

    static public void SetFolderName(string _foldeName)
    {
        m_folderName = _foldeName;
    }
    static public string GetFolderName()
    {
        return m_folderName;
    }



    public void Load(string _type)
    {
        string path = m_folderName + "/" + _type;

        string inputString = Resources.Load<TextAsset>(path).ToString();
        Data inputJson = JsonUtility.FromJson<Data>(inputString);

        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            float kankaku = 60.0f / (inputJson.BPM * (float)inputJson.notes[i].LPB);
            float beatSec = kankaku * (float)inputJson.notes[i].LPB;
            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset;

            PerNoteInfo info = new PerNoteInfo();
            info.time = time;
            info.lane = inputJson.notes[i].block;
            info.type = inputJson.notes[i].type;
            m_perNoteInfos.Add(info);
        }

        // ƒ\[ƒgˆ—
        m_perNoteInfos.Sort((a, b) => a.time.CompareTo(b.time));
    }
}
