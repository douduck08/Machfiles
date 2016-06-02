using UnityEngine;
using System.Collections;

public class LogDisplay : MonoBehaviour {
	public bool isOpen = true;
	public int FontSize = 24;

	private string myLog = "";
	private GUIStyle style;

	void OnEnable () {
		Application.logMessageReceived += newLog;
	}

	void OnDisable () {
		// Remove callback when object goes out of scope
		Application.logMessageReceived -= newLog;
	}

	void Awake() {
		style = new GUIStyle();
		style.alignment = TextAnchor.UpperLeft;
		style.fontSize = FontSize;
		style.normal.textColor = new Color (0.7f, 0.7f, 0.7f, 1.0f);
	}

	void OnGUI () {
		if (isOpen) {
			if (GUI.Button(new Rect(10, 10, 20, 20), "x")) {
				isOpen = false;
			}
			if (GUI.Button(new Rect(60, 10, 20, 20), "<")) {
				FontSize -= 2;
				style.fontSize = FontSize;
			}
			if (GUI.Button(new Rect(110, 10, 20, 20), ">")) {
				FontSize += 2;
				style.fontSize = FontSize;
			}
			int w = Screen.width, h = Screen.height;
			GUI.Label(new Rect(10, 30, w - 20, h - 40), myLog, style);
		} else {
			if (GUI.Button(new Rect(10, 10, 20, 20), "o")) {
				isOpen = true;
			}
		}
	}

	public void newLog(string logString, string stackTrace, LogType type)	{
		myLog = logString + "\n" + myLog;
		if (myLog.Length > 512) {
			// if myLog is too long, cut it.
			myLog = myLog.Substring(0, 374);
		}
	}

}