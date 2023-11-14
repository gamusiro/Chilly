using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MaterialSO")]
public class MaterialSO : ScriptableObject
{
    [SerializeField] public List<Material> Material;
}
