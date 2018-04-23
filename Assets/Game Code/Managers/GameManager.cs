using TMPro;
using UnityEngine;

namespace Game_Code.Managers {
	public class GameManager : MonoBehaviour {

		public float StartingSpeed;
		public float SpeedBoostPerSecond;
		public TextMeshProUGUI MphDisplay;
		public TextMeshProUGUI ScoreDisplay;
		
		public static GameManager SharedInstance;

		private int _score;
		private float _currentSpeed;

		[HideInInspector]
		public float UserXaxisInput;

		private void Awake() {
			SharedInstance = this;
			_currentSpeed = StartingSpeed;
			Time.timeScale = 1;
		}

		private void Update() {
			UpdateScoreWithSpeed();
			UpdateUI();
			UpdateSpeed();
		}

		public void AddPoints(int points) {
			_score += points;
		}
		
		private void UpdateScoreWithSpeed() {
			_score -= (int) (GetCurrentSpeed().x / 10 * Time.timeScale);
		}

		public int GetScore() {
			return _score;
		}

		// ReSharper disable once InconsistentNaming
		private void UpdateUI() {
			MphDisplay.text = $"{GetCurrentSpeed().x * -1:0.0} MPH";
			ScoreDisplay.text = $"Score: {_score:N0}";
		}

		private void UpdateSpeed() {
			_currentSpeed += Time.timeScale * (SpeedBoostPerSecond * (UserXaxisInput + 0.5f) * Time.deltaTime +
			                 Mathf.Pow(SpeedBoostPerSecond * Time.deltaTime, 1.1f));
		}

		public Vector3 GetCurrentSpeed() {
			return new Vector3(-1 * _currentSpeed, 0f, 0f);
		}
	}
}
