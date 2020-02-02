using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Чекпоинт
/// </summary>
public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ConstString.PLAYER_TAG)
        {
            OnCheckpoint();
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// ВЫзываем событие финиша
    /// </summary>
    private void OnCheckpoint()
    {
        EventManager.CallCheckpoint();
    }
}
