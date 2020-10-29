using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

	public float carSpeed;
	public float maxPos = 2.2f;	
	public audioManager am;
	bool currentPlateformAndroid = false;
	Rigidbody2D rb;

	Vector3 position;

	public UiManager ui;

	public void Awake(){
	
		rb = GetComponent<Rigidbody2D> ();
		#if UNITY_ANDROID
			currentPlateformAndroid = true;
		#else
			currentPlateformAndroid = false;
		#endif
	}


	// Use this for initialization
	void Start () {
		am.carSound.Play();
		position = transform.position;

		if (currentPlateformAndroid) {

			Debug.Log ("Android");
		} else {
			Debug.Log ("Window");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if (currentPlateformAndroid) {
			AccelrometerMove();

		} else {
			position.x += Input.GetAxis ("Horizontal") * carSpeed * Time.deltaTime;
		}
		position = transform.position;
		position.x = Mathf.Clamp (position.x, -2.2f, 2.2f);
		transform.position = position;
	}

	void OnCollisionEnter2D(Collision2D col){

		if (col.gameObject.tag == "Enemy Car") {
			//Destroy (gameObject);
			gameObject.SetActive(false);
			ui.gameOver ();
			am.carSound.Stop ();
			am.carDistorySound.Play();

		}
	}
		

	void AccelrometerMove(){

		float x = Input.acceleration.x;

		if (x < -0.1f) {
			MoveLeft ();
		} else if (x > 0.1f) {
			MoveRight ();
		} else {
			setVelocityZero ();
		}

	}

	void TouchMove(){
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch (0);

			float middle = Screen.width/2;

			if (touch.position.x < middle && touch.phase == TouchPhase.Began) {
				MoveLeft ();
			} else if (touch.position.x > middle && touch.phase == TouchPhase.Began) {
				MoveRight ();
			} else {
				setVelocityZero ();		
			}
		}
	}

	public void MoveLeft(){
		
			rb.velocity = new Vector2(-carSpeed,0); //(i, 0);	
	}

	public void MoveRight(){
		
			rb.velocity = new Vector2 (carSpeed, 0);
	}

	public void setVelocityZero(){
		rb.velocity = Vector2.zero;
	}
}
