using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Добавляем камеру
/// </summary>
public static class AddCamera
{
    private static Camera camera = null;
    
    /// <summary>
    /// Находим камеру
    /// </summary>
    /// <returns></returns>
    public static Camera Add ()
    {
        if (!camera)
        {
            camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
        return camera;
    }
}
