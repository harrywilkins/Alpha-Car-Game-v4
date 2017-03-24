using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

	public Vector3 teleportPos;

	void OnCollisionEnter(Collision collision) {
		collision.transform.position = teleportPos;
		try {
			collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		} catch {
			Debug.Log ("Couldn't teleport!");
		}
	}
}
