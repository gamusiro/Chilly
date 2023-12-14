using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_HealthHeart : MonoBehaviour
{
    private HP m_hp;


    public void Initialize(HP hp)
    {
        m_hp = hp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            // ‰¹‚ð–Â‚ç‚·
            CS_AudioManager.Instance.PlayAudio("Heal");

            m_hp.Recover();
            Destroy(gameObject);
        }
    }
}
