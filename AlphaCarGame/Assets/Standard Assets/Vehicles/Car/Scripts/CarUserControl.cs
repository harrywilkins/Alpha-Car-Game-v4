using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
		[Header("Player")]
		[Range(1, 4)]
		public int player = 1;
		public float acceleration;
		public KeyCode handbrakeKey;
		public KeyCode homeTeleport;
		public Vector3 homePos;

		[Header("Shooting")]
		public KeyCode[] shootKeys;
		public float shootSpeed;
		public Transform spawnPos;
		public GameObject block;

        private CarController m_Car; // the car controller we want to use
		Rigidbody rb;
		bool upsideDown;


        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
			upsideDown = false;
			InvokeRepeating ("CheckRotation", 1.0f, 1.5f);
			rb = GetComponent<Rigidbody> ();
        }


        private void FixedUpdate()
        {
            // pass the input to the car!
			float h = CrossPlatformInputManager.GetAxis("Horizontal" + player.ToString());
			float v = CrossPlatformInputManager.GetAxis("Vertical" + player.ToString());
#if !MOBILE_INPUT
			float handbrake;
			if (Input.GetKey(handbrakeKey)) {
				handbrake = 999999999999999999999999999f;
			} else {
				handbrake = 0f;
			}


			float speed = v * acceleration;
            m_Car.Move(h, speed, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif
        }

		void Update() {
			if (Input.GetKeyDown(homeTeleport)) {
				transform.position = homePos;
				rb.velocity = Vector3.zero;
				transform.rotation = Quaternion.identity;
			}

			if (Input.GetKeyDown ("r")) {
				SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
			}

			foreach (KeyCode key in shootKeys) {
				if (Input.GetKeyDown (key)) {
					GameObject blockClone = (GameObject)Instantiate (block, spawnPos.position, transform.rotation);
					blockClone.GetComponent<Rigidbody> ().velocity = transform.forward * shootSpeed;
				}
			}
		}

		void CheckRotation() {
			if (Mathf.Abs(transform.rotation.z * 180) > 110 && Mathf.Abs(transform.rotation.z * 180) < 250 && rb.velocity.magnitude < 2) {
				if (upsideDown) {
					Flip ();
					upsideDown = false;
				} else {
					upsideDown = true;
				}
			} else {
				upsideDown = false;
			}
		}

		void Flip() {
			transform.position += new Vector3 (0f, 2f, 0f);
			transform.rotation = Quaternion.Euler (transform.rotation.x, transform.rotation.y, 0f);
		}
    }
}
