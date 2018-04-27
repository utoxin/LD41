using UnityEngine;
using UnityEngine.EventSystems;

namespace Game_Code.Utils {
	public class MenuInit : MonoBehaviour {
		public GameObject SelectOnEnable;

		private void OnEnable() {
			EventSystem.current.SetSelectedGameObject(null);
			EventSystem.current.SetSelectedGameObject(SelectOnEnable);
		}
	}
}