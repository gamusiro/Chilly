using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemyNotesManager : NotesManager
{
    [Header("音楽を読み込む")]
    [SerializeField] private AudioSource _audioSourceCS; 

    [Header("小さい敵の生成スクリプト")]
    [Header("1.生成したいプレハブ")]
    [SerializeField] private GameObject _smallEnemyObject;
    [Header("2.スポーン位置")]
    [SerializeField] private List<Transform> _spawnTransformList;
    [Header("3.移動速度")]
    [SerializeField] private float _speed = 5.0f;
    [Header("4.プレイヤー位置")]
    [SerializeField] private Transform _playerTransform;

    private List<SmallEnemy> _smallEnemyList = new List<SmallEnemy>();//敵を格納する
    private int _spawnIndex = 0;//管理する敵のインデックス
    private int _startIndex = 0;//管理する敵のインデックス
    private int _destroyIndex = 0;//管理する敵のインデックス

    private int _index = 0;

    public override void Initialized()
    {
        //管理する敵のインデックスを更新
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

            if (_audioSourceCS.time >= notesTime.m_noteTime) //時間内であれば
            {
                _index++;
                // ノーツのタイプによって演出を変える
                switch (notesTime.m_noteType)
                {
                    case 0:
                        //生成する
                        CreateSmallEnemy();
                        break;
                    case 1:
                        //移動開始(スタート)
                        StartMoveSmallEnemy();
                        break;
                    case 2:
                        //消滅
                        DestroySmallEnemy();
                        break;
                }
            }
            else//制限時間外
            {
                break;
            }
        }
    }

//小さい敵の生成
void CreateSmallEnemy()
    {
        //エラー文
        if (_spawnTransformList.Count <= _spawnIndex || _spawnTransformList[_spawnIndex] == null) 
        {
            Debug.LogWarning("あれれ～？\n" +
                "「Jsonデータのノーツの数」と「エネミーのスポーン地点の数」が一致してないよ！\n"
                + "何のことかわからなければDiscordで聞いてね！");
            return;
        }

        //向きと移動量を決める
        Quaternion quaternion = new Quaternion();
        Vector3 velocity = new Vector3();

        if (_spawnTransformList[_spawnIndex].position.x < _playerTransform.position.x)
        {
            //右移動
            quaternion = Quaternion.AngleAxis(330, Vector3.up);
            velocity = new Vector3(_speed, 0.0f, 0.0f);
        }
        else
        {
            //左移動
            quaternion = Quaternion.AngleAxis(30, Vector3.up);
            velocity = new Vector3(-_speed, 0.0f, 0.0f);
        }

        //インスタンスの生成
        GameObject smallEnemyObject = Instantiate(_smallEnemyObject, _spawnTransformList[_spawnIndex].position, quaternion, this.transform);
        SmallEnemy smallEnemyCS = smallEnemyObject.GetComponent<SmallEnemy>();
        smallEnemyCS.Initialized(velocity);//初期化
        smallEnemyCS.SetThisObjectToBaseManager();//BaseManagerへセット
        _smallEnemyList.Add(smallEnemyCS);

        //管理する敵のインデックスを更新
        _spawnIndex++;
    }

    //小さい敵の移動開始フラグ
    void StartMoveSmallEnemy()
    {
        _smallEnemyList[_startIndex].StartMoving();
        _startIndex++;
    }

    //小さい敵の消滅フラグ
    void DestroySmallEnemy()
    {
        _smallEnemyList[_destroyIndex].Destroy();
        _destroyIndex++;
    }

}
