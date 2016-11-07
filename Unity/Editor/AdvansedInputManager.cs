﻿using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

// ////////////////////////////////////////////////////////////////////////////////
// Advansed Input Manager
// Created by douduck08 @ 20161107
// 
// This is a simple editor for quickly modifying the Unity Input Manager Setting.
// Part of the code is from the Internet, I writed the link at Ref.
// 
// * This editor will not modify the default value in the Unity Input Manager.
// 
// Ref: http://plyoung.appspot.com/blog/manipulating-input-manager-in-script.html
// ////////////////////////////////////////////////////////////////////////////////

public class AdvansedInputManager : EditorWindow {

	[MenuItem("Window/Advansed InputManager")]
	public static void ShowWindow() {
		EditorWindow.GetWindow(typeof(AdvansedInputManager), false, "Input Manager");
	}

	public enum AxisType {
		KeyOrMouseButton = 0,
		MouseMovement = 1,
		JoystickAxis = 2
	};

	private const float GRAVITY = 1f;
	private const float DEAD = 0.2f;
	private const float SENSITIVITY = 1f;

	public class InputAxis {
		public string name = "";
		public string descriptiveName;
		public string descriptiveNegativeName;
		public string negativeButton;
		public string positiveButton;
		public string altNegativeButton;
		public string altPositiveButton;

		public float gravity = GRAVITY;
		public float dead = DEAD;
		public float sensitivity = SENSITIVITY;

		public bool snap = false;
		public bool invert = false;

		public AxisType type;

		public int axis;
		public int joyNum;
	}

	private SerializedObject m_serializedObject;
	private SerializedProperty m_axesProperty;

	private bool m_QuickMode = true;
	private bool m_NeedGlobalJoystick = false;
	private int m_iJoystickNumber;
	private int m_iAxisNumber;
	private InputAxis[] m_InputAxis = new InputAxis[0];

	public void OnGUI() {
		if (GUILayout.Button("Save")) {
			ResetInputManagerAsset();
			if (m_QuickMode) {
				if (m_NeedGlobalJoystick) {
					for (int i = 1; i <= m_iAxisNumber; i++) {
						if (i == 1) {
							AddAxis(new InputAxis () { name = "JoystickAxisX", type = AxisType.JoystickAxis, axis = i, joyNum = 0 });
						} else if (i == 2) {
							AddAxis(new InputAxis () { name = "JoystickAxisY", type = AxisType.JoystickAxis, axis = i, joyNum = 0 });
						} else {
							AddAxis(new InputAxis () { name = "JoystickAxis" + i, type = AxisType.JoystickAxis, axis = i, joyNum = 0 });
						}
					}
				}
				for (int n = 1; n <= m_iJoystickNumber; n++) {
					for (int i = 1; i <= m_iAxisNumber; i++) {
						if (i == 1) {
							AddAxis(new InputAxis () { name = string.Format("Joystick{0}AxisX", n), type = AxisType.JoystickAxis, axis = i, joyNum = 0 });
						} else if (i == 2) {
							AddAxis(new InputAxis () { name = string.Format("Joystick{0}AxisY", n), type = AxisType.JoystickAxis, axis = i, joyNum = 0 });
						} else {
							AddAxis(new InputAxis () { name = string.Format("Joystick{0}Axis{1}", n, i), type = AxisType.JoystickAxis, axis = i, joyNum = 0 });
						}
					}
				}

			} else {
				for (int i = 0; i < m_InputAxis.Length; i++) {
					AddAxis(m_InputAxis[i]);
				}
			}
		}
		if (GUILayout.Button("Reset to default")) {
			ResetInputManagerAsset();
		}
		m_QuickMode = GUILayout.Toggle(m_QuickMode, "Quick Mode");
		GUILayout.TextArea("", GUI.skin.horizontalSlider);

		if (m_QuickMode) {
			m_NeedGlobalJoystick = GUILayout.Toggle(m_NeedGlobalJoystick, "Need Global Joystick");
			m_iJoystickNumber = Mathf.Clamp(EditorGUILayout.IntField("Joystick Number", m_iJoystickNumber), 0, 11);
			m_iAxisNumber = Mathf.Clamp(EditorGUILayout.IntField("Axis Number", m_iAxisNumber), 1, 28);
		} else {
			GUILayout.BeginHorizontal();
			GUILayout.Label("Custom InputAxis Num:");
			if (GUILayout.Button("-")) {
				if (m_InputAxis.Length > 0) {
					System.Array.Resize(ref m_InputAxis, m_InputAxis.Length - 1);
				}
			}
			if (GUILayout.Button("+")) {
				System.Array.Resize(ref m_InputAxis, m_InputAxis.Length + 1);
			}
			GUILayout.EndHorizontal();

			GUILayout.Label("InputAxis Array:");
			GUILayout.BeginHorizontal();
			GUILayout.Label("name");
			GUILayout.Label("axis");
			GUILayout.Label("joyNum");
			GUILayout.EndHorizontal();

			for (int i = 0; i < m_InputAxis.Length; i++) {
				if (m_InputAxis [i] == null) {
					m_InputAxis [i] = new InputAxis ();
				}
				EditorGUILayout.BeginHorizontal();
				m_InputAxis[i].name = EditorGUILayout.TextField("", m_InputAxis[i].name);
				m_InputAxis[i].axis = EditorGUILayout.IntField("", m_InputAxis[i].axis);
				m_InputAxis[i].joyNum = EditorGUILayout.IntField("", m_InputAxis[i].joyNum);
				EditorGUILayout.EndHorizontal();
			}
		}
	}

	private void ResetInputManagerAsset() {
		m_serializedObject = new SerializedObject (AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset") [0]);
		m_axesProperty = m_serializedObject.FindProperty("m_Axes");
		m_axesProperty.arraySize = 18;
		m_serializedObject.ApplyModifiedProperties();
	}

	private SerializedProperty GetChildProperty(SerializedProperty parent, string name) {
		SerializedProperty child = parent.Copy();
		child.Next(true);
		do {
			if (child.name == name) return child;
		} while (child.Next(false));
		return null;
	}
		
	private void AddAxis(InputAxis axis) {
		m_axesProperty.arraySize++;
		m_serializedObject.ApplyModifiedProperties();

		SerializedProperty axisProperty = m_axesProperty.GetArrayElementAtIndex(m_axesProperty.arraySize - 1);

		GetChildProperty(axisProperty, "m_Name").stringValue = axis.name;
		GetChildProperty(axisProperty, "descriptiveName").stringValue = axis.descriptiveName;
		GetChildProperty(axisProperty, "descriptiveNegativeName").stringValue = axis.descriptiveNegativeName;
		GetChildProperty(axisProperty, "negativeButton").stringValue = axis.negativeButton;
		GetChildProperty(axisProperty, "positiveButton").stringValue = axis.positiveButton;
		GetChildProperty(axisProperty, "altNegativeButton").stringValue = axis.altNegativeButton;
		GetChildProperty(axisProperty, "altPositiveButton").stringValue = axis.altPositiveButton;
		GetChildProperty(axisProperty, "gravity").floatValue = axis.gravity;
		GetChildProperty(axisProperty, "dead").floatValue = axis.dead;
		GetChildProperty(axisProperty, "sensitivity").floatValue = axis.sensitivity;
		GetChildProperty(axisProperty, "snap").boolValue = axis.snap;
		GetChildProperty(axisProperty, "invert").boolValue = axis.invert;
		GetChildProperty(axisProperty, "type").intValue = (int)axis.type;
		GetChildProperty(axisProperty, "axis").intValue = axis.axis - 1;
		GetChildProperty(axisProperty, "joyNum").intValue = axis.joyNum;

		m_serializedObject.ApplyModifiedProperties();
	}
}
