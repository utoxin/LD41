using UnityEngine;

namespace Game_Jam_Template.Scripts.Menu {
	public class QuitApplication : MonoBehaviour {
		private void OnApplicationQuit() {
			if (!Application.isEditor) {
				Application.CancelQuit();
				System.Diagnostics.Process.GetCurrentProcess().Kill();
			}
		}

		public void Quit() {
			if (!Application.isEditor) {
				Application.Quit();
			}	
		}
	}
}
