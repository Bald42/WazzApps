using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Очищаем префсы
/// </summary>
public class ClearPrefs : MonoBehaviour
{
    /// <summary>
    /// Очищаем
    /// </summary>
    public void Clear ()
    {
        PlayerPrefs.DeleteAll();
    }
}