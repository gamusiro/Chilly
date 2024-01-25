using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_PlayerJumpCollider : MonoBehaviour
{
    public bool m_isFlying;
    Rigidbody m_rigidBody;
    Animator m_animator;
    GameObject m_parent;

    public void Init(Rigidbody rb, Animator animator, GameObject obj)
    {
        m_isFlying = false;
        m_animator = animator;
        m_rigidBody = rb;
        m_parent = obj;
    }

    public bool IsFlying => m_isFlying;

    public void Fly()
    {
        m_isFlying = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Field")
        {
            m_isFlying = false;
            m_rigidBody.constraints |= RigidbodyConstraints.FreezePositionY;
            m_animator.Play("Running", 0, 0.0f);    // 最初から

            // プレイヤーのポジション調整
            Vector3 pos = m_parent.transform.position;
            pos.y = 0.2f;
            m_parent.transform.position = pos;
        }
    }
}
