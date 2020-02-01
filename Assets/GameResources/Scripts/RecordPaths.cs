using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Записываем пути 
/// </summary>
public class RecordPaths : MonoBehaviour
{
    [SerializeField]
    private PathsContainer pathsContainer = null;

    [SerializeField]
    private int maxPaths = 3;

    [SerializeField]
    private float intervalRecord = 1f;

    [SerializeField]
    private Transform transformPlayer = null;

    [SerializeField]
    private Path path = new Path();

    private Vector2 wayPoint = new Vector2();

    #region Subscribes / UnSubscribes
    private void OnEnable ()
    {
        Subscribe();
    }

    private void OnDisable ()
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

    /// <summary>
    /// Обработчик события финиш
    /// </summary>
    private void OnFinish ()
    {
        StopAllCoroutines();
        pathsContainer.Paths.Add(path);
    }
    #endregion

    private void Awake ()
    {
        Init();
    }

    /// <summary>
    /// Инициализация
    /// </summary>
    private void Init ()
    {
        if (pathsContainer.Paths.Count >= maxPaths)
        {
            Destroy(gameObject);
        }
        //TODO переделать на событие старта
        CheckStart();
    }

    /// <summary>
    /// Начинаем запись
    /// </summary>
    private void CheckStart ()
    {
        StartCoroutine(CheckRecord());
    }

    /// <summary>
    /// Чекаем позицию по таймеру
    /// </summary>
    /// <returns></returns>
    private IEnumerator CheckRecord ()
    {
        path = new Path();

        while (true)
        {
            yield return new WaitForSeconds(intervalRecord);
            //TODO Если, что-то пойдёт не так можно добавить доп проверки (например на дистанцию)
            wayPoint.x = transformPlayer.position.x;
            wayPoint.y = transformPlayer.position.y;
            path.WayPoints.Add(wayPoint);
        }
    }
}