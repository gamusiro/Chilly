using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cysharp.Threading.Tasks;
using System;

public class OpeningMovieManager : MonoBehaviour
{
    //���[�u�I�u�W�F�N�g
    [SerializeField] Transform _moveObjectTransform;

    //�G�l�~�[
    [SerializeField] private Enemy _enemy;

    //�F�B�̃v���n�u
    [SerializeField] private Friend _friend;

    ////�t�F�[�h
    //[SerializeField] private Fade_K _fade;

    private async void Start()
    {
        //MoveObject();

        ////�x�����Ȃ�炷
        //await UniTask.WaitUntil(() => _playerCS.OnBell);
        //_bell.SetRing(true);
        //Instantiate(_soundWave, _bell.GetPosition(), Quaternion.identity, _phase1.transform);
        //_playerCS.OnBell = false;

        ////�G�����ꂽ����ɂȂ�
        //await UniTask.Delay(TimeSpan.FromSeconds(4.0f));
        //_enemy.Disapper(_phase1.transform);
        ////Destroy(_moveObjectTransform.gameObject);

        ////������
        //await UniTask.Delay(TimeSpan.FromSeconds(5.0f));
        //_enemy.Explosion(_phase1.transform);

        ////�t�F�[�Y1�I��,�t�F�[�Y2�J�n
        //await UniTask.Delay(TimeSpan.FromSeconds(5.0f));
        //_bell.SetRing(false);
        //Destroy(_phase1);
        //Instantiate(_phase2);
        //_fade.FadeIn(0.0f);
        //_fade.FadeOut(1.0f);

        ////�F�B����
        //await UniTask.Delay(TimeSpan.FromSeconds(1.0f));
        //Instantiate(_friendPrefab, new Vector3(0.0f, 500.0f, 30.0f), Quaternion.identity);

        //  await UniTask.Delay(TimeSpan.FromSeconds(1.0f));
       // _fade.FadeIn(3.0f);
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
