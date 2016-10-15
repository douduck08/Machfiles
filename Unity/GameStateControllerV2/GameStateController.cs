using UnityEngine;
using System.Collections;

namespace DouduckGame {
	public class GameStateController {
		private GameState m_oCurrentState = null;
		public GameState CurrentState {
			get {
				return m_oCurrentState;
			}
		}

		public bool isLoadingLevel;
		private bool m_bStarted;
		private bool m_bTerminated;
		private bool m_bAtStateBegin;
		private string m_sLoadingSceneName;

		public GameStateController() {
			isLoadingLevel = false;
			m_bStarted = false;
			m_bTerminated = false;
			m_bAtStateBegin = false;
		}

		public GameStateController(GameState oStartState) : this() {
			Start(oStartState);
		}

		public void Start(GameState oState) {
			if (m_bStarted) {
				Debugger.LogError("[GameStateController] has been started");
				return;

			}
			string sCurrentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
			Debugger.Log("[GameStateController] Start with " + oState.ToString() + ", scene: " + sCurrentSceneName);
			m_bStarted = true;
			m_bAtStateBegin = true;
			m_oCurrentState = oState;
			m_oCurrentState.SetProperty(this, sCurrentSceneName);
		}

		public void Terminate() {
			if (m_oCurrentState != null) {
				m_oCurrentState.StateEnd();
				m_oCurrentState = null;
			}
			m_bTerminated = true;
		}

		public void TransTo(GameState oState, string sSceneName, bool bForceLoading = false) {
			if (!m_bStarted) {
				Debugger.LogError("[GameStateController] need to be started first");
				return;

			}
			if (m_bTerminated) {
				Debugger.LogError("[GameStateController] has been terminated");
				return;

			}
			if (isLoadingLevel == true) {
				Debugger.LogError("[GameStateController] isLoadingLevel = true, blocking transform");
				return;
			}

			Debugger.Log("[GameStateController] TransTo: " + oState.ToString());
			if (m_oCurrentState != null) {
				m_oCurrentState.StateEnd();
			}

			if (bForceLoading) {
				LoadSceneAsync(sSceneName);
				isLoadingLevel = true;
			} else {
				if (m_oCurrentState != null && sSceneName != m_oCurrentState.SceneName) {
					LoadSceneAsync(sSceneName);
					isLoadingLevel = true;
				}
			}

			m_bAtStateBegin = true;
			m_oCurrentState = oState;
			m_oCurrentState.SetProperty(this, sSceneName);
		}

		public void StateUpdate() {
			if (isLoadingLevel || !m_bStarted || m_bTerminated) {
				return;
			}
			if (m_oCurrentState != null) {
				if (m_bAtStateBegin) {
					m_oCurrentState.StateBegin();
					m_bAtStateBegin = false;
				}
				m_oCurrentState.StateUpdate();
			}
		}

		public void LoadSceneAsync(string sSceneName) {
			Debugger.Log("[GameStateController] Load Scene Async: " + sSceneName + "start");
			m_sLoadingSceneName = sSceneName;
			isLoadingLevel = true;
			SingletonMono.Instance.StartCoroutine(StartLoadScene());  // A Singleton MonoBehaviour to StartCoroutine
		}

		public IEnumerator StartLoadScene() {
			AsyncOperation op = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(m_sLoadingSceneName, UnityEngine.SceneManagement.LoadSceneMode.Single);
			while (!op.isDone) {
				yield return new WaitForEndOfFrame();
			}
			Debugger.Log("[GameStateController] Load Scene Async :" + m_sLoadingSceneName + " is done.");
			isLoadingLevel = false;
		}
	}
}