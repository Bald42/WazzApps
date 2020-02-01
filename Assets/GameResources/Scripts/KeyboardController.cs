using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

/// <summary>
/// Управление машиной с клавиатуры
/// </summary>
public class KeyboardController : MonoBehaviour
{
    [SerializeField]
    private string axisNameX = "Horizontal";
    [SerializeField]
    private string axisNameY = "Vertical";
    [SerializeField]
    private float axisValue = 1f;
    [SerializeField]
    private float responseSpeed = 1f;
    [SerializeField]
    private float returnToCentreSpeed = 3f;

    private CrossPlatformInputManager.VirtualAxis m_AxisX = null;
    private CrossPlatformInputManager.VirtualAxis m_AxisY = null;

    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// Инициализация
    /// </summary>
    private void Init ()
    {
#if !UNITY_EDITOR
        Destroy(gameObject);
#endif
        AddControl(axisNameX, out m_AxisX);
        AddControl(axisNameY, out m_AxisY);
    }

    /// <summary>
    /// Добавляем управление
    /// </summary>
    /// <param name="axisName"></param>
    /// <param name="m_Axis"></param>
    private void AddControl (string axisName, out CrossPlatformInputManager.VirtualAxis m_Axis)
    {
        if (!CrossPlatformInputManager.AxisExists(axisName))
        {
            m_Axis = new CrossPlatformInputManager.VirtualAxis(axisName);
            CrossPlatformInputManager.RegisterVirtualAxis(m_Axis);
        }
        else
        {
            m_Axis = CrossPlatformInputManager.VirtualAxisReference(axisName);
        }
    }

    private void Update()
    {
        CheckButton();
    }

    /// <summary>
    /// Считываем ввод с клавиатуры
    /// </summary>
    private void CheckButton ()
    {
        if (Input.GetKey(KeyCode.W))
        {
            m_AxisY.Update(Mathf.MoveTowards(m_AxisY.GetValue, axisValue, responseSpeed * Time.deltaTime));
        }

        if (Input.GetKey(KeyCode.S))
        {
            m_AxisY.Update(Mathf.MoveTowards(m_AxisY.GetValue, -axisValue, responseSpeed * Time.deltaTime));
        }

        if (Input.GetKey(KeyCode.A))
        {
            m_AxisX.Update(Mathf.MoveTowards(m_AxisX.GetValue, -axisValue, responseSpeed * Time.deltaTime));
        }

        if (Input.GetKey(KeyCode.D))
        {
            m_AxisX.Update(Mathf.MoveTowards(m_AxisX.GetValue, axisValue, responseSpeed * Time.deltaTime));
        }

        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            m_AxisX.Update(Mathf.MoveTowards(m_AxisX.GetValue, 0, responseSpeed * Time.deltaTime));
        }

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            m_AxisY.Update(Mathf.MoveTowards(m_AxisY.GetValue, 0, responseSpeed * Time.deltaTime));
        }
    }
}
