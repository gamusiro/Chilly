using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class FaseManager : MonoBehaviour
{
    private int _faseIndex;
    private List<GameObject> _faseInstanceList = new List<GameObject>();

    [Header("�t�F�[�Y�v���n�u")]
    [SerializeField] List<GameObject> _fasePrefabList = new List<GameObject>(); 

    private void Start()
    {
        _faseIndex = 0;
        _faseInstanceList.Add(Instantiate(_fasePrefabList[_faseIndex]));
    }

    private void Update()
    {
        //�t�F�[�Y�̑J�ڂ��Ǘ�����
        FaseTransition();
    }

    private void FaseTransition()
    {
        //���f�o�b�O�p
        if (!Input.GetMouseButtonDown(0))
          return;

        //�J�ڕ��@�̐ݒ�
        //�^�Ȃ�Ώu���ɐ؂�ւ���E�U�Ȃ�Ί��炩�ɐ؂�ւ���
        if (_faseInstanceList[_faseIndex].GetComponent<FaseParent>().InstantaneousTransition())
        {
            foreach (var faseInstance in _faseInstanceList)
            {
                Destroy(faseInstance);
            }
            _faseInstanceList.Clear();
        }

        //���̃t�F�[�Y�̐���
        _faseIndex++;
        _faseInstanceList.Add(Instantiate(_fasePrefabList[_faseIndex]));
    }
}