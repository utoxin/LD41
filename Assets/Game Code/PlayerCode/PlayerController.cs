using System;
using Game_Code.Managers;
using Game_Code.Utils;
using Rewired;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Game_Code.PlayerCode {
	[RequireComponent(typeof(CharacterController))]
	public class PlayerController : MonoBehaviour {
		[SerializeField]
		private float _moveSpeed;

		public GameObject CollisionParticles;
		public GameObject DestructionParticles;

		public int PlayerId; // Rewired Player ID

		private Player              _player; // Rewired player
		private CharacterController _cc;
		private Vector3             _moveVector;

		private int _health = 100;

		// --------------------------------------
		// Details about primary weapon
		[SerializeField]
		private float _primaryShootDelay;

		[SerializeField]
		private float _primaryBulletSpeed;

		private bool _canShootPrimary = true;
		private bool _firePrimary;

		private Vector3 _primaryFireVector;
		// --------------------------------------


		// --------------------------------------
		// Details about secondary weapon
		[SerializeField]
		private float _secondaryShootDelay;

		[SerializeField]
		private float _secondaryBulletSpeed;

		private bool _canShootSecondary = true;

		private bool _fireSecondary;
		// --------------------------------------

		public Image HealthBar;
		public AudioSource BigGun;
		public AudioSource Loss;
		public Image ShotgunTimer;
		private float _firedSecondaryAt = -100;

		private void Awake() {
			_player = ReInput.players.GetPlayer(PlayerId);
			_cc     = GetComponent<CharacterController>();
		}

		private void Update() {
			GetInput();
			ProcessInput();
			UpdateHealth();
			UpdateWeaponCooldowns();
		}

		private void UpdateHealth() {
			HealthBar.fillAmount = _health / 100f;
		}

		private void UpdateWeaponCooldowns() {
			float secondPercent = Mathf.Clamp((Time.time - _firedSecondaryAt) / _secondaryShootDelay, 0, 1);
			ShotgunTimer.fillAmount = secondPercent;
		}

		private void GetInput() {
			_moveVector.x = _player.GetAxis("HorizMovement");
			_moveVector.y = _player.GetAxis("VertMovement");

			GameManager.SharedInstance.UserXaxisInput = _moveVector.x;

			_primaryFireVector.x = _player.GetAxis("HorizAim");
			_primaryFireVector.y = _player.GetAxis("VertAim");
			_firePrimary         = _primaryFireVector.magnitude > 0.1f;

			_fireSecondary = _player.GetButton("FireSecondary");
		}

		private void ProcessInput() {
			if (Math.Abs(_moveVector.x) > 0.001 || Math.Abs(_moveVector.y) > 0.001) {
				_cc.Move(_moveVector * _moveSpeed * Time.deltaTime);
			}

			Vector3 pos = transform.position;
			pos.z = 0;
			transform.position = pos;

			if (_firePrimary && _canShootPrimary) {
				FirePrimary();
			} else if (_fireSecondary && _canShootSecondary) {
				FireSecondary();
			}
		}

		private void FirePrimary() {
			GameObject bullet = ObjectPooler.SharedInstance.GetPooledPrimaryBullet();
			if (bullet != null) {
				bullet.transform.position = transform.position + _primaryFireVector.normalized * 2;
				bullet.transform.rotation = Quaternion.LookRotation(_primaryFireVector.normalized) *
				                            Quaternion.AngleAxis(90, Vector3.right);

				Rigidbody rigidBody = bullet.GetComponent<Rigidbody>();

				// Reset velocity and rotation
				rigidBody.angularVelocity = Vector3.zero;
				rigidBody.velocity        = Vector3.zero;

				bullet.SetActive(true);
				rigidBody.AddForce(_primaryFireVector.normalized * _primaryBulletSpeed,
					ForceMode.VelocityChange);

				_canShootPrimary = false;
			}

			Invoke(nameof(PrimaryShootDelay), _primaryShootDelay);
		}

		private void PrimaryShootDelay() {
			_canShootPrimary = true;
		}

		private void FireSecondary() {
			int numberOfShots = Random.Range(5, 10);

			for (int i = 0; i < numberOfShots; i++) {
				GameObject bullet = ObjectPooler.SharedInstance.GetPooledSecondaryBullet();
				if (bullet != null) {
					bullet.transform.parent = transform;

					Vector3 randomDirection = new Vector3(1, Random.Range(-0.7f, 0.7f), 0).normalized;
					bullet.transform.rotation = Quaternion.Euler(randomDirection) * Quaternion.AngleAxis(90, Vector3.right);
					bullet.transform.position = transform.position + randomDirection * 2;
					
					Rigidbody rigidBody = bullet.GetComponent<Rigidbody>();

					// Reset velocity and rotation
					rigidBody.angularVelocity = Vector3.zero;
					rigidBody.velocity        = Vector3.zero;

					bullet.SetActive(true);
					rigidBody.AddForce(randomDirection.normalized * _secondaryBulletSpeed,
						ForceMode.VelocityChange);

					_canShootSecondary = false;
					_firedSecondaryAt = Time.time;
					BigGun.Play();
				}
			}

			
			Invoke(nameof(SecondaryShootDelay), _secondaryShootDelay);
		}

		private void SecondaryShootDelay() {
			_canShootSecondary = true;
		}

		private void OnCollisionEnter(Collision other) {
			foreach (ContactPoint contact in other.contacts) {
				Instantiate(CollisionParticles, contact.point, Quaternion.Euler(contact.normal));
			}

			if (other.gameObject.CompareTag("Obstacle")) {
				_health -= 10;
				
				if (Random.value < 0.25) {
					Instantiate(DestructionParticles, other.gameObject.transform.position, transform.rotation);
					other.gameObject.SetActive(false);
				}
			} else if (other.gameObject.CompareTag("Barrier")) {
				_health -= 1;
			}

			if (_health <= 0) {
				int score = GameManager.SharedInstance.GetScore();
				int highScore = PlayerPrefs.GetInt("highScore", 0);

				if (score > highScore) {
					PlayerPrefs.SetInt("highScore", score);
				}

				_canShootPrimary = false;
				_canShootSecondary = false;
				Instantiate(DestructionParticles, transform);
				Loss.Play();
				Invoke(nameof(ActuallyReturnToStart), 1);
			}
		}

		private void ActuallyReturnToStart() {
			SceneManager.LoadScene(0);
		}
	}
}