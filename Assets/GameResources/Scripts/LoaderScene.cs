using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Загрузка игровой сцены
/// </summary>
public class LoaderScene : MonoBehaviour
{
    [SerializeField]
    private PathsContainer pathsContainer = null;

    [SerializeField]
    private string sceneName = "Game";

    [SerializeField]
    private bool isClearPathsContainer = true;

    private Path path = new Path();

    private Vector2 wayPoint = new Vector2();

    private void Start()
    {
        LoadPaths();
        LoadScene();
    }

    private void LoadPaths ()
    {
        if (isClearPathsContainer)
        {
            pathsContainer.Paths.Clear();
        }

        for (int i = 0; i < int.MaxValue; i++)
        {
            if (PlayerPrefs.HasKey(KeyPrefs.PATH +
                                   i +
                                   KeyPrefs.POINT +
                                   "0X"))
            {
                path = new Path();
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
                        wayPoint.x = x;
                        wayPoint.y = z;
                        path.WayPoints.Add(wayPoint);
                    }
                    else
                    {
                        pathsContainer.Paths.Add(path);
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

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}