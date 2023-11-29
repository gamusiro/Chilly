using System.Collections;
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using DG.Tweening;

public class Credit : MonoBehaviour
{
    [SerializeField] private List<CreditInfo> _creditInfoList = new();
    private float _time = 0.0f;

    private void Start()
    {
        Text();
    }

    private void Text()
    {
        //�k���`�F�b�N
        if (_creditInfoList.Count == 0)
        {
            Debug.LogWarning("�N���W�b�g���X�g����ł�");
            return;
        }

        //������
        float alpha = 0.0f;
        foreach (var creditInfo in _creditInfoList){ creditInfo.TMP.alpha = alpha; }

        //�N���W�b�g�\��
        foreach (var creditInfo in _creditInfoList){ Draw(creditInfo); }
    }

    private async void Draw(CreditInfo creditInfo)
    {
        float fadeTime = 0.5f;
        float alpha;

        //�����̃t�F�[�h�C��
        await UniTask.WaitUntil(() => _time > creditInfo.StartTime);
        alpha = 1.0f;
        creditInfo.TMP.DOFade(alpha, fadeTime).SetLink(gameObject);

        //�����̃t�F�[�h�A�E�g
        await UniTask.WaitUntil(() => _time > creditInfo.StartTime + creditInfo.DisplayTime);
        alpha = 0.0f;
        creditInfo.TMP.DOFade(alpha, fadeTime).SetLink(gameObject);
    }

    private void FixedUpdate()
    {
        _time += Time.deltaTime;
    }
}

//��b��񃊃X�g
[Serializable]
public class CreditInfo
{
    [CustomLabel("������\������TMP")]
    public TMPro.TextMeshProUGUI TMP;
    [CustomLabel("�\���J�n����")]//(Instance��������v��)
    public float StartTime;
    [CustomLabel("�\������")]
    public float DisplayTime;
}
