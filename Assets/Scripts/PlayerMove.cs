using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerMove : MonoBehaviour {
	public static PlayerMove Instance;

	Rigidbody Rig;
	[Space(20)]
	public float Forvard_Speed;
	public float Rotate_Speed;
	public float Back_Speed;
	float Vertical_Axis;
	float Horizontal_Axis;
	// public Joystick Joy;
	public bool Action;

	void Awake(){
		Instance = this;
		Rig = GetComponent<Rigidbody> ();
	}
	void Start () {

	}
	void Update(){
	}
	// Update is called once per frame
	void FixedUpdate () {

		Vertical_Axis = Input.GetAxis ("Vertical");
		Horizontal_Axis = Input.GetAxis ("Horizontal");

		//  Vertical_Axis = CrossPlatformInputManager.GetAxis ("Vertical");
		//  Horizontal_Axis = CrossPlatformInputManager.GetAxis ("Horizontal");
		if(!Action){
		if (Vertical_Axis > 0) {
			Rig.MovePosition(transform.position + transform.forward * Forvard_Speed * Vertical_Axis * Time.deltaTime);
		}
		if (Vertical_Axis < 0) {
			Rig.MovePosition (transform.position + transform.forward * Back_Speed * Vertical_Axis* Time.deltaTime);
		}
		if (Horizontal_Axis > 0.55f) {
			transform.Rotate (0, +Rotate_Speed * Time.deltaTime, 0);
		}
		if (Horizontal_Axis < -0.55f) {
			transform.Rotate (0, -Rotate_Speed * Time.deltaTime, 0);
		}
		//  if (Horizontal_Axis == 0 && Vertical_Axis == 0) {
		//  }
		}
	}

	public void ChangeRotation(){
		//transform.rotation = 
	}

}
