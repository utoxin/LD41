using UnityEngine;

namespace Game_Code.Utils {
	public class RandomizeColor : MonoBehaviour {
		private void OnEnable() {
			Color randColor = new Color();
			randColor.r = Random.value;
			randColor.g = Random.value;
			randColor.b = Random.value;
			randColor.a = Random.value;

			gameObject.GetComponent<MeshRenderer>().materials[0].color = randColor;
		}
	}
}