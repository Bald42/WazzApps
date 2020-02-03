using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Чекаем финишную линию
/// </summary>
public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter (Collider other)
    {
        if (other.tag == ConstString.PLAYER_TAG)
        {
            EventManager.CallFinish();
        }

        string _name = other.transform.GetComponentInParent<DriverName>().Driver_Name;
        EventManager.CallFinishName(_name);        
    }
}
