using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;

/// <summary>
/// Активируем камеру
/// </summary>
public class ActiveCamera : MonoBehaviour
{
    [SerializeField]
    private AutoCam autoCam = null;

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
        EventManager.OnSpawnPlayer += OnSpawnPlayer;
    }

    /// <summary>Отписки</summary>
    private void UnSubscribe()
    {
        EventManager.OnSpawnPlayer -= OnSpawnPlayer;
    }

    /// <summary>
    /// Обработчик события спавна игрока
    /// </summary>
    /// <param name="_transform"></param>
    private void OnSpawnPlayer(Transform _transform)
    {
        autoCam.SetTarget(_transform);
    }
    #endregion
}