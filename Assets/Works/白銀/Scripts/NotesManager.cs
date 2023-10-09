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
    [Header("�ǂݍ���Json�f�[�^")]
    [SerializeField]�@protected string m_songName;

    [NonSerialized] protected int m_noteNum;         �@�@�@�@�@�@�@ �@�@�@�@ // �m�[�c�̑���
    [NonSerialized] protected List<int> m_laneNum = new List<int>();         // ���ԃ��[���Ƀm�[�c�������邩
    [NonSerialized] protected List<int> m_noteType = new List<int>();        // ���m�[�c��(�����O���ǂ���)
    [NonSerialized] protected List<float> m_notesTime = new List<float>();   // �m�[�c��������Əd�Ȃ鎞��


    void OnEnable()
    {
        m_noteNum = 0;
        Load(m_songName);
    }

    private void Load(string SongName)
    {
        // Json�t�@�C���̓ǂݍ���
        string inputString = Resources.Load<TextAsset>(SongName).ToString();
        Data inputJson = JsonUtility.FromJson<Data>(inputString);

        // ���m�[�c�̐����擾
        m_noteNum = inputJson.notes.Length;

        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            // 1���߂̒���
            float kankaku = 60.0f / (inputJson.BPM * (float)inputJson.notes[i].LPB);
            
            // �m�[�c�Ԃ̒���
            float beatSec = kankaku * (float)inputJson.notes[i].LPB;
            
            // �m�[�c�̍~���Ă��鎞��
            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset;
            
            m_notesTime.Add(time);
            m_laneNum.Add(inputJson.notes[i].block);
            m_noteType.Add(inputJson.notes[i].type);
        }
    }
}