using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Чекаем финишную линию
/// </summary>
public class FinishLine : MonoBehaviour
{
    private void OnTriggerExit (Collider other)
    {
        if (other.tag == ConstString.PLAYER_TAG)
        {
            OnFinish();
        }
    }

    /// <summary>
    /// ВЫзываем событие финиша
    /// </summary>
    private void OnFinish ()
    {
        EventManager.CallFinish();
    }
}
