using UnityEngine;
using System.Collections;

public class LogDisplay : MonoBehaviour {
	private string myLog = "";
	private GUIStyle style;
	private Rect rect;

	void OnEnable () {
		Application.logMessageReceived += show;
	}

	void OnDisable () {
		// Remove callback when object goes out of scope
		Application.logMessageReceived -= show;
	}

	void Awake() {
		int w = Screen.width, h = Screen.height;
		rect = new Rect(10, 10, w / 2, h - 20);
		style = new GUIStyle();
		style.alignment = TextAnchor.UpperLeft;
		style.fontSize = h / 16;
		style.normal.textColor = new Color (0.7f, 0.7f, 0.7f, 1.0f);
	}

	void OnGUI () {
		GUI.backgroundColor = new Color(0, 0, 0, 0.8f);
		GUI.Label(rect, myLog, style);
	}

	public void show(string logString, string stackTrace, LogType type)	{
		myLog = logString + "\n" + myLog;
		if (myLog.Length > 512) {
			// if myLog is too long, cut it.
			myLog = myLog.Substring(0, 374);
		}
	}

}