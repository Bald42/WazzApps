using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Показываем имя
/// </summary>
public class ViewName : MonoBehaviour
{
    [SerializeField]
    private Text text = null;

    [SerializeField]
    private Image back = null;

    private Transform target = null;
    private bool isActive = false;
    private Camera camera = null;
    private Vector3 point = Vector3.zero;
    private Vector3 drawPositionVector = Vector3.zero;

    /// <summary>
    /// Инициализация
    /// </summary>
    public void Init (Transform _transform, Color _color, string _text)
    {
        //TODO можно добавить вектор смещения
        camera = AddCamera.Add();
        target = _transform;
        back.color = _color;
        text.text = _text;
        isActive = true;
    }

    private void Update()
    {
        if (isActive)
        {
            Move();
        }        
    }

    private void Move ()
    {
        point = camera.WorldToViewportPoint(target.position);

        if (point.z > 0)
        {
            drawPositionVector.x = point.x * Screen.width;
            drawPositionVector.y = point.y * Screen.height;
            drawPositionVector.z = 0;
            transform.position = drawPositionVector;
        }
    }
}