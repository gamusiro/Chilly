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
   public int m_laneNum;// ���ԃ��[���Ƀm�[�c�������邩
   public int m_noteType;// ���m�[�c��(�����O���ǂ���)
   public float m_noteTime;// �m�[�c��������Əd�Ȃ鎞��
}

public class NotesManager : BaseObject
{
    [Header("�ǂݍ���Json�f�[�^")]
    [SerializeField]�@protected string m_songName;

    [SerializeField] protected int m_noteNum;// �m�[�c�̑���
    [SerializeField] protected List<NotesTime> m_notesTimesList = new List<NotesTime>();


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
            
            //�m�[�c�̏���ǉ�
            NotesTime notesTime = new NotesTime();
            notesTime.m_laneNum = inputJson.notes[i].block;
            notesTime.m_noteType = inputJson.notes[i].type;
            notesTime.m_noteTime = time;
            m_notesTimesList.Add(notesTime);
        }
        //�\�[�g
        m_notesTimesList.Sort((a, b) => a.m_noteTime.CompareTo(b.m_noteTime)) ;
    }
}