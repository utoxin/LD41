using UnityEngine;

namespace Game_Code.Utils {
	public class BackgroundPlacer : MonoBehaviour {
		public int PlacementOffset;
		public int PlacementDepth;

		// Use this for initialization
		void Start () {
			for (int x = -1; x < 2; x++) {
				for (int y = -1; y < 2; y++) {
					Vector3 snappedCameraPos = Camera.main.transform.position;
					snappedCameraPos.x = snappedCameraPos.x / 100;
					snappedCameraPos.y = snappedCameraPos.y / 100;
					
					Vector3 newPos = new Vector3(transform.position.x + x * PlacementOffset, transform.position.y + y * PlacementOffset, PlacementDepth);
					GameObject newPlane = ObjectPooler.SharedInstance.GetPooledBackgroundPlane();
					newPlane.transform.parent = transform;
					newPlane.transform.position = newPos;
					newPlane.SetActive(true);
					newPlane.GetComponent<Rigidbody>().velocity = new Vector3(-10, 0, 0);
				}
			}
		}
	}
}
