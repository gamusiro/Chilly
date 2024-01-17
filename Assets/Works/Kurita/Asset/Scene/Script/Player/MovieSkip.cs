using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class MovieSkip : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _text;
    private bool _showText = false;

    private async void Start()
    {
        //�F�𔖂�����
        {
            float alpha = 0.0f;
            float duration = 0.0f;
            _text
                .DOFade(alpha, duration)
                .SetLink(this.gameObject);
        }

        while (true)
        {
            //�v���X�{�^���������ꂽ�Ƃ��@��
            await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.K));

            if (!_showText)
            {
                _showText = true;

                //�F��Z������
                {
                    float alpha = 1.0f;
                    float duration = 0.5f;
                    _text
                        .DOFade(alpha, duration)
                        .OnComplete(() => HideText())
                        .SetLink(this.gameObject);
                }
            }
            else if(_showText)
            {
                //�V�[���J�ځ@��
                Debug.Log("�V�[�����J�ڂ��܂����B");
            }
        }
    }

    private async void HideText()
    {
        float timeSpan = 4.0f;
        await UniTask.Delay(System.TimeSpan.FromSeconds(timeSpan));

        //�F��Z������
        {
            float alpha = 0.0f;
            float duration = 0.5f;
            _text.DOKill();
            _text
                .DOFade(alpha, duration)
                .OnComplete(() => _showText = false)
                .SetLink(this.gameObject);
        }
    }
}
