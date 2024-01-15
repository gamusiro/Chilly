using Cinemachine;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameCameraPhaseManager : AbstractCameraPhaseManager
{
    [SerializeField] private CS_Player _player;
    private float _elapsedTime = 0.0f;

    private async void Start()
    {
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        // �J�����̃A�Z�b�g��ݒ肷��
        string audioName = CS_LoadNotesFile.GetFolderName();
        _transTimeList = GetAssets(audioName);

        //�J�����̗D��x�����Z�b�g����
        foreach (var virtualCamera in _virtualCamera) { virtualCamera.Priority = 0; }
        _cameraIndex = 0;
        _virtualCamera[_cameraIndex].Priority = 1;

        //�J�����̑J�ڏ���
        while (_cameraIndex < _virtualCamera.Count - 1) 
        {
            //�J�n���牽�b�Ő؂�ւ�邩
            _elapsedTime = _transTimeList[_cameraIndex];
            await UniTask.WaitUntil(() => CS_AudioManager.Instance.TimeBGM > _elapsedTime, cancellationToken: token);
            NextCamera();      
        }
    }

    //�J������؂�ւ���
    protected new void NextCamera()
    {
        _virtualCamera[_cameraIndex].Priority = 0;
        _cameraIndex++;
        _virtualCamera[_cameraIndex].Priority = 1;

        // ����J�����̐ݒ�
        _player.SetUsingCamera();
    }

    /// <summary>
    /// �J�����̕b���ݒ�p(���ł�����)
    /// </summary>
    /// <param name="audioName"></param>
    /// <returns></returns>
    private List<float> GetAssets(string audioName)
    {
        Dictionary<string, List<float>> pairs = new Dictionary<string, List<float>>();
        pairs.Add("WeMadeIt", new List<float> { 1, 73, 126 });
        pairs.Add("IWillBeHere", new List<float> { 1, 68, 132 });

        if(!pairs.ContainsKey(audioName))
        {
            Debug.LogError(audioName + "�̃f�[�^��������܂���");
            return null;
        }

        return pairs[audioName];
    }
}