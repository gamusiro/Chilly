using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;
using System.Threading;
using DG.Tweening;

public class LastMovieManager : MonoBehaviour
{
    [SerializeField, CustomLabel("�t�F�[�Y1�I�u�W�F�N�g")]
    private GameObject _phase1;

    //�v���C���[
    [SerializeField] private LastPlayer _playerCS;

    //�G�l�~�[
    [SerializeField] private LastEnemy _enemy;

    //���[�u�I�u�W�F�N�g
    [SerializeField] Transform _moveObjectTransform;

    //���g
    [SerializeField] private SoundWave _soundWave;

    //�x��
    [SerializeField] private Bell _bell;

    [SerializeField, CustomLabel("�t�F�[�Y2�I�u�W�F�N�g")]
    private GameObject _phase2;

    //�F�B�̃v���n�u
    [SerializeField] private GameObject _friendPrefab;

    //�t�F�[�h
    [SerializeField] private Fade_K _fade;
    [SerializeField] string _nextScene; 

    //�^�C�g�����S
    [SerializeField] private LastLogo _lastLogoFirstShow;
    [SerializeField] private LastLogo _lastLogoSecondShow;

    private async void Start()
    {
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        CS_AudioManager.Instance.PlayAudio("Last", true);
        CS_AudioManager.Instance.MasterVolume = 1.0f;

        //�x�����Ȃ�炷
        await UniTask.WaitUntil(() => _playerCS.OnBell, cancellationToken: token);
        _bell.SetRing(true);
        Instantiate(_soundWave, _bell.GetPosition(), Quaternion.identity, _phase1.transform);
        _playerCS.OnBell = false;

        //�G�����ꂽ����ɂȂ�
       �@float time = 4.0f;
        await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: token);
        _enemy.Disapper(_enemy.transform);

        //������
        time = 5.0f;
        await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: token);
        _enemy.Explosion(_phase1.transform);

        //�t�F�[�Y1�I��,�t�F�[�Y2�J�n
        time = 5.0f;
        await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: token);
        _bell.SetRing(false);
        Destroy(_phase1);
        Instantiate(_phase2);

        time = 0.0f;
        _fade.FadeIn(time);

        //CS_AudioManager.Instance.MasterVolume = 0.0f;
        //CS_AudioManager.Instance.StopBGM();
        //CS_AudioManager.Instance.PlayAudio("Rescue", true);
        //CS_AudioManager.Instance.MasterVolume = 1.0f;

        time = 1.0f;
        _fade.FadeOut(time);

        //�F�B����
        time = 1.0f;
        await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: token);
        Vector3 spawnPosition = new Vector3(0.0f, 500.0f, 30.0f);
        Instantiate(_friendPrefab, spawnPosition, Quaternion.identity);


        //�ŏ���Logo�̕\��
        time = 18.5f;
        await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: token);
        _lastLogoFirstShow.Show();

        //��ڂ�Logo�̕\��
        time = 3.0f;
        await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: token);
        _lastLogoSecondShow.Show();
        CS_AudioManager.Instance.MasterVolume = 2.0f;
        Debug.Log(CS_AudioManager.Instance.MasterVolume);

        time = 4.0f;
        await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: token);
        CS_AudioManager.Instance.MasterVolume = 0.0f;
        CS_AudioManager.Instance.StopBGM();
        SceneManager.LoadScene(_nextScene);
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
       MoveObject();
    }

    //�ړ��I�u�W�F�N�g
    private  void MoveObject()
    {
        if (_moveObjectTransform == null)
            return;

        Vector3 position = _moveObjectTransform.localPosition;
        position.z -= 3.0f;
        _moveObjectTransform.localPosition = position;
    }
}
