using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;


public class Menu : MonoBehaviour
{
    private enum SongName
    {
        WeMadeIt,
        Max
    }

    private enum Section
    {
        L,
        R,
        Return,
        Play,
        Tutorial,
        BGM,
        SE,
        MAX,
    }

    private enum InputDirection
    {
        None,
        Up,
        Left,
        Right,
        Down,
    }

    private Section _currentSection = new();//åªç›ÇÃëIëçÄñ⁄
    [SerializeField] private List<Renderer> _quadUIList;

    private void Start()
    {
        _currentSection = Section.L;
    }
}


