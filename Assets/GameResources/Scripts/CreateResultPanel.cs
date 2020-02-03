using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Выводим результат конкретного гонщика
/// </summary>
public class CreateResultPanel : MonoBehaviour
{
    [SerializeField]
    private Transform parent = null;

    [SerializeField]
    private ViewResult prefabViewPanel = null;

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

    /// <summary>
    /// Обработчик события появления результата
    /// </summary>
    /// <param name="_name"></param>
    private void OnViewResult (string _name, string _time)
    {
        CreatePanel (_name,_time);
    }
    #endregion

    private void CreatePanel(string _name, string _time)
    {
        ViewResult newViewResult = Instantiate(prefabViewPanel, parent);
        newViewResult.name = "Result " + _name;
        newViewResult.Init(_name,_time);
    }
}