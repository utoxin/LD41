using UnityEngine;

namespace Game_Code.Utils {
	public class DestroyByBoundary : MonoBehaviour {
		private void OnTriggerExit(Collider other) {
			if (other.gameObject.CompareTag("Boundary")) {
				if (gameObject.CompareTag("PlayerProjectile") || gameObject.CompareTag("PlayerProjectile2")) {
					gameObject.SetActive(false);
				} else {
					Destroy(gameObject);
				}
			}
		}
	}
}
