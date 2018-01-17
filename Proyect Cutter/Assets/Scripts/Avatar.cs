using UnityEngine;

public class Avatar : MonoBehaviour {

	public ParticleSystem shape, trail, burst;

	private Player player;
    public MeshRenderer shield_renderer;

	public float deathCountdown = -1f;

    public bool isShieldUp = false;

   
    public bool goingFast = false;
    
    public float stopRunning = 0f;

    public ParticleSystem speed_particles;
    public ParticleSystem lostshield_particles;

    public AudioClip fx_gotShield;
    public AudioClip fx_gotSpeed;
    public AudioClip fx_lostShield;


    private void Awake () {
		player = transform.root.GetComponent<Player>();

    }
    private void Start()
    {
        shield_renderer.enabled = false;
        speed_particles.Stop();
        lostshield_particles.Stop();
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
                SoundManager.getInstance().playSoundEffect(fx_lostShield, 100f, 1.3f);
                lostshield_particles.Play();
                isShieldUp = false;
                shield_renderer.enabled = false;
            }
        }else if(collider.name == "mesh_shield" && isShieldUp == false)
        {
            isShieldUp = true;
            shield_renderer.enabled = true;
            SoundManager.getInstance().playSoundEffect(fx_gotShield, 10f, 1.3f);
            //Instantiate(shieldBarrier,avatar.transform.position, Quaternion.identity);
            Destroy(collider.gameObject);

        }else if (collider.name == "mesh_speed")
        {
            goingFast = true;
            SoundManager.getInstance().playSoundEffect(fx_gotSpeed, 10f, 1f);
            stopRunning = stopRunning + 5f;
            Destroy(collider.gameObject);
            speed_particles.Play();
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