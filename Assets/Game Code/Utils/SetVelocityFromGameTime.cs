using Game_Code.Managers;
using UnityEngine;

namespace Game_Code.Utils {
	public class SetVelocityFromGameTime : MonoBehaviour {
		private void OnEnable() {
			GetComponent<Rigidbody>().velocity = GameManager.SharedInstance.GetCurrentSpeed();
		}

		private void Update() {
			if (gameObject.CompareTag("BackgroundPlane")) {
				GetComponent<Rigidbody>().velocity = GameManager.SharedInstance.GetCurrentSpeed();
			}
		}
	}
}
