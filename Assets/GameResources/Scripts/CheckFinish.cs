using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Проверяем проходение
/// </summary>
public class CheckFinish : MonoBehaviour
{
    [SerializeField]
    private int maxCheckPoint = 2;

    private int currentCheckpoint = 0;

    #region Subscribes / UnSubscribes
    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        UnSubscribe();
    }

    /// <summary>Подписки</summary>
    private void Subscribe()
    {
        EventManager.OnCheckpoin += OnCheckpoin;
        EventManager.OnFinish += OnFinish;
    }

    /// <summary>Отписки</summary>
    private void UnSubscribe()
    {
        EventManager.OnCheckpoin -= OnCheckpoin;
        EventManager.OnFinish -= OnFinish;
    }
    
    private void OnCheckpoin ()
    {
        currentCheckpoint--;
    }

    private void OnFinish ()
    {
        if (currentCheckpoint == 0)
        {
            EventManager.CallOnViewFinish();
        }
    }
    #endregion

    private void Awake()
    {
        Init();
    }


    private void Init ()
    {
        currentCheckpoint = maxCheckPoint;
    }
}