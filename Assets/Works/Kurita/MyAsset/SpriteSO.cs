using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpriteSO")]
public class SpriteSO : ScriptableObject
{
    [SerializeField] public List<Sprite> sprite;
}
