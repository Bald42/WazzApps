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

    [SerializeField]
    private AnimScale mapPanel = null;

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
        EventManager.OnViewFinish += OnViewFinish;
    }

    /// <summary>Отписки</summary>
    private void UnSubscribe()
    {
        EventManager.OnViewFinish -= OnViewFinish;
    }

    private void OnViewFinish()
    {
        buttonControl.Active(false);
        mapPanel.Active(false);
        finishPanel.Active(true);
    }
    #endregion
}