using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Сборщик мусора
/// </summary>
public class GarbageСollector : MonoBehaviour
{
    void Start()
    {
        System.GC.Collect();
    }
}
