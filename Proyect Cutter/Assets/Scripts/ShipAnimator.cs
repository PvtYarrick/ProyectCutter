using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAnimator : MonoBehaviour {


    Animator anim;
	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();
		
	}
	
	// Update is called once per frame
	void Update () {

        float move = Input.GetAxis("Horizontal");
        anim.SetFloat("Rotating", move);
		
	}
}
