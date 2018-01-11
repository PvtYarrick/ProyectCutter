using UnityEngine;

public class Avatar : MonoBehaviour {

	public ParticleSystem shape, trail, burst;

	private Player player;
    public MeshRenderer shield_renderer;

	public float deathCountdown = -1f;

    public bool isShieldUp = false;

   
    public bool goingFast = false;
    
    public float stopRunning = 0f;

    //public GameObject shieldBarrier;

	private void Awake () {
		player = transform.root.GetComponent<Player>();
        
        
	}
    private void Start()
    {
        shield_renderer.enabled = false;
    }

	private void OnTriggerEnter (Collider collider) {

        if (collider.tag == "Enemy")
        {
            Debug.Log(collider.tag);
            if (deathCountdown < 0f && isShieldUp == false)
            {
                shape.enableEmission = false;
                trail.enableEmission = false;
                burst.Emit(burst.maxParticles);
                deathCountdown = burst.startLifetime;
            }else
            {
                isShieldUp = false;
                shield_renderer.enabled = false;
            }
        }else if(collider.name == "mesh_shield")
        {
            isShieldUp = true;
            shield_renderer.enabled = true;
            //Instantiate(shieldBarrier,avatar.transform.position, Quaternion.identity);
            Destroy(collider.gameObject);

        }else if (collider.name == "mesh_speed")
        {
            goingFast = true;
            stopRunning = stopRunning + 5f;
            Destroy(collider.gameObject);
        }
    }
	
	private void Update () {
        if (stopRunning >= 0f)
        {
            stopRunning = stopRunning - (1f * Time.deltaTime);
        }
        else
        {
            goingFast = false;
        }



        if (deathCountdown >= 0f) {
			deathCountdown -= Time.deltaTime;
			if (deathCountdown <= 0f) {
				deathCountdown = -1f;
				shape.enableEmission = true;
				trail.enableEmission = true;
				player.Die();
			}
		}
	}
}