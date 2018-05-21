using System;
using UnityEngine;

namespace Game_Code.Utils {
	public class FollowObject : MonoBehaviour {
		public float CatchupSpeed;

		public GameObject TargetObject;
		public String LockCoordinate;

		private void FixedUpdate() {
			Vector3 target = TargetObject.transform.position;
			
			if (LockCoordinate == "z") {
				target.z = transform.position.z;
			} else if (LockCoordinate == "y") {
				target.y = transform.position.y;
			}

			transform.position = Vector3.Lerp(transform.position, target, CatchupSpeed * Time.fixedDeltaTime);
		}
	}
}