using TMPro;
using UnityEngine;

namespace Game_Code.Utils {
	public class LoadHighScore : MonoBehaviour {
		public TextMeshProUGUI ScoreDisplay;

		private void OnEnable() {
			int highScore = PlayerPrefs.GetInt("highScore", 0);
			ScoreDisplay.text = $"High Score: {highScore:N0}";
		}
	}
}