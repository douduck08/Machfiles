using UnityEngine;
using UnityEditor;

public class MyGameObjectTools : EditorWindow 
{
	static int go_count = 0, components_count = 0, missing_count = 0;
	
	[MenuItem("Window/My GameObject Tools")]
	public static void ShowWindow() {
		EditorWindow.GetWindow(typeof(MyGameObjectTools), false, "My Tools");
	}
	
	public void OnGUI()
	{
		if (GUILayout.Button("Find Missing Scripts in selected GameObjects")) {
			FindInSelected();
		}
		EditorGUILayout.LabelField ("Searched GameObjects:", go_count.ToString());
		EditorGUILayout.LabelField ("Found Components:", components_count.ToString());
		EditorGUILayout.LabelField ("Found Missing:", missing_count.ToString());

		if (GUILayout.Button("Active Selection")) {
			SetActiveInSelected(true);
		}

		if (GUILayout.Button("Inactive Selection")) {
			SetActiveInSelected(false);
		}
	}

	private static void FindInSelected() {
		GameObject[] go = Selection.gameObjects;
		go_count = 0;
		components_count = 0;
		missing_count = 0;
		foreach (GameObject g in go) {
			FindInGO(g);
		}
	}
	
	private static void FindInGO(GameObject g) {
		go_count++;
		Component[] components = g.GetComponents<Component>();
		for (int i = 0; i < components.Length; i++) {
			components_count++;
			if (components[i] == null) {
				missing_count++;
				string s = g.name;
				Transform t = g.transform;
				while (t.parent != null) 
				{
					s = t.parent.name +"/"+s;
					t = t.parent;
				}
				Debug.LogWarning (s + " has an empty script attached in position: " + i, g);
			}
		}
		foreach (Transform childT in g.transform) {
			FindInGO(childT.gameObject);
		}
	}

	private static void SetActiveInSelected(bool active) {
		GameObject[] go = Selection.gameObjects;
		foreach (GameObject g in go) {
			SetActiveInGO(g, active);
		}
	}

	private static void SetActiveInGO(GameObject g, bool active) {
		foreach (Transform childT in g.transform) {
			SetActiveInGO(childT.gameObject, active);
		}
		g.SetActive(active);
	}
}
