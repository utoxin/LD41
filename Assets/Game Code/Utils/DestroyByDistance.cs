using UnityEngine;

namespace Game_Code.Utils {
	public class DestroyByDistance : MonoBehaviour {
		private GameObject _player;
		public float MaxDistanceFromPlayer;

		private void OnEnable() {
			_player = GameObject.FindWithTag("Player");
		}
		
		private void LateUpdate() {
			float currentPlayerDistance = Vector3.Distance(transform.position, _player.transform.position);

			if (currentPlayerDistance > MaxDistanceFromPlayer) {
				gameObject.SetActive(false);
			}
		}
	}
}