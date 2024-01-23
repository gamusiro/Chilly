using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cinemachine;

public class HP : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;//HPのメッシュレンダラー
    [SerializeField] private MeshRenderer _meshRendererFrame;//HPフレームのメッシュレンダラー
    private int _hp = 0;//現在の体力
    private bool _isAlive;

    [SerializeField] private CS_Player _player;//位置をプレイヤーに合わせるため
    [SerializeField] private GameCameraPhaseManager _gameCameraPhaseManager;//向きをカメラ方向に合わせるため
    [SerializeField] private CinemachineBrain _cinemachineBrain;

    private CinemachineBlend _cinemachineBlend;
    private Tween _hideTweener;
    private Tween _showTweener;

    public bool Die
    {
        get { return !_isAlive; }
    }

    private void Start()
    {
        _hp = _meshRenderer.materials.Length - 1;//HPの値を設定する
        _isAlive = true;
        _cinemachineBlend = null;
        _hideTweener = null;
        _showTweener = null;
        Recover();
    }

    private void FixedUpdate()
    {
        //座標をプレイヤーから少しずらす
        Vector3 offset = new Vector3(20.0f, 5.0f, 0.0f);
        this.transform.position = _player.transform.position + offset;

        //向きをカメラに合わせる
        this.transform.LookAt(_gameCameraPhaseManager.GetCurCamera().transform.position);

        //カメラのブレンド中は表示しない
        if (_cinemachineBrain.ActiveBlend != _cinemachineBlend)
        {
            _cinemachineBlend = _cinemachineBrain.ActiveBlend;
            if (_cinemachineBlend != null)
                Hide();
            else
                Show();
        }
    }

    public void Hit()
    {
        //表示するアニメーションを削除する
        _showTweener?.Kill();

        //やられていなければ処理を続行
        if (_hp - 1 < 0)
        {
            _isAlive = false;
            return;
        }

        //アニメーション
        float alpha = 0.0f;
        float time = 1.0f;

        _hideTweener = _meshRenderer.materials[_hp].DOFade(alpha, time).SetLink(this.gameObject);

        //体力を減らす
        _hp--;
    }

    public void Recover()
    {
        //非表示するアニメーションを削除する
        _hideTweener?.Kill();

        //体力が上限になっていなければ処理を続行
        if (_hp >= _meshRenderer.materials.Length - 1)
            return;

        //体力を減らす
        _hp++;

        //アニメーション
        //アニメーション
        float alpha = 1.0f;
        float time = 1.0f;
        _showTweener = _meshRenderer.materials[_hp]
            .DOFade(alpha, time)
            .SetLink(this.gameObject);
    }

    public void Hide()
    {
        //表示するアニメーションを削除する
        _showTweener?.Kill();

        float alpha = 0.0f;
        float time = 2.5f;

        foreach (var material in _meshRenderer.materials)
        {
            material
                .DOFade(alpha, time)
                .SetLink(this.gameObject);
        }

        int index = 0;
        _hideTweener = _meshRendererFrame.materials[index]
                     .DOFade(alpha, time)
                     .SetLink(this.gameObject);
    }

    public void Show()
    {
        //非表示するアニメーションを削除する
        _hideTweener?.Kill();

        float alpha = 1.0f;
        float time = 2.0f;

        foreach (var material in _meshRenderer.materials)
        {
            material
                .DOFade(alpha, time)
                .SetLink(this.gameObject);
        }

        int index = 0;
        _showTweener = _meshRendererFrame.materials[index]
                     .DOFade(alpha, time)
                     .SetLink(this.gameObject);
    }
}
