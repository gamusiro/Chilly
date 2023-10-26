using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class CS_NegativePiece : MonoBehaviour
{
    Vector3 m_velocity;
    GameObject m_player;

    private void Start()
    {
        m_player = CS_MoveController.Instance.GetPlayer();
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        transform.position += m_velocity * Time.deltaTime;
    }

    /// <summary>
    /// 速度の設定
    /// </summary>
    public void SetVelocity(Vector3 vel, float destroyTime)
    {
        m_velocity = vel;
        Destroy(gameObject, destroyTime);
    }
}
