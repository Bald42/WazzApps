using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Контроллер интерфейса
/// </summary>
public class UIController : MonoBehaviour
{
    [SerializeField]
    private AnimScale buttonControl = null;

    [SerializeField]
    private AnimScale finishPanel = null;

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
        EventManager.OnFinish += OnFinish;
    }

    /// <summary>Отписки</summary>
    private void UnSubscribe()
    {
        EventManager.OnFinish -= OnFinish;
    }

    private void OnFinish()
    {
        buttonControl.Active(false);
        finishPanel.Active(true);
    }
    #endregion
}