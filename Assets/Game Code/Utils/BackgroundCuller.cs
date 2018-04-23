using System.Collections.Generic;
using System.Linq;
using Game_Code.Managers;
using UnityEngine;

namespace Game_Code.Utils {
	public class BackgroundCuller : MonoBehaviour {
		public GameObject Player;
		public float MaxDistanceFromPlayer;
		public float SpawnCheckDistance;
		private float _lastDistanceFromPlayer = float.MaxValue;

		private void OnEnable() {
			Player = GameObject.FindWithTag("Player");
		}

		private void LateUpdate() {
			float currentPlayerDistance = Vector3.Distance(transform.position, Player.transform.position);

			if (currentPlayerDistance > MaxDistanceFromPlayer) {
				gameObject.SetActive(false);
				return;
			}
			
			// Last tick, we were outside the spawncheck distance, this tick we're inside it. Run neighbor spawns.
			if (currentPlayerDistance < SpawnCheckDistance && _lastDistanceFromPlayer > SpawnCheckDistance) {
				Vector3 relativePosition = transform.position - Player.transform.position;
				relativePosition.z = 0;

				PlaceVerticalNeighbor(relativePosition);
				PlaceHorizontalNeighbor(relativePosition);
			}

			_lastDistanceFromPlayer = currentPlayerDistance;
		}

		private void PlaceVerticalNeighbor(Vector3 relativePos) {
			Vector3 newPos = new Vector3(transform.position.x, 0, transform.position.z);

			if (relativePos.y > 0) {
				newPos.y = transform.position.y + 100;
			} else {
				newPos.y = transform.position.y - 100;				
			}
			
			if (IsLocationEmpty(newPos)) {
				GameObject obj = ObjectPooler.SharedInstance.GetPooledBackgroundPlane();
				obj.transform.parent = transform.parent;
				obj.transform.position = newPos;
				obj.SetActive(true);
				obj.GetComponent<Rigidbody>().velocity = GameManager.SharedInstance.GetCurrentSpeed();
			}
		}

		private void PlaceHorizontalNeighbor(Vector3 relativePos) {
			Vector3 newPos = new Vector3(0, transform.position.y, transform.position.z);

			if (relativePos.x > 0) {
				newPos.x = transform.position.x + 100;
			} else {
				newPos.x = transform.position.x - 100;				
			}

			if (IsLocationEmpty(newPos)) {
				GameObject obj = ObjectPooler.SharedInstance.GetPooledBackgroundPlane();
				obj.transform.parent = transform.parent;
				obj.transform.position = newPos;
				obj.SetActive(true);
				obj.GetComponent<Rigidbody>().velocity = GameManager.SharedInstance.GetCurrentSpeed();
			}
		}

		private bool IsLocationEmpty(Vector3 checkPosition) {
			List<Transform> backgroundPositions = transform.parent.gameObject.GetComponentsInChildren<Transform>().ToList();
			return !backgroundPositions.Any(p => p != null && (Vector3.Distance(p.position, checkPosition) < 1));
		}
	}
}
