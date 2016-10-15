﻿using UnityEngine;
using System.Collections;

namespace DouduckGame {
	public sealed class StateController {
		private IState m_oCurrentState = null;
		public IState CurrentState {
			get {
				return m_oCurrentState;
			}
		}

		private bool m_bStarted = false;
		private bool m_bTerminated = false;

		public StateController() {}
		public StateController(IState oStartState) : this() {
			Start(oStartState);
		}

		public void Start(IState oState) {
			if (m_bTerminated) {
				Debugger.LogError("[StateController] has been terminated");
				return;
			}
			if (m_bStarted) {
				Debugger.LogError("[StateController] has been started");
				return;
			}
			Debugger.Log("[StateController] Start: " + oState.ToString());
			m_bStarted = true;
			m_oCurrentState = oState;
			m_oCurrentState.SetProperty(this);
		}

		public void Terminate() {
			if (m_oCurrentState != null) {
				m_oCurrentState.StateEnd();
				m_oCurrentState = null;
			}
			m_bTerminated = true;
		}

		public void TransTo(IState oState) {
			if (m_bTerminated) {
				Debugger.LogError("[StateController] has been terminated");
				return;
			}
			if (!m_bStarted) {
				Debugger.LogError("[StateController] need to be started first");
				return;
			}
			Debugger.Log("[StateController] TransTo: " + oState.ToString());
			if (m_oCurrentState != null) {
				m_oCurrentState.StateEnd();
			}
			m_oCurrentState = oState;
			m_oCurrentState.SetProperty(this);
		}

		public void StateUpdate() {
			if (m_bTerminated || !m_bStarted) {
				return;
			}
			if (m_oCurrentState != null) {
				if (m_oCurrentState.AtStateBegin) {
					m_oCurrentState.TouchStateBegin();
					m_oCurrentState.StateBegin();
				}
				m_oCurrentState.StateUpdate();
			}
		}
	}
}
