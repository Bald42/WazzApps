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

    private bool isFinish = false;

    private List <Result> results = new List<Result> ();
    private Result newResult = null;

    private List<string> names = new List<string>();

    private const float DELAY = 0.5f;

    private System.TimeSpan startTime;
    private System.TimeSpan nowTime;

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
        EventManager.OnFinishName += OnFinishName;
        EventManager.OnViewFinish += OnViewFinish;
    }

    /// <summary>Отписки</summary>
    private void UnSubscribe()
    {
        EventManager.OnFinishName -= OnFinishName;
        EventManager.OnViewFinish -= OnViewFinish;
    }

    /// <summary>
    /// Обработчик события пришедшего на финиш
    /// </summary>
    /// <param name="_name"></param>
    private void OnFinishName(string _name)
    {
        WriteResult(_name);
    }

    /// <summary>
    /// Обработчик события завершения гонки
    /// </summary>
    private void OnViewFinish()
    {
        if (!isFinish)
        {
            StartCoroutine(DelayViewResult());
        }
        isFinish = true;        
    }
    #endregion

    private void Awake()
    {
        Init();
    }

    private void Init ()
    {
        startTime = System.TimeSpan.FromSeconds(0);
    }

    private void WriteResult (string _name)
    {
        if (!IsNewResult(_name))
        {
            return;
        }

        names.Add(_name);
        newResult = new Result();
        newResult.Name = _name;
        newResult.Time = CheckTime();
        results.Add(newResult);

        if (isFinish)
        {
            StopAllCoroutines();
            StartCoroutine(DelayViewResult());
        }
    }

    private bool IsNewResult(string _name)
    {
        bool isNew = true;
        for (int i=0; i < names.Count; i++)
        {
            if (names[i] == _name)
            {
                isNew = false;
            }
        }
        return isNew;
    }

    private IEnumerator DelayViewResult ()
    {
        while (results.Count > 0)
        {
            yield return new WaitForSecondsRealtime(DELAY);
            EventManager.CallViewResult(results[0].Name, results[0].Time);
            results.RemoveAt(0);
        }
    }

    private string CheckTime ()
    {
        nowTime = System.TimeSpan.FromSeconds(Time.timeSinceLevelLoad);
        string _time = (nowTime - startTime).Minutes.ToString() + "." +
                       (nowTime - startTime).Seconds.ToString() + "." +
                       (nowTime - startTime).Milliseconds.ToString();
        return _time;
    }

    [System.Serializable]
    public class Result
    {
        public string Name = string.Empty;
        public string Time = string.Empty;
    }
}