using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemyNotesManager : NotesManager
{
    [Header("�������G�̐����X�N���v�g")]
    [Header("1.�����������v���n�u")]
    [SerializeField] private GameObject _smallEnemyObject;
    [Header("2.�X�|�[���ʒu")]
    [SerializeField] private List<Transform> _spawnTransformList;
    [Header("3.�ړ����x")]
    [SerializeField] private float _speed = 5.0f;
    [Header("4.�v���C���[�ʒu")]
    [SerializeField] private Transform _playerTransform;

    private List<SmallEnemy> _smallEnemyList = new List<SmallEnemy>();//�G���i�[����
    private int _spawnIndex = 0;//�Ǘ�����G�̃C���f�b�N�X
    private int _startIndex = 0;//�Ǘ�����G�̃C���f�b�N�X
    private int _destroyIndex = 0;//�Ǘ�����G�̃C���f�b�N�X

    public override void Initialized()
    {
        // ���ԂƍU���^�C�v����I�����āA�w��b����Ɏ��s���鏈����o�^���Ă���
        for (int i = 0; i < m_noteNum; ++i)
        {
            StartCoroutine(Control(i));
        }

        //�Ǘ�����G�̃C���f�b�N�X���X�V
        _spawnIndex = 0;
        _startIndex = 0;
        _destroyIndex = 0;
    }

    //�m�[�c�̃^�C�v
    private IEnumerator Control(int i)
    {
        yield return new WaitForSeconds(m_notesTime[i]);

        // �m�[�c�̃^�C�v�ɂ���ĉ��o��ς���
        switch(m_noteType[i])
        {
            case 0:
                CreateSmallEnemy();
                break;
            case 1:
                StartMoveSmallEnemy();
                break;
            case 2:
                DestroySmallEnemy();
                break;
        }
    }

    //�������G�̐���
    void CreateSmallEnemy()
    {
        //�G���[��
        if (_spawnTransformList.Count <= _spawnIndex || _spawnTransformList[_spawnIndex] == null) 
        {
            Debug.LogWarning("�����`�H\n" +
                "�uJson�f�[�^�̃m�[�c�̐��v�Ɓu�G�l�~�[�̃X�|�[���n�_�̐��v����v���ĂȂ���I\n"
                + "���̂��Ƃ��킩��Ȃ����Discord�ŕ����ĂˁI");
            return;
        }

        //�����ƈړ��ʂ����߂�
        Quaternion quaternion = new Quaternion();
        Vector3 velocity = new Vector3();

        if (_spawnTransformList[_spawnIndex].position.x < _playerTransform.position.x)
        {
            //�E�ړ�
            quaternion = Quaternion.AngleAxis(330, Vector3.up);
            velocity = new Vector3(_speed, 0.0f, 0.0f);
        }
        else
        {
            //���ړ�
            quaternion = Quaternion.AngleAxis(30, Vector3.up);
            velocity = new Vector3(-_speed, 0.0f, 0.0f);
        }

        //�C���X�^���X�̐���
        GameObject smallEnemyObject = Instantiate(_smallEnemyObject, _spawnTransformList[_spawnIndex].position, quaternion, this.transform);
        SmallEnemy smallEnemyCS = smallEnemyObject.GetComponent<SmallEnemy>();
        smallEnemyCS.Initialized(velocity);//������
        smallEnemyCS.SetThisObjectToBaseManager();//BaseManager�փZ�b�g
        _smallEnemyList.Add(smallEnemyCS);

        //�Ǘ�����G�̃C���f�b�N�X���X�V
        _spawnIndex++;
    }

    //�������G�̈ړ��J�n�t���O
    void StartMoveSmallEnemy()
    {
        _smallEnemyList[_startIndex].StartMoving();
        _startIndex++;
    }

    //�������G�̏��Ńt���O
    void DestroySmallEnemy()
    {
        _smallEnemyList[_destroyIndex].Destroy();
        _destroyIndex++;
    }

}
