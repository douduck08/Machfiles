using UnityEngine;
using System.Collections;

public static class Debugger {
	public static bool DebugMode = false;

	public static void Log(object message) {
		if (DebugMode) {
			Debug.Log(message);
		}
	}

	public static void LogWarning(object message) {
		if (DebugMode) {
			Debug.LogWarning(message);
		}
	}

	public static void LogError(object message) {
		if (DebugMode) {
			Debug.LogError(message);
		}
	}
}
