using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class Title : MonoBehaviour
{
    [SerializeField] private TitleCameraPhaseManager _titleCameraPhaseManager;
    [SerializeField] private MenuStateMachine _menuStateMachine;
    bool _play;

    private void Start()
    {
        Cursor.visible = false;
        CS_AudioManager.Instance.PlayAudio("TitleAudio", true);

        // �t�F�[�h�C���̏���
        _menuStateMachine.SetFadeIn(1.0f, () => { CS_AudioManager.Instance.MasterVolume = 1.0f; });
        _play = false;
    }

    private void Update()
    {
        bool flag = _titleCameraPhaseManager.GetCanUpdate();
        _menuStateMachine.SetCanUpdate(flag);

        if (flag && !_play)
        {
            _menuStateMachine.SetMenuAudio();
            _play = true;
        }
    }
}
