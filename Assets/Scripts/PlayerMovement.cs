﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

	public float Speed;
	public float JumpStrength;
    private int CurrentLevelInt;
	public bool isGrounded = false;
	public bool isDead = false;

    //Animator to cause GameOver
    public Animator GameOver;

	Rigidbody2D rb2d;
	Material playerMat;

	// Use this for initialization
	void Start () {
        CurrentLevelInt = Application.loadedLevel;
        playerMat = GetComponent<Renderer>().material;
		rb2d = GetComponent<Rigidbody2D>();
		playerMat.color = Color.red;
		rb2d.velocity = new Vector2(Speed, 0);
	}
	
	// Update is called once per frame
	void Update () {
		rb2d.velocity = new Vector2(Speed, rb2d.velocity.y);

		if (rb2d.position.y <= -10){
			isDead = true;
		}
		if (isDead){
            Debug.Log("Died");
			rb2d.velocity = rb2d.velocity*(-1);

            //This code added to trigger Gameover Anim.
            GameOver.SetTrigger("isDead");
		}

		if (Input.GetButtonDown("Jump") && isGrounded){
			//print("Jumping");
			rb2d.AddForce(Vector2.up * JumpStrength);
			isGrounded = false;
		}

		if (Input.GetKeyDown(KeyCode.Q)){
			if (playerMat.color == Color.red) {
				playerMat.color = Color.black;
			}
			else if (playerMat.color == Color.black) {
				playerMat.color = Color.red;
			}
		}

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(CurrentLevelInt);
        }


        //This is test code to kill character and view GameOver screen.
        if (Input.GetKeyDown(KeyCode.D))
        {
            isDead = true;
        }


    }

	void OnCollisionEnter2D(Collision2D blockCollision){
		//Debug.Log("blockCollision.gameObject.tag = " + blockCollision.gameObject.tag);
		if (blockCollision.gameObject.tag == "WhitePlatform" || blockCollision.gameObject.tag == "BlackPlatform"){
			isGrounded = true;
		}
		if (blockCollision.gameObject.tag == "Spike"){
			Debug.Log("Fuck, that hurt");
			isDead = true;
		}
	}
}
