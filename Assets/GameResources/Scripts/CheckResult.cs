using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Отправляем результат
/// </summary>
public class CheckResult : MonoBehaviour
{
    [SerializeField]
    private Transform parent = null;

    [SerializeField]
    private ViewResult viewResult = null;

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
        EventManager.OnViewResult += OnViewResult;
    }

    /// <summary>Отписки</summary>
    private void UnSubscribe()
    {
        EventManager.OnViewResult -= OnViewResult;
    }

    private void OnViewResult(string _name)
    {

    }
    #endregion
}