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
        while(true)
        {
            bool flag = _titleCameraPhaseManager.GetCanUpdate();
            _menuStateMachine.SetCanUpdate(flag);

            await UniTask.WaitForFixedUpdate();
        }
    }
}
