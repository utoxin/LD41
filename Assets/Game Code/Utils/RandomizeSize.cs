using UnityEngine;

namespace Game_Code.Utils {
	public class RandomizeSize : MonoBehaviour {
		private void OnEnable() {
			Vector3 randomScale = new Vector3();
			randomScale.x = Random.Range(0.5f, 1.2f);
			randomScale.y = Random.Range(0.5f, 1.2f);
			randomScale.z = Random.Range(0.5f, 1.2f);

			gameObject.transform.localScale = randomScale;
		}
	}
}