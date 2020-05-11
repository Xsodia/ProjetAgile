using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    // configuration paramaters
    [SerializeField] float sceneXSize = 21.25f;


    // cached references
    Ball theBall;
    Level level;


    float paddleOffset;
    float sceneLimitLeft;
    float sceneLimitRight;


	// Use this for initialization
	void Start () {

        theBall = FindObjectOfType<Ball>();

        paddleOffset = GetComponent<Collider2D>().bounds.size.x / 2;
        sceneLimitLeft = 10 - sceneXSize / 2 + paddleOffset;
        sceneLimitRight = 10 + sceneXSize / 2 - paddleOffset;

    }
	
	

	void Update () {

        //Get horizontal movement, normalized and smoothed by deltaTime (to avoid fps problems), multiplied by controlled speed.
        float move = Input.GetAxis("Horizontal") * Time.deltaTime * GameSession.instance.paddleMvtSpeed;

        //Adjust the movement to not exceed scene space
        move += transform.position.x;
        move = Mathf.Clamp(move, sceneLimitLeft, sceneLimitRight);
        if(!GameSession.instance.IsAutoPlayEnabled())
        {
            transform.position = new Vector3(move, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(theBall.transform.position.x, transform.position.y, transform.position.z);
        }
        
	}

}
