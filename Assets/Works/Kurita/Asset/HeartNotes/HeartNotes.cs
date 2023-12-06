using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeartNotes : MonoBehaviour
{
    private CameraPhaseManager m_mainGameCameraManager;
    private CS_Player _player;

    private GameObject m_enemyObject;
    private GameObject m_cameraObject;
     
    private Vector3 m_rootRight;
    private Vector3 m_rootLeft;
    private Vector3 m_useRoot;

    private bool m_getted;
    private float m_work;

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        m_work = 0.0f;

        m_rootRight = new Vector3(200.0f, 100.0f, 0.0f);
        m_rootLeft = new Vector3(-200.0f, 100.0f, 0.0f);
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void FixedUpdate()
    {
        if (m_getted)
        {
            m_work += 0.02f;

            Vector3 offset = new Vector3(0.0f, 0.0f, 10.0f);    // エネミーの後ろまで行くようにしたいので

            Vector3 a = Vector3.Lerp(gameObject.transform.localPosition, m_cameraObject.transform.localPosition, m_work);
            Vector3 b = Vector3.Lerp(a, m_useRoot, m_work);
            Vector3 c = Vector3.Lerp(b, m_enemyObject.transform.localPosition + offset, m_work);

            gameObject.transform.localPosition = c;

            if(m_work >= 1.0f)
                Destroy(gameObject);
        }
    }

    /// <summary>
    /// 衝突したら
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        //プレイヤーがスピンしていなければ返る
        if (!_player.GetSpin())
            return;

        if (other.gameObject.tag == "Player")
        {
            gameObject.transform.parent = other.gameObject.transform.parent;

            // 経由するルートを確定する
            if (gameObject.transform.position.x > 0)
                m_useRoot = m_rootRight;
            else
                m_useRoot = m_rootLeft;

            m_getted = true;
            gameObject.transform.localPosition = other.gameObject.transform.localPosition;

            m_enemyObject = CS_MoveController.GetObject("GameEnemy");
            m_cameraObject = m_mainGameCameraManager.GetCurCamera();
        }

        if (other.gameObject.tag == "GameEnemy")
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// 衝突した
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionStay(Collision collision)
    {
        //プレイヤーがスピンしていなければ返る
        if (!_player.GetSpin())
            return;

        if (collision.gameObject.tag == "Player")
        {
            gameObject.transform.parent = collision.gameObject.transform.parent;

            // 経由するルートを確定する
            if (gameObject.transform.position.x > 0)
                m_useRoot = m_rootRight;
            else
                m_useRoot = m_rootLeft;

            m_getted = true;
            gameObject.transform.localPosition = collision.gameObject.transform.localPosition;

            m_enemyObject = CS_MoveController.GetObject("GameEnemy");
            m_cameraObject = m_mainGameCameraManager.GetCurCamera();
        }

        if (collision.gameObject.tag == "GameEnemy")
        {
            Debug.Log("このオブジェクトは消されました");
            Destroy(this.gameObject);
        }
    }

    public void SetMainGameCameraManager(CameraPhaseManager cameraManager)
    {
        m_mainGameCameraManager = cameraManager;
    }    
    
    public void SetPlayer(CS_Player player)
    {
        _player = player;
    }
}
