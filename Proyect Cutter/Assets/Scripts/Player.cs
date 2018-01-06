using UnityEngine;

public class Player : MonoBehaviour
{

    public PipeSystem pipeSystem;

    public float velocity;
    public float rotationVelocity;

    public Pipe currentPipe { get; private set; }

    public int currentPipeSegment { get; private set; }

    private float distanceTraveled;
    private float deltaToRotation;
    private float systemRotation;
    private float worldRotation, avatarRotation;

    private Transform world, rotater;

    //Variables añadidas de codigo anterior
    //Bool para ver si está o no muerto el personaje
    public static bool dead_ship;
    private int score_count = 0;

    //Codigo para el disparo: que se instancia, velocidad de disparo...
    public GameObject Shoot;
    public Transform ShootSpawn;

    private float fireDelta = 0.5F;
    private float nextFire = 1F;
    private float myTime = 0.0F;

    public void Die()
    {
        gameObject.SetActive(false);
        dead_ship = true;
    }

    private void Start()
    {
        world = pipeSystem.transform.parent;
        rotater = transform.GetChild(0);
        currentPipe = pipeSystem.SetupFirstPipe();
        currentPipeSegment = 0;
        SetupCurrentPipe();
    }

    private void Update()
    {

        score_count++;
        myTime = myTime + Time.deltaTime;

        if (!dead_ship)
        {
            if (velocity == 5)
            {
                if (score_count == 5)
                {
                    //Score.score += (Score.score_add * Multiplier._Multiplier);
                    score_count = 0;
                }
                /*currentPos.position = new Vector3(0, 0, 48.5f);
                Music.pitch -= Time.deltaTime * NormalPitch / timeToDecrease;
                if (Music.pitch <= 1)
                {
                    Music.pitch = NormalPitch;
                }*/

            }
            /*else if (Tube.tubeSpeed == SpeedPowerup.ExtraSpeed)
            {

                currentPos.position = new Vector3(0, 0, 52.4f);
                Score.score += (Score.score_add * 2 * Multiplier._Multiplier);
                score_count = 0;
                Music.pitch += Time.deltaTime * NormalPitch / timeToIncrease;
            }*/
        }
        /*if (dead_ship)
        {
            Music.pitch -= Time.deltaTime * NormalPitch / timeToDecrease;
            if (Music.pitch < 0)
            {
                Music.pitch = 0;
            }
    }*/
        if (Input.GetKey("up") && myTime > nextFire)
        {
            //if (!isBlueActive)
            //{
            nextFire = myTime + fireDelta;
            Instantiate(Shoot, ShootSpawn.position, ShootSpawn.rotation);
            nextFire = nextFire - myTime;
            myTime = 0.0F;
            //pew.Play();
            //}
            /*else
            {
                nextFire = myTime + fireDelta;
                Instantiate(Shoot, ShootSpawn.position, Quaternion.identity);
                Instantiate(Shoot, ShootSpawnRight.position, Quaternion.identity);
                Instantiate(Shoot, ShootSpawnLeft.position, Quaternion.identity);
                nextFire = nextFire - myTime;
                myTime = 0.0f;
                threepew.Play();
                isBlueActive = false;
            }*/
        }

        float delta = velocity * Time.deltaTime;
        distanceTraveled += delta;
        systemRotation += delta * deltaToRotation;

        if (systemRotation >= currentPipe.CurveAngle)
        {
            delta = (systemRotation - currentPipe.CurveAngle) / deltaToRotation;
            currentPipe = pipeSystem.SetupNextPipe();
            SetupCurrentPipe();
            systemRotation = delta * deltaToRotation;


        }

        pipeSystem.transform.localRotation = Quaternion.Euler(0f, 0f, systemRotation);

        UpdateAvatarRotation();
    }

    private void UpdateAvatarRotation()
    {
        avatarRotation +=
            rotationVelocity * Time.deltaTime * Input.GetAxis("Horizontal");

        if (avatarRotation < 0f)
            avatarRotation += 360f;
        else if (avatarRotation >= 360f)
            avatarRotation -= 360f;

        rotater.localRotation = Quaternion.Euler(avatarRotation, 0f, 0f);
    }

    private void SetupCurrentPipe()
    {
        deltaToRotation = 360f / (2f * Mathf.PI * currentPipe.CurveRadius);
        worldRotation += currentPipe.RelativeRotation;
        if (worldRotation < 0f)
        {
            worldRotation += 360f;
        }
        else if (worldRotation >= 360f)
        {
            worldRotation -= 360f;
        }
        world.localRotation = Quaternion.Euler(worldRotation, 0f, 0f);
    }
}