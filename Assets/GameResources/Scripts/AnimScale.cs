using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Плавное увеличение и уменьшение объекта
/// </summary>
public class AnimScale : MonoBehaviour
{
    private Vector3 vectorClose = Vector3.zero;
    private Vector3 vectorOpen = Vector3.one;

    private Coroutine coroutineActiveWindow = null;

    private const float MIN = 0.0001f;

    private const float SPEED = 10f;

    /// <summary>
    /// Открываем/закрываем окно
    /// </summary>
    /// <param name="_isActive"></param>
    /// <param name="_isAnim"></param>
    public void Active(bool _isActive, bool _isAnim)
    {
        if (_isAnim)
        {
            if (coroutineActiveWindow != null)
            {
                StopCoroutine(coroutineActiveWindow);
            }

            if (_isActive)
            {
                coroutineActiveWindow = StartCoroutine(MoveWindow(vectorOpen));
            }
            else
            {
                coroutineActiveWindow = StartCoroutine(MoveWindow(vectorClose));
            }
        }
        else
        {
            if (_isActive)
            {
                transform.localScale = vectorOpen;
            }
            else
            {
                transform.localScale = vectorClose;
            }
        }
    }

    /// <summary>
    /// Открываем/закрываем окно по умолчанию с анимацией
    /// </summary>
    /// <param name="_isActive"></param>
    /// <param name="_isAnim"></param>
    public void Active(bool _isActive)
    {
        Active(_isActive, true);
    }

    /// <summary>
    /// Анимация окна
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveWindow(Vector3 _vector)
    {
        bool _isMove = true;
        while (_isMove)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, _vector, Time.unscaledDeltaTime * SPEED);

            if (_vector == vectorOpen &&
                transform.localScale.x > vectorOpen.x - MIN)
            {
                _isMove = false;
                transform.localScale = vectorOpen;
            }

            if (_vector == vectorClose &&
                transform.localScale.x < vectorClose.x + MIN)
            {
                _isMove = false;
                transform.localScale = vectorClose;
            }
            yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
        }
    }
}