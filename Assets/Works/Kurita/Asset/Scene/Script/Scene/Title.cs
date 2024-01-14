using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class Title : MonoBehaviour
{
    [SerializeField] private TitleCameraPhaseManager _titleCameraPhaseManager;
    [SerializeField] private MenuStateMachine _menuStateMachine;

    private async void Start()
    {
        Cursor.visible = false;
        CS_AudioManager.Instance.PlayAudio("TitleAudio", true);

        // フェードインの処理
        _menuStateMachine.SetFadeIn(1.0f, () => { CS_AudioManager.Instance.MasterVolume = 1.0f; });

        while (true)
        {
            bool flag = _titleCameraPhaseManager.GetCanUpdate();
            _menuStateMachine.SetCanUpdate(flag);

            await UniTask.WaitForFixedUpdate();
        }
    }
}
