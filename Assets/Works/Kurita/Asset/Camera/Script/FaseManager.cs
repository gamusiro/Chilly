using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class FaseManager : MonoBehaviour
{
    private int _faseIndex;
    private List<GameObject> _faseInstanceList = new List<GameObject>();

    [Header("フェーズプレハブ")]
    [SerializeField] List<GameObject> _fasePrefabList = new List<GameObject>(); 

    private void Start()
    {
        _faseIndex = 0;
        _faseInstanceList.Add(Instantiate(_fasePrefabList[_faseIndex]));
    }

    private void Update()
    {
        //フェーズの遷移を管理する
        FaseTransition();
    }

    private void FaseTransition()
    {
        //※デバッグ用
        if (!Input.GetMouseButtonDown(0))
          return;

        //遷移方法の設定
        //真ならば瞬時に切り替える・偽ならば滑らかに切り替える
        if (_faseInstanceList[_faseIndex].GetComponent<FaseParent>().InstantaneousTransition())
        {
            foreach (var faseInstance in _faseInstanceList)
            {
                Destroy(faseInstance);
            }
            _faseInstanceList.Clear();
        }

        //次のフェーズの生成
        _faseIndex++;
        _faseInstanceList.Add(Instantiate(_fasePrefabList[_faseIndex]));
    }
}