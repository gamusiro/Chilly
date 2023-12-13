using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;
using System.Threading;

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

    private async void Start()
    {
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        CS_AudioManager.Instance.PlayAudio("Explosion", true);
        CS_AudioManager.Instance.MasterVolume = 1.0f;
        //_fade.FadeOut(0.5f);

        //�x�����Ȃ�炷
        await UniTask.WaitUntil(() => _playerCS.OnBell, cancellationToken: token);
        _bell.SetRing(true);
        Instantiate(_soundWave, _bell.GetPosition(), Quaternion.identity, _phase1.transform);
        _playerCS.OnBell = false;

        //�G�����ꂽ����ɂȂ�
        await UniTask.Delay(TimeSpan.FromSeconds(4.0f), cancellationToken: token);
        _enemy.Disapper(_enemy.transform);
        //Destroy(_moveObjectTransform.gameObject);

        //������
        await UniTask.Delay(TimeSpan.FromSeconds(5.0f), cancellationToken: token);
        _enemy.Explosion(_phase1.transform);

        //�t�F�[�Y1�I��,�t�F�[�Y2�J�n
        await UniTask.Delay(TimeSpan.FromSeconds(5.0f), cancellationToken: token);
        _bell.SetRing(false);
        Destroy(_phase1);
        Instantiate(_phase2);

        _fade.FadeIn(0.0f);

        CS_AudioManager.Instance.MasterVolume = 0.0f;
        CS_AudioManager.Instance.StopBGM();
        CS_AudioManager.Instance.PlayAudio("Rescue", true);
        CS_AudioManager.Instance.MasterVolume = 1.0f;

        _fade.FadeOut(1.0f);

        //�F�B����
        await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: token);
        Instantiate(_friendPrefab, new Vector3(0.0f, 500.0f, 30.0f), Quaternion.identity);

        await UniTask.Delay(TimeSpan.FromSeconds(22.0f), cancellationToken: token);
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
      //  while (true)
        //{
            //�k���`�F�b�N
            if (_moveObjectTransform == null)
                return;

            Vector3 position = _moveObjectTransform.localPosition;
            position.z -= 3.0f;
            _moveObjectTransform.localPosition = position;

       //     await UniTask.Delay(1);
        //}
    }
}
