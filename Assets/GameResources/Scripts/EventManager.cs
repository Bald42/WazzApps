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

    public delegate void TransformEventHandler (Transform transform);
    public static event TransformEventHandler OnSpawnPlayer = delegate { };

    public delegate void SpawnBotEventHandler(Transform transform, Color color, string name);
    public static event SpawnBotEventHandler OnSpawnBot = delegate { };

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

    /// <summary>
    /// Вызываем событие спавна плеера
    /// </summary>
    public static void CallSpawnPlayer(Transform _transform)
    {
        OnSpawnPlayer(_transform);
    }
}