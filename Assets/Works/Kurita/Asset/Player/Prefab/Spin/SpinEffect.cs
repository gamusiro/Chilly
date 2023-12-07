using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class SpinEffect : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    [SerializeField] private Renderer _renderer;

    private void Start()
    {
        float time = 0.5f;

        //Šg‘å
        Vector3 scale = this.transform.localScale;
        float max = 3.0f;
        scale.x *= max;
        scale.z *= max;
        _object.transform.DOScale(scale, time)
            .SetLink(this.gameObject);

        //‰ñ“]
        Vector3 rotate = new Vector3(0.0f, 360.0f, 0.0f);
        _object.transform.DORotate(rotate, time, RotateMode.FastBeyond360)
            .SetLink(this.gameObject);

        //ƒtƒF[ƒh
        float alpha = 0.0f;
        _renderer.material.DOFade(alpha, time)
            .SetLink(this.gameObject)
            .OnComplete(() => Destroy(this.gameObject));
    }
}
