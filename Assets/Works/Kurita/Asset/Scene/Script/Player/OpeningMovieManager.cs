using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;
using System.Threading;

public class OpeningMovieManager : MonoBehaviour
{
    //���[�u�I�u�W�F�N�g
    [SerializeField] Transform _moveObjectTransform;

    //�G�l�~�[
    [SerializeField] private ParticleSystem _ateParticle;

    //�F�B
    [SerializeField] private OpeningFriend _friend;

    //�z�����܂�鏈��(���`�⊮)
    [SerializeField] private Transform _friendStartTransform;
    [SerializeField] private Transform _enemyMouthTransform;

    //�t�F�[�h
    [SerializeField] private Fade_K _fade;
    [SerializeField] string _sceneName;

    private async void Start()
    {
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        CS_AudioManager.Instance.MasterVolume = 1.0f;
        CS_AudioManager.Instance.PlayAudio("Opening", true);

        //��������
        _ateParticle.Stop();

        MoveObject();

        //���z�����݃p�[�e�B�N������
        {
            float time = 18.0f;
            await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: token);

            if (_ateParticle)
                _ateParticle.Play();
            else
                return;
        }

        //���F�B���z�����܂��
        {
            float time = 2.0f;
            await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: token);

            if (_friend)
                _friend.Ate(_friend.transform, _enemyMouthTransform);
            else
                return;
        }

        //���t�F�[�h�C��
        {
            float time = 0.5f;
            await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: token);

            if (_fade)
                _fade.FadeIn(0.5f);
            else
                return;
        }

        //���t�F�[�h�A�E�g
        {
            float time = 3.0f;
            await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: token);

            if (_friend && _fade && _ateParticle)
            {
                _friend.SetDestroy();
                float fadeTime = 2.0f;
                _fade.FadeOut(fadeTime);
                _ateParticle.Stop();
            }
            else
                return;
        }

        //���t�F�[�h�C��
        {
            float time = 6.0f;
            await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: token);

            if (_fade)
            {
                float fadeTime = 2.0f;
                _fade.FadeIn(fadeTime);
            }
            else
                return;
        }

        //���V�[���J��
        {
            float time = 2.0f;
            await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: token);

            if (this != null && this.gameObject)
            {
                CS_AudioManager.Instance.StopBGM();
                SceneManager.LoadScene(_sceneName);
            }
            else
                return;
        }
    }

    //�ړ��I�u�W�F�N�g
    private async void MoveObject()
    {
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        while (true)
        {
            //�k���`�F�b�N
            if (_moveObjectTransform == null)
                return;

            Vector3 position = _moveObjectTransform.localPosition;
            position.z -= 3.0f;
            _moveObjectTransform.localPosition = position;

            await UniTask.WaitForFixedUpdate(cancellationToken: token);
        }
    }
}
