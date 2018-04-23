using Game_Code.Managers;
using UnityEngine;

namespace Game_Code.Utils {
	public class DestroyByCollision : MonoBehaviour {
		public GameObject CollisionParticles;
		public GameObject DestructionParticles;

		private void OnCollisionEnter(Collision other) {
			if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("Barrier")) {
				foreach (ContactPoint contact in other.contacts) {
					Instantiate(CollisionParticles, contact.point, Quaternion.Euler(contact.normal));
				}

				if (other.gameObject.CompareTag("Obstacle") && (gameObject.CompareTag("PlayerProjectile") && Random.value < 0.1 ||
				    gameObject.CompareTag("PlayerProjectile2") && Random.value < 0.75)) {
					GameManager.SharedInstance.AddPoints(100);

					Instantiate(DestructionParticles, other.gameObject.transform.position, transform.rotation);

					GameObject scoreObj = ObjectPooler.SharedInstance.GetPooledScore();
					scoreObj.transform.position = other.gameObject.transform.position;
					scoreObj.SetActive(true);
					scoreObj.GetComponent<Rigidbody>().velocity = GameManager.SharedInstance.GetCurrentSpeed();

					other.gameObject.SetActive(false);
				}

				gameObject.SetActive(false);
			}
		}
	}
}