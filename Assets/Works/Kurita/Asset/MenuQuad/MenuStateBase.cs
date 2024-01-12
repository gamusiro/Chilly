using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    private List<Color> _quadUIOriginalColorList = new();

    //ステージ
    private List<StageInfo> _stageInfoList = new();
    private static int _stageIndex = 0;

    //BGM/SE
    private GameObject _bgmLine;//ライン
    private GameObject _seLine; //ライン

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
       // _quadUIOriginalColorList[stateType] = _quadUIList[stateType].material.GetColor("_EmissionColor");

        //色を変更する
        float intensity = 10.0f;
        float factor = Mathf.Pow(1.2f, intensity);
        _quadUIList[stateType].material.SetColor("_EmissionColor", Color.red * factor);
    }

    public void ChangeBlueColor(int stateType)
    {
        //元の色に戻す
       // _quadUIList[stateType].material.SetColor("_EmissionColor", _quadUIOriginalColorList[stateType]);
        _quadUIList[stateType].material.SetColor("_EmissionColor", Color.blue);
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
        float half = 0.5f;

        float left = _bgmLine.transform.localPosition.x + _bgmLine.transform.localScale.x * -half;
        float right = _bgmLine.transform.localPosition.x + _bgmLine.transform.localScale.x * half;

        float t = volume;
        float positionX = Mathf.Lerp(left,right,t);
        float duration = 0.5f;
        _quadUIList[(int)StateType.BGM].transform
            .DOLocalMoveX(positionX, duration)
            .SetLink(_quadUIList[(int)StateType.BGM].gameObject);

        Debug.Log("ポジション" + positionX);
    }

    public void SetSEVolume(float volume)
    {
        float half = 0.5f;
        float left = _seLine.transform.localScale.x * -half + _seLine.transform.localPosition.x;
        float right = _seLine.transform.localScale.x * half + _seLine.transform.localPosition.x;
        float t = volume;
        float position = Mathf.Lerp(left, right, t);
        float duration = 0.5f;
        _quadUIList[(int)StateType.SE].transform
            .DOMoveX(position, duration)
            .SetLink(_quadUIList[(int)StateType.SE].gameObject);
    }
}

public class MenuStateMachineBase<T> : MonoBehaviour where T : MenuStateMachineBase<T>
{
    private MenuStateBase<T> _currentState;
    private MenuStateBase<T> _nextState;

    //UI
    [SerializeField] protected List<Renderer> _quadUIList = new();

    //ステージ
    [SerializeField] protected List<StageInfo> _stageInfoList = new();

    //BGM/SE
    [SerializeField]private GameObject _bgmLine;//ライン
    [SerializeField] private GameObject _seLine; //ライン

    private void Update()
    {
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

        if (_currentState != null)
        {
            _currentState.OnUpdate();
        }
    }

    public bool SetNextState(MenuStateBase<T> nextState)
    {
        bool bRet = _nextState == null;
        _nextState = nextState;
        _nextState.SetQuadUIList(_quadUIList);
        _nextState.SetStageInfoList(_stageInfoList);
        _nextState.SetBGMLine(_bgmLine);
        _nextState.SetSELine(_seLine);
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
}

[System.Serializable]
public class StageInfo
{
    public Renderer Name;
    public Renderer Picture;
}
