using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SO = Scriptable Object
/// </summary>
namespace Alex
{

[CreateAssetMenu(fileName = "New Item", menuName = "Alex/Create New Item")]
public class SOItem : ScriptableObject
{

    public GameObject itemPrefab;
    public Sprite sprite;
    public string names;
    public string description;

}

}


