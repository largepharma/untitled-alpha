using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Just making sure the component is here
[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
	CharacterController cc;

	public float speed = 3f;

	float xMove;
	float zMove;

    // Start is called first thing the scene starts
    void Awake()
    {
    	//taking in our keyboard inputs and making the player move around.
        cc = GetComponent<CharacterController> ();
    }

    // Update is called once per frame
    void Update()
    {
        xMove = Input.GetAxisRaw ("Horizontal");
        zMove = Input.GetAxisRaw ("Vertical");
        //As long as we have some kind of motion
        if (xMove != 0 || zMove != 0){
        	cc.SimpleMove(new Vector3(xMove, 0, zMove).normalized * speed);
        }
    }
}
