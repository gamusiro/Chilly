using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

//ToDo:演出が決定してから制作再開

//フェーズクラスの親
public class FaseParent : MonoBehaviour
{
    [SerializeField] protected CinemachineVirtualCamera _cinemachineVirtualCamera;
    [SerializeField] protected Image _fadeImage;
    [SerializeField] protected LerpSO _fadeImageAlphaLerpSO;

    //protected void Awake()
    //{
    //    //カメラの優先度設定
    //    _cinemachineVirtualCamera.Priority = 1;

    //    //フェード
    //    //_fadeImageAlphaLerpSO = new LerpSO();
    //    //_fadeImageAlphaLerpSO.StartValue = 1.0f;
    //    //_fadeImageAlphaLerpSO.EndValue = 0.0f;
    //    //_fadeImageAlphaLerpSO.CurrentPoint = 0.9f;
    //    //_fadeImageAlphaLerpSO.Speed = 0.05f;
    //    //  ChangeAlpha(_fadeImage, 1.0f);
    //    Debug.LogError("呼び出される");
    //}

    //protected void OnDestroy()
    //{
    //    //カメラの優先度設定
    //    _cinemachineVirtualCamera.Priority = 0;
    //}

    //オブジェクトの回転をさせ続ける
    protected void RotateObject(Transform transform, float speed)
    {
        Quaternion addAngle = Quaternion.Euler(0.0f, speed, 0.0f);
        transform.rotation = addAngle * transform.rotation;
    }

    //オブジェクトの移動させ続ける
    protected void MoveObject(Transform transform, float speed)
    {
        Vector3 position = transform.position;
        Vector3 addVelocity = new Vector3(0.0f, speed, 0.0f);
        transform.position += addVelocity;
    }

    //カメラの暗転
    protected void FadeOut()
    {
        _fadeImageAlphaLerpSO.CurrentPoint += _fadeImageAlphaLerpSO.Speed * Time.deltaTime / (_fadeImageAlphaLerpSO.StartValue - _fadeImageAlphaLerpSO.EndValue);
        float alpha = Mathf.Lerp(_fadeImageAlphaLerpSO.StartValue, _fadeImageAlphaLerpSO.EndValue,
            _fadeImageAlphaLerpSO.CurrentPoint);
        ChangeAlpha(_fadeImage, alpha);
    }

    //α値の変更
    protected void ChangeAlpha(Image image, float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }

    //true:カメラの遷移を一瞬で行う
    //false:カメラの遷移を滑らかに行う
    public virtual bool InstantaneousTransition()
    {
       return true;
       //return false;
    }
}

