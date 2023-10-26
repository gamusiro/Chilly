using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    [SerializeField] private SpriteSO _spriteList;//spriteList
    [SerializeField] private Image _image;//�\���摜
    [SerializeField] private int _hp;

    //�t�F�[�h�A�j���[�V�����n
    private bool _animeFlag;//�A�j���[�V���������̔���
    private float _alpha;//���l
    private float _curflashNum;//���݂̓_�ŉ�
    private const float MAX_FLASH_NUM = 3.0f;//�ő�̓_�ŉ�

    private void Start()
    {
        _hp = _spriteList.sprite.Count;
        _animeFlag = false;
        _curflashNum = 0;
    }

    private void Update()
    {
        
    }

    private void Hit()
    {
        //�̗͂����炷
        _hp--;

        //���ꂽ����
        if (_hp <= 0)
            return;

        //HP�����X�V
        SetHitAnimation();
    }

    private void SetHitAnimation()
    {
        _animeFlag = true;
        _alpha = 1.0f;
        _curflashNum = MAX_FLASH_NUM;
    }
    private void HitAnimation()
    {
        if(_animeFlag)
        {
            if (_curflashNum > 0)
            {
                _alpha -= 0.1f;

                if (_alpha <= 0.0f)
                {
                    _alpha = 1.0f;
                    _curflashNum--;
                }
            }
            else 
            {
            _image.sprite = _spriteList.sprite[_hp];
            
            }
        }
    }
}
