using Game_Code.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game_Code.Managers {
	public class ObstacleManager : MonoBehaviour {
		public float StartingDelay;
		public int MaxPerWave;
		public GameObject Player;

		private float _timerStarted;
		private float _originalStart;

		private void Start() {
			_timerStarted = Time.time;
			_originalStart = Time.time;
		}

		private void Update() {
			if (Time.time > _timerStarted + CalculateDelay()) {
				int waveSize = Random.Range(1, MaxPerWave);
				for (int i = 0; i < waveSize; i++) {
					GameObject obj = ObjectPooler.SharedInstance.GetPooledObstacle();
					if (obj != null) {
						obj.transform.position =
							new Vector3(
								Random.Range(Player.transform.position.x + 50, Player.transform.position.x + 100),
								Random.Range(Player.transform.position.y - 45, Player.transform.position.y + 45), 0);
						obj.SetActive(true);
					}
				}

				_timerStarted = Time.time;
			}
		}

		private float CalculateDelay() {
			return StartingDelay - Mathf.Pow(Time.time - _originalStart, 0.25f);
		}
	}
}