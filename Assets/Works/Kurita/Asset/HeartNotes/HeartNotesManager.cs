using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class HeartNotesManager : CS_LoadNotesFile
{
    // ��������I�u�W�F�N�g
    [SerializeField, CustomLabel("�����I�u�W�F�N�g")]
    GameObject _heartPrefab;

    private void Start()
    {
        this.Load();// �ǂݍ��ݏ���
        HeartNotes();//�n�[�g�m�[�c
    }

    private async void HeartNotes()
    {
        float mae = 2.0f;
        foreach (var info in m_perNoteInfos)
        {
            await UniTask.WaitUntil(() => CS_AudioManager.Instance.TimeBGM >= info.time - mae);

            // ����������W���v�Z����
            Vector3 createPos = Vector3.zero;
            createPos.x = -60.0f + info.lane * 30.0f;
            float time = info.time - CS_AudioManager.Instance.TimeBGM;
            createPos.y = (9.81f / 2.0f) * Mathf.Pow(time, 2.0f);
            createPos.y = 0.0f;
            createPos.z = info.time * CS_MoveController.GetMoveVel() * -1.0f - 10.0f;//(������O�ɒu��)

            //��������
            Instantiate(_heartPrefab, createPos, Quaternion.identity, this.transform);
            Debug.Log("��������܂���");
        }
    }

    private async void Create()
    {
    
    }
}
