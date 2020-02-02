using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

/// <summary>
/// Спавним тачки
/// </summary>
public class SpawnCars : MonoBehaviour
{
    [SerializeField]
    private Transform parent = null;

    [SerializeField]
    private Transform prefabPlayer = null;

    [SerializeField]
    private Transform prefabBot = null;

    [SerializeField]
    private WaypointCircuit prefabWayPount = null;

    [SerializeField]
    private int maxBot = 3;

    [SerializeField]
    private List<Color> colorsBot = new List<Color>();

    [SerializeField]
    private PathsContainer pathsContainer = null;

    [SerializeField]
    private List<Transform> startPoint = new List<Transform>();

    private Vector3 newWayPoint = Vector3.zero;

    private WaypointCircuit newWaypointCircuit = new WaypointCircuit();

    private void Start()
    {
        Init();
    }

    /// <summary>
    /// Инициализация
    /// </summary>
    private void Init ()
    {
        SpawPlayer();
        SpawBots();
        EventManager.CallStart();
    }

    /// <summary>
    /// Спавним игрока
    /// </summary>
    private void SpawPlayer ()
    {
        Transform startPoint = CheckPosition();
        Transform transformPlayer = Instantiate(prefabPlayer, startPoint.position, startPoint.rotation, parent);
        transformPlayer.name = "Player";
        transformPlayer.GetComponent<DriverName>().Driver_Name = ConstString.PLAYER_NAME;
        EventManager.CallSpawnPlayer (transformPlayer);
    }

    /// <summary>
    /// Спавним ботов
    /// </summary>
    private void SpawBots ()
    {
        int numberBot = 0;
        while (IsCanSpawnBot(numberBot))
        {
            if (pathsContainer.Paths.Count == 0)
            {
                return;
            }

            Transform startPoint = CheckPosition();
            Transform transformBot = Instantiate(prefabBot, startPoint.position, startPoint.rotation, parent);
            string nameDriver = NameBot();
            transformBot.name = "Enemy_" + nameDriver;
            transformBot.GetComponent<DriverName>().Driver_Name = nameDriver;

            SpawnWaypointCircuit(nameDriver);
            transformBot.GetComponent<WaypointProgressTracker>().circuit = newWaypointCircuit;
            numberBot++;
        }
    }

    /// <summary>
    /// Проверяем можно ли спавнить ботов
    /// </summary>
    /// <returns></returns>
    private bool IsCanSpawnBot(int _numberBot)
    {
        if (_numberBot <= maxBot)
        {
            //TODO на свежую голову переписать условия
            if (_numberBot < pathsContainer.Paths.Count)/* &&
                (maxBot - _numberBot) < startPoint.Count &&
                (maxBot - _numberBot) < ConstString.BotNames.Count)*/
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Находим свободную позицию старта
    /// </summary>
    /// <returns></returns>
    private Transform CheckPosition ()
    {        
        int rnd = Random.Range(0, startPoint.Count);
        Transform pos = startPoint[rnd];
        startPoint.RemoveAt(rnd);
        return pos;
    }

    /// <summary>
    /// Находим свободное имя боту
    /// </summary>
    /// <returns></returns>
    private string NameBot ()
    {
        int rnd = Random.Range(0, ConstString.BotNames.Count);
        string name = ConstString.BotNames[rnd];
        ConstString.BotNames.RemoveAt(rnd);
        return name;
    }

    /// <summary>
    /// Спавним вайпоинты
    /// </summary>
    private void SpawnWaypointCircuit (string nameDriver)
    {
        WaypointCircuit waypointCircuit = Instantiate(prefabWayPount, parent.position, Quaternion.identity, parent);
        WaypointCircuit.WaypointList waypointList = new WaypointCircuit.WaypointList();
        waypointCircuit.name = "WayPoints_" + nameDriver;
        int rnd = Random.Range(0,pathsContainer.Paths.Count);
        for (int i=0; i < pathsContainer.Paths[rnd].WayPoints.Count; i++)
        {
            newWayPoint.x = pathsContainer.Paths[rnd].WayPoints[i].x;
            newWayPoint.z = pathsContainer.Paths[rnd].WayPoints[i].y;

            GameObject point = new GameObject();
            GameObject newPoint = Instantiate(point, newWayPoint, Quaternion.identity, waypointCircuit.transform);
            newPoint.name = "WayPoint" + i.ToString();
            waypointList.items.Add(newPoint.transform);
        }
        waypointCircuit.waypointList = waypointList;
        newWaypointCircuit = waypointCircuit;
    }
}