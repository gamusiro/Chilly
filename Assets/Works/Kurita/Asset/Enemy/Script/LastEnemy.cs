using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using DG.Tweening;

public class LastEnemy : Enemy
{
    //‚â‚ç‚ê‚½‚Æ‚«‚Ì–Ú(Prefab)
    [SerializeField] protected DisapperEyes _disapperEyePrefab;
    [SerializeField] protected Transform _disapperEyeParent;
    protected DisapperEyes _disapperEyeInstance = null;
    [SerializeField] protected GameObject _explosionPrefab;
    [SerializeField] protected GameObject _explosionPrefab2;

    private void Start()
    {
        //–Ú‚ÆŒû‚Ìƒ‰ƒWƒAƒ“
        _eyeTransform[(int)Eye.Left].eulerAngles = new Vector3(0.0f, 0.0f, 45.0f);
        _eyeTransform[(int)Eye.Right].eulerAngles = new Vector3(0.0f, 0.0f, 225.0f);
        _subEyeSpeed = _eyeSpeed = 4.0f;
        _subEyeSpeed *= 0.1f;
        _mouthScaleeRadian = 0.0f;
        _subMouthSpeed = _mouthSpeed = 0.04f;
        _subMouthSpeed *= 0.4f;
        _standardMouthScale = _mouthTransform.localScale;
        _moveSpeed = 3.0f;
    }

    private void FixedUpdate()
    {
        Eyes();
        Mouth();

        float range = 10.0f;
        Move(range);
    }

    //‚â‚ç‚ê‚é
    public async void Disapper(Transform parent)
    {
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        _disapperEyeInstance = Instantiate(_disapperEyePrefab, _disapperEyeParent);

        while (true)
        {
            await UniTask.DelayFrame(1, cancellationToken: token);

            //“®‚«‚ğ’x‚­‚·‚é
            _eyeSpeed -= _subEyeSpeed * Time.deltaTime;
            _eyeSpeed = Mathf.Clamp(_eyeSpeed, 0.0f, _eyeSpeed);
            _mouthSpeed -= _subMouthSpeed * Time.deltaTime;
            _mouthSpeed = Mathf.Clamp(_mouthSpeed, 0.0f, _mouthSpeed);
            //‚â‚ç‚ê‚½‚Æ‚«‚Ì–Ú‚Ì“®‚«‚Æ’Êí‚Ì–Ú‚Ì“®‚«‚ğ‡‚í‚¹‚é
            if (_disapperEyeInstance)
                _disapperEyeInstance.SetAngle(_eyeTransform[(int)Eye.Left].eulerAngles, _eyeTransform[(int)Eye.Right].eulerAngles);
        }
    }

    //”š”­
    public async void Explosion(Transform parent,GameObject flag)
    {
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        Vector3 createPosition = _disapperEyeParent.position + new Vector3(0.0f, 40.0f, -20.0f);
        Instantiate(_explosionPrefab, createPosition, Quaternion.identity, parent);
        await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: token);
        if (!flag)
            return;
        Instantiate(_explosionPrefab2, createPosition, Quaternion.identity, parent);
    }
}
