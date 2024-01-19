using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using System;
using UnityEngine.InputSystem;

//whereは制約を設けること。
//<T>は「MenuStateMachine」を継承していないといけませんよっていう制約。
public enum SongName
{
    WeMadeIt,
    Max,
}

public enum StateType
{
    LeftTriangle,
    RightTriangle,
    Play,
    BGM,
    SE,
    MAX,
}

public class MenuStateBase<T> where T : MenuStateMachineBase<T>
{
    //ステートマシーン
    public MenuStateBase(T _machine) { machine = _machine; }
    protected T machine;

    //UI
    protected List<Renderer> _quadUIList = new();
    private Color[] _quadUIOriginalColorList = new Color[5];

    //ステージ
    private List<StageInfo> _stageInfoList = new();
    private static int _stageIndex = 0;

    //BGM/SE
    private GameObject _bgmLine;//ライン
    private GameObject _seLine; //ライン

    //リミット
    private Transform _bgmLineLeftRimit;
    private Transform _bgmLineRightRimit;
    private Transform _seLineLeftRimit;
    private Transform _seLineRightRimit;

    //更新フラグ
    protected bool _canUpdate;

    // 入力
    protected PlayerInput _input;

    public virtual void OnEnter() { }
    public virtual void OnUpdate() { }
    public virtual void OnExit() { }

    public void SetQuadUIList(List<Renderer> quadUiList)
    {
        _quadUIList = quadUiList;
    }

    public void SetStageInfoList(List<StageInfo> stageInfoList)
    {
        _stageInfoList = stageInfoList;
    }

    public void ChangeRedColor(int stateType)
    {
        //元の色を保存する
        _quadUIOriginalColorList[stateType] = _quadUIList[stateType].material.GetColor("_EmissionColor");

        //色を変更する
        float intensity = 10.0f;
        float factor = Mathf.Pow(1.2f, intensity);
        _quadUIList[stateType].material.SetColor("_EmissionColor", Color.red * factor);
    }

    public void ChangeBlueColor(int stateType)
    {
        //元の色に戻す
        _quadUIList[stateType].material.SetColor("_EmissionColor", _quadUIOriginalColorList[stateType]);
    }

    //真なら次のステージ、偽なら一つ前のステージに遷移する
    public void NextStage(bool transition)
    {
        //現在のステージ情報を非表示にする
        float alpha = 0.0f;
        float duration = 0.5f;

        _stageInfoList[_stageIndex].Name.material
            .DOFade(alpha, duration)
            .SetLink(_stageInfoList[_stageIndex].Name.gameObject);

        _stageInfoList[_stageIndex].Picture.material
            .DOFade(alpha, duration)
            .SetLink(_stageInfoList[_stageIndex].Picture.gameObject);

        //インデックスの変更
        if (transition)
        {
            _stageIndex++;
            if (_stageIndex > _stageInfoList.Count - 1)
                _stageIndex = 0;
        }
        else
        {
            _stageIndex--;
            if (_stageIndex < 0)
                _stageIndex = _stageInfoList.Count - 1;
        }

        //新たなステージの情報を表示する
        alpha = 1.0f;

        _stageInfoList[_stageIndex].Name.material
            .DOFade(alpha, duration)
            .SetLink(_stageInfoList[_stageIndex].Name.gameObject);

        _stageInfoList[_stageIndex].Picture.material
            .DOFade(alpha, duration)
            .SetLink(_stageInfoList[_stageIndex].Picture.gameObject);

        // オーディオの入れ替え
        StageInfo info = _stageInfoList[_stageIndex];
        CS_AudioManager.Instance.PlayAudioAndFadeBeteenTime(info.AudioName, info.StartTime, info.EndTime);
    }

    public void SetBGMLine(GameObject line)
    {
        _bgmLine = line;
    }

    public void SetSELine(GameObject line)
    {
        _seLine = line;
    }

    public void SetBGMVolume(float volume)
    {
        float t = volume;
        float positionX = Mathf.Lerp(_bgmLineLeftRimit.localPosition.x, _bgmLineRightRimit.localPosition.x, t);
        float duration = 0.5f;
        _quadUIList[(int)StateType.BGM].transform
            .DOLocalMoveX(positionX, duration)
            .SetLink(_quadUIList[(int)StateType.BGM].gameObject);
    }

    public void SetSEVolume(float volume)
    {
        float t = volume;
        float positionX = Mathf.Lerp(_seLineLeftRimit.localPosition.x, _seLineRightRimit.localPosition.x, t);
        float duration = 0.5f;
        _quadUIList[(int)StateType.SE].transform
            .DOLocalMoveX(positionX, duration)
            .SetLink(_quadUIList[(int)StateType.SE].gameObject);
    }

    public void SetLineRimit(Transform bgmLeft, Transform bgmRight, Transform seLeft, Transform seRight)
    {
        _bgmLineLeftRimit = bgmLeft;
        _bgmLineRightRimit = bgmRight;
        _seLineLeftRimit = seLeft;
        _seLineRightRimit = seRight;
    }

    public void SetCanUpdate(bool flag)
    {
        _canUpdate = flag;
    }

    public void SetPlayerInput(PlayerInput input)
    {
        _input = input;
    }

    public StageInfo GetSelectStage()
    {
        return _stageInfoList[_stageIndex];
    }
}

public class MenuStateMachineBase<T> : MonoBehaviour where T : MenuStateMachineBase<T>
{
    private MenuStateBase<T> _currentState;
    private MenuStateBase<T> _nextState;
    protected static int _currentStageIndex = 0;

    //UI
    [SerializeField] protected List<Renderer> _quadUIList = new();

    //ステージ
    [SerializeField] protected List<StageInfo> _stageInfoList = new();

    //BGM/SE
    [SerializeField] private GameObject _bgmLine;//ライン
    [SerializeField] private GameObject _seLine; //ライン

    //リミット
    [SerializeField] private Transform _bgmLineLeftRimit;
    [SerializeField] private Transform _bgmLineRightRimit;
    [SerializeField] private Transform _seLineLeftRimit;
    [SerializeField] private Transform _seLineRightRimit;

    //フェード
    [SerializeField] private Fade _fade;
    //入力
    [SerializeField] protected PlayerInput _input;
    //カメラ
    [SerializeField] protected CinemachineBrain _brainCamera;

    private void Start()
    {
        //子クラスでオーバーライドしてるため、これは呼ばれない
    }

    private void Update()
    {
        // 音量フェード
        if (_fade.GetState() != Fade.STATE.NONE)
        {
            float tmp = _fade.GetRange();
            CS_AudioManager.Instance.FadeVolume(tmp);
        }
        else
        {
            if (_currentState != null && _brainCamera.ActiveBlend == null) 
            {
                _currentState.OnUpdate();
            }
        }
    }

    protected void SetStageInfo()
    {
        {
            //全てのステージ情報を非表示にする
            float alpha = 0.0f;
            float duration = 0.0f;

            for (int i = 0; i < _stageInfoList.Count; i++)
            {
                _stageInfoList[i].Name.material
                    .DOFade(alpha, duration)
                    .SetLink(_stageInfoList[i].Name.gameObject);

                _stageInfoList[i].Picture.material
                    .DOFade(alpha, duration)
                    .SetLink(_stageInfoList[i].Picture.gameObject);
            }
        }

        {
            //現在のステージのみ表示する
            float alpha = 1.0f;
            float duration = 0.0f;

            _currentStageIndex = 0;
            _stageInfoList[_currentStageIndex].Name.material
                .DOFade(alpha, duration)
                .SetLink(_stageInfoList[_currentStageIndex].Name.gameObject);

            _stageInfoList[_currentStageIndex].Picture.material
                .DOFade(alpha, duration)
                .SetLink(_stageInfoList[_currentStageIndex].Picture.gameObject);
        }
    }

    public bool SetNextState(MenuStateBase<T> nextState, bool se = true)
    {
        // カーソル音を鳴らす
        if(se)
            CS_AudioManager.Instance.PlayAudio("Cursor");

        bool bRet = _nextState == null;
        _nextState = nextState;
        _nextState.SetQuadUIList(_quadUIList);
        _nextState.SetStageInfoList(_stageInfoList);
        _nextState.SetBGMLine(_bgmLine);
        _nextState.SetSELine(_seLine);
        _nextState.SetLineRimit(_bgmLineLeftRimit, _bgmLineRightRimit, _seLineLeftRimit, _seLineRightRimit);
        _nextState.SetPlayerInput(_input);

        //現在のステートを次のステートに更新する
        if (_nextState != null)
        {
            if (_currentState != null)
            {
                _currentState.OnExit();
            }

            _currentState = _nextState;
            _currentState.OnEnter();
            _nextState = null;
        }

        return bRet;
    }    

    public void SetBGMVolume(float volume)
    {
        _currentState.SetBGMVolume(volume);
    }

    public void SetSEVolume(float volume)
    {
        _currentState.SetSEVolume(volume);
    }

    public void SetCanUpdate(bool flag)
    {
        _currentState?.SetCanUpdate(flag);
    }

    public void SetFadeIn(float time, Action act)
    {
        _fade.FadeIn(time, act);
    }

    public void SetFadeOut(float time, Action act)
    {
        _fade.FadeOut(time, act);
    }
}

[System.Serializable]
public class StageInfo
{
    public string AudioName;
    public float StartTime;
    public float EndTime;
    public Renderer Name;
    public Renderer Picture;
}
