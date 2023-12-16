using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SmallEnemyExplosion : MonoBehaviour
{
    [SerializeField] private GameObject _sphere;

    private void Start()
    {
        //�g�債�Ď��g���폜����
        Vector3 scale = new Vector3(100.0f, 100.0f, 100.0f);
        float time = 0.2f;
        _sphere.transform.DOScale(scale, time)
            .SetLink(this.gameObject);


        //�Z�W�𔖂�����
        float alpha = 0.0f;
        _sphere.GetComponent<Renderer>().material.DOFade(alpha, time)
            .OnComplete(() => Destroy(this.gameObject))
            .SetLink(this.gameObject);
    }
}
