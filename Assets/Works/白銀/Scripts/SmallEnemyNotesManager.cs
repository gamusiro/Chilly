using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemyNotesManager : NotesManager
{
    [Header("���y��ǂݍ���")]
    [SerializeField] private AudioSource _audioSourceCS; 

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

    private int _index = 0;

    public override void Initialized()
    {
        //�Ǘ�����G�̃C���f�b�N�X���X�V
        _spawnIndex = 0;
        _startIndex = 0;
        _destroyIndex = 0;

        _index = 0;
    }

    private void Update()
    {

        for (int i = _index; i < m_notesTimesList.Count; i++)
        {
            NotesTime notesTime = m_notesTimesList[_index];

            if (_audioSourceCS.time >= notesTime.m_noteTime) //���ԓ��ł����
            {
                _index++;
                // �m�[�c�̃^�C�v�ɂ���ĉ��o��ς���
                switch (notesTime.m_noteType)
                {
                    case 0:
                        //��������
                        CreateSmallEnemy();
                        break;
                    case 1:
                        //�ړ��J�n(�X�^�[�g)
                        StartMoveSmallEnemy();
                        break;
                    case 2:
                        //����
                        DestroySmallEnemy();
                        break;
                }
            }
            else//�������ԊO
            {
                break;
            }
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
