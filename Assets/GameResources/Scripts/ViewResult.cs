using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Показываем результат конкретной машины
/// </summary>
public class ViewResult : MonoBehaviour
{
    [SerializeField]
    private Text textName = null;

    [SerializeField]
    private Text textResult = null;

    [SerializeField]
    private AnimScale animScale = null;

    /// <summary>
    /// Инициализация
    /// </summary>
    public void Init (string _name, string _result)
    {
        animScale.Active(false, false);
        textName.text = _name;
        textResult.text = _result;
        animScale.Active(true);
    }
}