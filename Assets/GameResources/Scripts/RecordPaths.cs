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
    private float minDistance = 1f;

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
        EventManager.OnSpawnPlayer += OnSpawnPlayer;
        EventManager.OnFinish += OnFinish;
    }

    /// <summary>Отписки</summary>
    private void UnSubscribe()
    {
        EventManager.OnFinish -= OnFinish;
        EventManager.OnSpawnPlayer -= OnSpawnPlayer;
    }

    /// <summary>
    /// Обработчик события финиш
    /// </summary>
    private void OnFinish ()
    {
        StopAllCoroutines();
        path.WayPoints.RemoveAt(0);
        pathsContainer.Paths.Add(path);
        StartCoroutine(SavePathInPrefs());
    }

    /// <summary>
    /// Обработчик события спавна игрока
    /// </summary>
    /// <param name="_transform"></param>
    private void OnSpawnPlayer (Transform _transform)
    {
        transformPlayer = _transform;
        CheckStart();
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

            wayPoint.x = transformPlayer.position.x;
            wayPoint.y = transformPlayer.position.z;

            if (path.WayPoints.Count > 0)
            {
                if ((path.WayPoints[path.WayPoints.Count-1] - wayPoint).sqrMagnitude > minDistance)
                {
                    path.WayPoints.Add(wayPoint);
                }
            }
            else
            {
                path.WayPoints.Add(wayPoint);
            }
        }
    }

    /// <summary>
    /// Сохраняем путь в префсы
    /// </summary>
    /// <returns></returns>
    private IEnumerator SavePathInPrefs ()
    {
        for (int i=0; i < path.WayPoints.Count; i++)
        {
            PlayerPrefs.SetFloat(KeyPrefs.PATH + 
                                 (pathsContainer.Paths.Count - 1) + 
                                 KeyPrefs.POINT + 
                                 i + 
                                 "X",
                                 path.WayPoints[i].x);

            PlayerPrefs.SetFloat(KeyPrefs.PATH +
                                 (pathsContainer.Paths.Count - 1) +
                                 KeyPrefs.POINT + 
                                 i + 
                                 "Z",
                                 path.WayPoints[i].y);
            yield return new WaitForFixedUpdate();
        }
        PlayerPrefs.Save();
        //TestPath();
        yield return null;
    }

    private void TestPath ()
    {
        for (int i = 0; i < int.MaxValue; i++)
        {
            if (PlayerPrefs.HasKey(KeyPrefs.PATH +
                                   i +
                                   KeyPrefs.POINT +
                                   "0X"))
            {
                for (int j = 0; j < int.MaxValue; j++)
                {
                    if (PlayerPrefs.HasKey(KeyPrefs.PATH +
                               i +
                               KeyPrefs.POINT +
                               j +
                               "X"))
                    {
                        float x = PlayerPrefs.GetFloat(KeyPrefs.PATH +
                                                       i +
                                                       KeyPrefs.POINT +
                                                       j +
                                                       "X");
                        float z = PlayerPrefs.GetFloat(KeyPrefs.PATH +
                                                       i +
                                                       KeyPrefs.POINT +
                                                       j +
                                                       "Z");
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                break;
            }
        }
    }
}