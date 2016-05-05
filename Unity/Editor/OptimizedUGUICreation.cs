using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class OptimizeEditor {

	[MenuItem("GameObject/Optimized UI/Image", false, 10)]
	static void CreatImage(MenuCommand menuCommand)
	{
		EditorApplication.ExecuteMenuItem("GameObject/UI/Image");
		GameObject go = (GameObject)Selection.activeObject;
		go.GetComponent<Image>().raycastTarget = false;
	}

	[MenuItem("GameObject/Optimized UI/Text", false, 10)]
	static void CreatText(MenuCommand menuCommand)
	{
		EditorApplication.ExecuteMenuItem("GameObject/UI/Text");
		GameObject go = (GameObject)Selection.activeObject;
		go.GetComponent<Text>().raycastTarget = false;
	}
}
