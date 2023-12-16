using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SmallEnemyExplosion : MonoBehaviour
{
    [SerializeField] private GameObject _sphere;

    private void Start()
    {
        //Šg‘å‚µ‚Ä©g‚ğíœ‚·‚é
        Vector3 scale = new Vector3(100.0f, 100.0f, 100.0f);
        float time = 0.2f;
        _sphere.transform.DOScale(scale, time)
            .SetLink(this.gameObject);


        //”Z’W‚ğ”–‚­‚·‚é
        float alpha = 0.0f;
        _sphere.GetComponent<Renderer>().material.DOFade(alpha, time)
            .OnComplete(() => Destroy(this.gameObject))
            .SetLink(this.gameObject);
    }
}
