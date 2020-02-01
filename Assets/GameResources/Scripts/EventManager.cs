using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Обработчик событий
/// </summary>
public static class EventManager
{
    public delegate void EmptyEventHandler();
    public static event EmptyEventHandler OnFinish = delegate { };
    public static event EmptyEventHandler OnStart = delegate { };

    /// <summary>
    /// Вызываем событие финиша
    /// </summary>
    public static void CallFinish()
    {
        OnFinish();
    }

    /// <summary>
    /// Вызываем событие старта
    /// </summary>
    public static void CallStart()
    {
        OnStart();
    }
}