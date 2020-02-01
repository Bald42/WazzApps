using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Контейнер путей
/// </summary>
[CreateAssetMenu(fileName = "PathsContainer", menuName = "ScriptableObjects/PathsContainer", order = 1)]
public class PathsContainer : ScriptableObject
{
    public List <Path> Paths = new List <Path>();
}