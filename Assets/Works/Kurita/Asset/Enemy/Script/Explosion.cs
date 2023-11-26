using UnityEngine;
using DG.Tweening;

public class Explosion : MonoBehaviour
{
    [SerializeField] private Vector3 _maxScale;
    [SerializeField] private float _time;

    private void Start()
    {
        this.transform.DOScale(_maxScale, _time).SetLink(gameObject);
    }
}
