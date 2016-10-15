using UnityEngine;
using System.Collections;

namespace DouduckGame {
	public class SingletonMono : MonoBehaviour {
		public delegate IEnumerator CoroutineDelegate();

		// *** Singleton object ***
		private static GameObject m_oContainer = null;
		public  static GameObject Container {
			get {
				if (m_oContainer == null) {
					m_oContainer = new GameObject("[SingletonContainer]");
					GameObject.DontDestroyOnLoad(m_oContainer);
				}
				return m_oContainer;
			}
		}
		private static SingletonMono m_oInstance = null;
		public  static SingletonMono Instance {
			get {
				if (m_oInstance == null) {
					m_oInstance = Container.AddComponent<SingletonMono>();
				}
				return m_oInstance;
			}
		}
		private SingletonMono() {}

		public void RunCoroutine(CoroutineDelegate dCoroutine) {
			StartCoroutine(dCoroutine());
		}
	}
}