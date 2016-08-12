using UnityEngine;
using System.Collections;

namespace DouduckGame {
	public abstract class GameState {
		private GameStateController m_GameStateController;
		protected GameStateController StateController {
			get {
				return m_GameStateController;
			}
		}
		private string m_sSceneName;
		public string SceneName {
			get {
				return m_sSceneName;
			}
		}
		public GameState () {}

		public override string ToString () {
			return string.Format ("<GameState>" + this.GetType().Name);
		}

		public void SetProperty(GameStateController oController, string sSceneName) {
			m_GameStateController = oController;
			m_sSceneName = sSceneName;
		}

		protected void TransTo(GameState oState, string sSceneName = "") {
			m_GameStateController.TransTo(oState, sSceneName);
		}
		public virtual void StateBegin() {}
		public virtual void StateUpdate() {}
		public virtual void StateEnd() {}
	}
}
