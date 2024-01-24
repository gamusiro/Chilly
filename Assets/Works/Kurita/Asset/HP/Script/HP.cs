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
    [SerializeField] private MeshRenderer _meshRenderer;//HP�̃��b�V�������_���[
    [SerializeField] private MeshRenderer _meshRendererFrame;//HP�t���[���̃��b�V�������_���[
    private int _hp = 0;//���݂̗̑�
    private bool _isAlive;

    [SerializeField] private CS_Player _player;//�ʒu���v���C���[�ɍ��킹�邽��
    [SerializeField] private GameCameraPhaseManager _gameCameraPhaseManager;//�������J���������ɍ��킹�邽��
    [SerializeField] private CinemachineBrain _cinemachineBrain;

    //private CinemachineBlend _cinemachineBlend;

    public bool Die
    {
        get { return !_isAlive; }
    }

    private void Start()
    {
        _hp = _meshRenderer.materials.Length - 1;//HP�̒l��ݒ肷��
        _isAlive = true;
        Recover();
    }

    private void FixedUpdate()
    {
        //���W���v���C���[���班�����炷
        Vector3 offset = new Vector3(20.0f, 5.0f, 0.0f);
        this.transform.position = _player.transform.position + offset;

        //�J�����̃u�����h���͕\�����Ȃ�
        if (_cinemachineBrain.ActiveBlend != null)
        {
            Hide();
        }
        else
        {
            //�������J�����ɍ��킹��
            this.transform.LookAt(_gameCameraPhaseManager.GetCurCamera().transform.position);
            Show();
        }
            
    }

    public void Hit()
    {
        //����Ă��Ȃ���Ώ����𑱍s
        if (_hp - 1 < 0)
        {
            _isAlive = false;
            return;
        }

        //�A�j���[�V����
        float alpha = 0.0f;
        float time = 1.0f;

        _meshRenderer.materials[_hp].DOKill();
        _meshRenderer.materials[_hp].DOFade(alpha, time).SetLink(this.gameObject);

        //�̗͂����炷
        _hp--;
    }

    public void Recover()
    {
        //�̗͂�����ɂȂ��Ă��Ȃ���Ώ����𑱍s
        if (_hp >= _meshRenderer.materials.Length - 1)
            return;

        //�̗͂����炷
        _hp++;

        //�A�j���[�V����
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
