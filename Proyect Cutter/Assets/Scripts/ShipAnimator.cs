using UnityEngine;

public class ShipAnimator : MonoBehaviour {


    Animator anim;
    private ParticleSystem speed_particles;
    

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
