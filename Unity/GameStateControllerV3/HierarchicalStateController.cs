using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DouduckGame {
	public sealed class HierarchicalStateController {

		private List<IHierarchicalState> m_oCurrentState = null;
		private bool m_bAtStateBegin = false;

		private bool m_bStarted = false;
		private bool m_bTerminated = false;

		public HierarchicalStateController() {
			m_oCurrentState = new List<IHierarchicalState> ();
		}
		public HierarchicalStateController(IHierarchicalState oStartState) : this() {
			Start(oStartState);
		}

		public void Start(IHierarchicalState oState) {
			if (m_bTerminated) {
				Debugger.LogError("[HStateController] has been terminated");
				return;
			}
			if (m_bStarted) {
				Debugger.LogError("[HStateController] has been started");
				return;
			}
			Debugger.Log("[HStateController] Start: " + oState.ToString());
			m_bStarted = true;
			m_oCurrentState.Add(oState);
			m_oCurrentState[0].SetProperty(this, 0);
		}

		public void Terminate() {
			for (int i = m_oCurrentState.Count - 1; i >= 0; i--) {
				m_oCurrentState[i].StateEnd();
			}
			m_oCurrentState.Clear();
			m_bTerminated = true;
		}

		public void TransTo(int iLevel, IHierarchicalState oState) {
			if (m_bTerminated) {
				Debugger.LogError("[StateController] has been terminated");
				return;
			}
			if (!m_bStarted) {
				Debugger.LogError("[StateController] need to be started first");
				return;
			}
			if (iLevel > m_oCurrentState.Count) {
				Debugger.LogError("[StateController] Level is too big");
				return;
			}

			Debugger.Log(string.Format("[StateController] Level {0:} transTo: {1:}", iLevel, oState.ToString()));
			if (iLevel == m_oCurrentState.Count) {
				m_oCurrentState.Add(oState);
				m_oCurrentState [iLevel].SetProperty(this, iLevel);
			} else {
				for (int i = m_oCurrentState.Count - 1; i >= iLevel; i--) {
					m_oCurrentState [i].StateEnd ();
					m_oCurrentState.RemoveAt (i);
				}
				m_oCurrentState.Add(oState);
				m_oCurrentState [iLevel].SetProperty(this, iLevel);
			}
		}

		public void StateUpdate() {
			if (m_bTerminated || !m_bStarted) {
				return;
			}
			for (int i = 0; i < m_oCurrentState.Count; i++) {
				if (m_oCurrentState[i].AtStateBegin) {
					m_oCurrentState[i].TouchStateBegin();
					m_oCurrentState[i].StateBegin();
				}
				m_oCurrentState[i].StateUpdate();
			}
		}
	}
}