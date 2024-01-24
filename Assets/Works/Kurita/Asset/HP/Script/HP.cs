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

    //private CinemachineBlend _cinemachineBlend;

    public bool Die
    {
        get { return !_isAlive; }
    }

    private void Start()
    {
        _hp = _meshRenderer.materials.Length - 1;//HPの値を設定する
        _isAlive = true;
        Recover();
    }

    private void FixedUpdate()
    {
        //座標をプレイヤーから少しずらす
        Vector3 offset = new Vector3(20.0f, 5.0f, 0.0f);
        this.transform.position = _player.transform.position + offset;

        //カメラのブレンド中は表示しない
        if (_cinemachineBrain.ActiveBlend != null)
        {
            Hide();
        }
        else
        {
            //向きをカメラに合わせる
            this.transform.LookAt(_gameCameraPhaseManager.GetCurCamera().transform.position);
            Show();
        }
            
    }

    public void Hit()
    {
        //やられていなければ処理を続行
        if (_hp - 1 < 0)
        {
            _isAlive = false;
            return;
        }

        //アニメーション
        float alpha = 0.0f;
        float time = 1.0f;

        _meshRenderer.materials[_hp].DOKill();
        _meshRenderer.materials[_hp].DOFade(alpha, time).SetLink(this.gameObject);

        //体力を減らす
        _hp--;
    }

    public void Recover()
    {
        //体力が上限になっていなければ処理を続行
        if (_hp >= _meshRenderer.materials.Length - 1)
            return;

        //体力を減らす
        _hp++;

        //アニメーション
        float alpha = 1.0f;
        float time = 1.0f;

        _meshRenderer.materials[_hp].DOKill();
        _meshRenderer.materials[_hp]
            .DOFade(alpha, time)
            .SetLink(this.gameObject);
    }

    public void Hide()
    {
        float alpha = 0.0f;
        float time = 2.5f;

        for (int i = 0; i < _meshRenderer.materials.Length; ++i)
        {
            _meshRenderer.materials[i].DOKill();
            _meshRenderer.materials[i]
                .DOFade(alpha, time)
                .SetLink(this.gameObject);
        }

        int index = 0;
        _meshRendererFrame.DOKill();
        _meshRendererFrame.materials[index]
                     .DOFade(alpha, time)
                     .SetLink(this.gameObject);
    }

    public void Show()
    {
        float alpha = 1.0f;
        float time = 2.0f;

        for(int i = 0; i <= _hp; ++i)
        {
            _meshRenderer.materials[i].DOKill();
            _meshRenderer.materials[i]
                .DOFade(alpha, time)
                .SetLink(this.gameObject);
        }

        int index = 0;
        _meshRendererFrame.DOKill();
        _meshRendererFrame.materials[index]
                     .DOFade(alpha, time)
                     .SetLink(this.gameObject);
    }
}
