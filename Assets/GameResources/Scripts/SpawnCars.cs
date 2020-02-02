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
    private Transform parentCar = null;

    [SerializeField]
    private Transform parentUi = null;

    [SerializeField]
    private Transform prefabPlayer = null;

    [SerializeField]
    private Transform prefabBot = null;

    [SerializeField]
    private WaypointCircuit prefabWayPount = null;

    [SerializeField]
    private ViewName prefabUi = null;

    [SerializeField]
    private int maxBot = 3;

    [SerializeField]
    private List<Color> colorsBot = new List<Color>();

    private List<string> botNames = new List<string>()
    {
        "Dominic Toretto",
        "Letty Ortiz",
        "Mia Toretto",
        "Han Lue",
        "Roman Pearce",
        "Tej Parker",
        "Sean Boswell",
        "Vinse Kretch",
        "Rico Santos",
        "Tego Leo",
        "Gisele Yashar",
        "Luke Hobbs",
        "Elena Neves"
    };

    [SerializeField]
    private PathsContainer pathsContainer = null;

    [SerializeField]
    private List<Transform> startPoint = new List<Transform>();

    private Vector3 newWayPoint = Vector3.zero;

    private WaypointCircuit newWaypointCircuit = new WaypointCircuit();

    [SerializeField]
    private GameObject point = null;

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
        Transform transformPlayer = Instantiate(prefabPlayer, startPoint.position, startPoint.rotation, parentCar);
        transformPlayer.name = "Player";
        transformPlayer.GetComponent<DriverName>().Driver_Name = ConstString.PLAYER_NAME;

        ViewName viewName = Instantiate(prefabUi, Vector3.one * 1000, Quaternion.identity, parentUi);
        viewName.name = "ViewName_" + ConstString.PLAYER_NAME;
        viewName.Init(transformPlayer, Color.white, ConstString.PLAYER_NAME);

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
            Transform transformBot = Instantiate(prefabBot, startPoint.position, startPoint.rotation, parentCar);
            string nameDriver = NameBot();
            transformBot.name = "Enemy_" + nameDriver;
            transformBot.GetComponent<DriverName>().Driver_Name = nameDriver;

            SpawnWaypointCircuit(nameDriver);
            WaypointProgressTracker newWaypointProgressTracker = transformBot.GetComponent<WaypointProgressTracker>();
            newWaypointProgressTracker.circuit = newWaypointCircuit;
            newWaypointProgressTracker.Reset();

            Color newBotColor = CheckColor();
            AddColorBot(newBotColor, transformBot);

            ViewName viewName = Instantiate (prefabUi, Vector3.one * 1000, Quaternion.identity, parentUi);
            viewName.name = "ViewName_" + nameDriver;
            viewName.Init(transformBot, newBotColor, nameDriver);

            //transformBot.gameObject.SetActive(true);
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
            if (_numberBot < pathsContainer.Paths.Count &&
                startPoint.Count > 0 &&
                botNames.Count > 0 &&
                colorsBot.Count > 0)
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
        int rnd = Random.Range(0, botNames.Count);
        string name = botNames[rnd];
        botNames.RemoveAt(rnd);
        return name;
    }

    /// <summary>
    /// Спавним вайпоинты
    /// </summary>
    private void SpawnWaypointCircuit (string nameDriver)
    {
        WaypointCircuit waypointCircuit = Instantiate(prefabWayPount, parentCar.position, Quaternion.identity, parentCar);
        WaypointCircuit.WaypointList waypointList = new WaypointCircuit.WaypointList();
        waypointCircuit.name = "WayPoints_" + nameDriver;
        int rnd = Random.Range(0,pathsContainer.Paths.Count);
        for (int i=0; i < pathsContainer.Paths[rnd].WayPoints.Count; i++)
        {
            newWayPoint.x = pathsContainer.Paths[rnd].WayPoints[i].x;
            newWayPoint.z = pathsContainer.Paths[rnd].WayPoints[i].y;
            
            GameObject newPoint = Instantiate(point, newWayPoint, Quaternion.identity, waypointCircuit.transform);
            newPoint.name = "WayPoint" + i.ToString();
            waypointList.items.Add(newPoint.transform);
        }
        waypointCircuit.waypointList = waypointList;
        newWaypointCircuit = waypointCircuit;
    }

    /// <summary>
    /// Чекаем цвет
    /// </summary>
    /// <returns></returns>
    private Color CheckColor()
    {
        int rnd = Random.Range(0, colorsBot.Count);
        Color _color = colorsBot[rnd];
        colorsBot.RemoveAt(rnd);
        return _color;
    }

    /// <summary>
    /// Добавляем цвет боту
    /// </summary>
    /// <param name="_color"></param>
    private void AddColorBot (Color _color, Transform _bot)
    {
        MeshRenderer [] arrayMaterial = _bot.GetComponentsInChildren<MeshRenderer>();
        _color.a = 0.25f;
        for (int i=0; i < arrayMaterial.Length;i++)
        {
            arrayMaterial[i].material.color = _color;
        }
    }
}