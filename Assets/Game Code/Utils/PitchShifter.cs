using UnityEngine;

namespace Game_Code.Utils {
	public class PitchShifter : MonoBehaviour {
		private void OnEnable() {
			gameObject.GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.1f);
		}
	}
}