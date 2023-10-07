using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_EnemySample : MonoBehaviour
{
    [SerializeField, CustomLabel("�e��")]
    GameObject m_bulletObject;


    CS_NotesManager m_manager;


    /// <summary>
    /// ����������
    /// </summary>
    void Start()
    {
        m_manager = GetComponent<CS_NotesManager>();

        // ���ԂƍU���^�C�v����I�����āA�w��b����Ɏ��s���鏈����o�^���Ă���
        for(int i = 0; i < m_manager.m_noteNum; ++i)
        {
            StartCoroutine(Control(i));
        }
    }

    /// <summary>
    /// �R���[�`���֐��̒�`
    /// </summary>
    /// <param name="i">�m�[�c���ԍ�</param>
    /// <returns></returns>
    private IEnumerator Control(int i)
    {

        yield return new WaitForSeconds(m_manager.m_notesTime[i]);

        // �m�[�c�̃^�C�v�ɂ���čU�����@��ς���
        switch(m_manager.m_noteType[i])
        {
            default:
                Shoot(m_manager.m_laneNum[i]);
                break;
        }
    }

    /// <summary>
    /// �e�ۂ𔭎�
    /// </summary>
    /// <param name="laneNum"></param>
    void Shoot(int laneNum)
    {
        // �I�u�W�F�N�g�̐����|�W�V�����̍쐬
        Vector3 createPos = gameObject.transform.position;
        createPos.x += (m_bulletObject.transform.localScale.x * 2.0f) * (laneNum - 1);

        // �I�u�W�F�N�g�쐬
        GameObject obj = Instantiate(m_bulletObject, createPos, Quaternion.identity);

        // 5�b��ɔj��
        Destroy(obj, 5.0f);
    }
}
