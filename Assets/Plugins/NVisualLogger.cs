using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NVisualLogger : MonoBehaviour {
	static NVisualLogger instance;
	
	public static NVisualLogger Instance {
		get
		{
			return instance;
		}
	}

//	static string _filter = "";
    static List <string> _filter = new List<string>();
	
	/// <summary>
	/// Отображение логов в виде GUI-текста
	/// </summary>
	public bool VisualDebug = true;
	
	public float ClearTime = 3;

	public bool OnlyInDevVersion = true;
//	public string Filter = "";
    public List <string> Filter = new List<string>();

	float clearTimer;
	
	List<string> text = new List<string>();
	string textOneLine = "";
	string time;
	Rect logRect;
	GUIStyle style;
	Rect timeRect;
	
	void Awake() {
		if(instance == null)
		{
			if(!OnlyInDevVersion || (OnlyInDevVersion && Debug.isDebugBuild))
			{
				instance = this;
				Init();
				DontDestroyOnLoad(gameObject);
				Log("Logger inited.");
				_filter = Filter;
			}
			else
				Destroy(gameObject);
		}
		else
			Destroy(gameObject);
	}

	void Init () {
		logRect = new Rect(5, 5, Screen.width, Screen.height/2);
		timeRect = new Rect(Screen.width - 250, 5, 240, 50);
		style = new GUIStyle();
		style.fontSize = 22;
		style.fontStyle = FontStyle.Bold;
		style.normal.textColor = Color.red;
		
		Application.RegisterLogCallback(HandleLog);
	}
	
	void HandleLog(string logString, string stackTrace, LogType type) {
		string msg = logString;
		if(type == LogType.Exception)
			msg += "\n" + stackTrace;

		Log(msg);
	}
	
	public static void Log(string msg)
	{
		if(instance != null)
		{
			if(instance.VisualDebug)
			{
                for (int i = 0; i < _filter.Count; i++)
                {
                    if ((_filter[i] == "") || (msg.Contains(_filter[i])))
                    {
                        instance.text.Add(msg + "\n");
                        instance.UpdateText();
                        instance.clearTimer = 0;
                    }
                }
                if (_filter.Count == 0)
                {
                    instance.text.Add(msg + "\n");
                    instance.UpdateText();
                    instance.clearTimer = 0;
                }
			}
			//CDebug.Log(msg);
		}
	}
	
	void UpdateText()
	{
		textOneLine = "";
		for(int i = 0; i < text.Count; i++)
		{
			textOneLine += text[i];
		}
	}
	
	void ClearLine()
	{
		if(text.Count > 0)
			text.RemoveAt(0);
		UpdateText();
		clearTimer = 0;
	}
	
	void Update()
	{
		clearTimer += Time.deltaTime;
		if(clearTimer > ClearTime)
			ClearLine();
	}
	
	void OnGUI()
	{
		if(VisualDebug)
		{
			time = Time.realtimeSinceStartup.ToString("00.00") + " (TS: " + Time.timeScale.ToString() + ")";
			GUI.Label(logRect, textOneLine, style);
			GUI.Label(timeRect, time, style); 
		}
	}

    public void Active ()
    {
        this.enabled = !this.enabled;
    }
}
