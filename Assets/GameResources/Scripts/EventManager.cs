using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Обработчик событий
/// </summary>
public static class EventManager
{
    public delegate void EmptyEventHandler();
    public static event EmptyEventHandler OnViewFinish = delegate { };
    public static event EmptyEventHandler OnFinish = delegate { };
    public static event EmptyEventHandler OnStart = delegate { };
    public static event EmptyEventHandler OnCheckpoin = delegate { };

    public delegate void TransformEventHandler (Transform transform);
    public static event TransformEventHandler OnSpawnPlayer = delegate { };

    public delegate void ViewResultEventHandler(string name, string time);
    public static event ViewResultEventHandler OnViewResult = delegate { };

    public delegate void StringEventHandler(string name);
    public static event StringEventHandler OnFinishName = delegate { };

    public delegate void SpawnBotEventHandler(Transform transform, Color color, string name);
    public static event SpawnBotEventHandler OnSpawnBot = delegate { };

    /// <summary>
    /// Вызываем событие показа финиша
    /// </summary>
    public static void CallOnViewFinish()
    {
        OnViewFinish();
    }

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
    /// Вызываем событие прохождения чекпоинта
    /// </summary>
    public static void CallCheckpoint()
    {
        OnCheckpoin();
    }

    /// <summary>
    /// Вызываем событие спавна плеера
    /// </summary>
    public static void CallSpawnPlayer(Transform _transform)
    {
        OnSpawnPlayer(_transform);
    }

    /// <summary>
    /// Вызываем событие показа результата
    /// </summary>
    public static void CallViewResult(string _name, string _time)
    {
        OnViewResult(_name, _time);
    }

    /// <summary>
    /// Вызываем событие завершившего гонку
    /// </summary>
    public static void CallFinishName(string _name)
    {
        OnFinishName(_name);
    }
}