using UnityEngine;

namespace Game_Code.Utils {
	public class DestroyByTime : MonoBehaviour {
		public float MaxLifetime;
		private float _birth;

		private void OnEnable() {
			_birth = Time.time;
		}

		private void LateUpdate() {
			if (Time.time - _birth > MaxLifetime) {
				gameObject.SetActive(false);
			}
		}
	}
}