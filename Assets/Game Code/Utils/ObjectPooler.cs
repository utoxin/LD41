using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game_Code.Utils {
	public class ObjectPooler : MonoBehaviour {
		public static ObjectPooler SharedInstance;

		// Player Bullets
		public List<GameObject> PooledPrimaryBullets;
		public GameObject       PrimaryBulletPrefab;
		public int              PrimaryBulletCount;

		// Player Bullets
		public List<GameObject> PooledSecondaryBullets;
		public GameObject       SecondaryBulletPrefab;
		public int              SecondaryBulletCount;

		// Background Planes
		public List<GameObject> PooledBackgroundPlanes;
		public GameObject       BackgloundPlanePrefab;
		public int              BackgroundPlaneCount;
		
		// Background Planes
		public List<GameObject> PooledObstacles;
		public GameObject       ObstaclePrefab;
		public int              ObstacleCount;
		
		// Score Popups
		public List<GameObject> PooledScores;
		public GameObject       ScorePrefab;
		public int              ScoreCount;
		
		private void Awake () {
			SharedInstance = this;
		}

		private void Start() {
			for (int i = 0; i < PrimaryBulletCount; i++) {
				GameObject obj = Instantiate(PrimaryBulletPrefab);
				obj.SetActive(false);
				PooledPrimaryBullets.Add(obj);
			}

			for (int i = 0; i < SecondaryBulletCount; i++) {
				GameObject obj = Instantiate(SecondaryBulletPrefab);
				obj.SetActive(false);
				PooledSecondaryBullets.Add(obj);
			}

			for (int i = 0; i < BackgroundPlaneCount; i++) {
				GameObject obj = Instantiate(BackgloundPlanePrefab);
				obj.SetActive(false);
				PooledBackgroundPlanes.Add(obj);
			}

			for (int i = 0; i < ObstacleCount; i++) {
				GameObject obj = Instantiate(ObstaclePrefab);
				obj.SetActive(false);
				PooledObstacles.Add(obj);
			}

			for (int i = 0; i < ScoreCount; i++) {
				GameObject obj = Instantiate(ScorePrefab);
				obj.SetActive(false);
				PooledScores.Add(obj);
			}
		}

		public GameObject GetPooledPrimaryBullet() {
			return PooledPrimaryBullets.FirstOrDefault(t => !t.activeInHierarchy);
		}

		public GameObject GetPooledSecondaryBullet() {
			return PooledSecondaryBullets.FirstOrDefault(t => !t.activeInHierarchy);
		}

		public GameObject GetPooledBackgroundPlane() {
			return PooledBackgroundPlanes.FirstOrDefault(t => !t.activeInHierarchy);
		}

		public GameObject GetPooledObstacle() {
			return PooledObstacles.FirstOrDefault(t => !t.activeInHierarchy);
		}

		public GameObject GetPooledScore() {
			return PooledScores.FirstOrDefault(t => !t.activeInHierarchy);
		}
	}
}
