using UnityEngine;

public class Player : MonoBehaviour
{
    private float countDown;
    private const float COUNT = 2f;

    public PipeSystem pipeSystem;

    public Avatar avatar;

    public GameSceneManager manager;

    public ScoreAndSpeed hud;

    public int startAcceleration;

    public float rotationVelocity;

    public Pipe currentPipe { get; private set; }

    public int currentPipeSegment { get; private set; }

    public float distanceTraveled;
    private float deltaToRotation;
    private float systemRotation;
    private float worldRotation, avatarRotation;

    public float startVelocity;

    public float[] accelerations;

    public float acceleration, velocity;

    private Transform world, rotater;

    //Variables añadidas de codigo anterior
    //Bool para ver si está o no muerto el personaje
    public static bool dead_ship;
    private int score_count = 0;

    //Codigo para el disparo: que se instancia, velocidad de disparo...
    public GameObject Shoot;
    public Transform ShootSpawn;

    public float fireDelta;
    public float nextFire;
    public float myTime;

    public void Die()
    {
        //gameObject.SetActive(false);
        manager.DeadPlayer();
    }

    private void Awake()
    {
        dead_ship = false;
        countDown = 0;
    }

    private void Start()
    {
        
        world = pipeSystem.transform.parent;
        rotater = transform.GetChild(0);
        //currentPipe = pipeSystem.SetupFirstPipe();
        //currentPipeSegment = 0;
        //SetupCurrentPipe();
        StartGame(startAcceleration);
    }

    public void StartGame(int accelerationMode)
    {
       
        currentPipe = pipeSystem.SetupFirstPipe();
        currentPipeSegment = 0;

        acceleration = accelerations[accelerationMode];
        velocity = startVelocity;

        hud.SetValues(distanceTraveled, velocity);

        SetupCurrentPipe();

    }

    private void Update()
    {
        if (avatar.goingFast == false)
        {
            velocity += acceleration * Time.deltaTime;
            score_count++;
            myTime = myTime + Time.deltaTime;

            hud.SetValues(distanceTraveled, velocity);
        } else
        {
            velocity += 4 * (acceleration * Time.deltaTime);
            myTime = myTime + Time.deltaTime;

            hud.SetValues(distanceTraveled, velocity);
        }


        if (dead_ship == false && ScoreAndSpeed.iveWon == false)
        {
            if ((Input.GetKey("up") || Input.GetKey("w")) && myTime > nextFire)
            {
                if (avatar.poweredUp == false)
                {
                    fireDelta = 0.5F;
                    nextFire = 1F;
                    myTime = 0.0F;
                }
                else if (avatar.poweredUp == true)
                {
                    fireDelta = 0.2F;
                    nextFire = 0.5F;
                    myTime = 0.0F;
                }
                nextFire = myTime + fireDelta;

                GameObject newBullet = Instantiate(Shoot,Vector3.zero, Quaternion.identity);

                //Vector3.zero
                newBullet.GetComponent<BulletPipeFollow>().SetInitialTarget(currentPipe, currentPipe.getClosestSegment(newBullet.transform.position), rotater.localRotation, pipeSystem);

                nextFire = nextFire - myTime;
                myTime = 0.0F;
            }
    

    UpdateAvatarRotation();
        }
        float delta = velocity * Time.deltaTime;
        distanceTraveled += delta;
        systemRotation += delta * deltaToRotation;

        if (systemRotation >= currentPipe.CurveAngle)
        {

            delta = (systemRotation - currentPipe.CurveAngle) / deltaToRotation;

            
                currentPipe = pipeSystem.SetupNextPipe();
                SetupCurrentPipe();
           if (currentPipe.isVisible == false)
            {
                countDown = COUNT;
            }

            systemRotation = delta * deltaToRotation;


        }
        pipeSystem.transform.localRotation = Quaternion.Euler(0f, 0f, systemRotation);
        if (countDown > 0)
        {
            countDown -= Time.deltaTime;
            if (countDown <= 0)
            {
                manager.WinConMet();
            }
        }

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
        if (currentPipe.CurveRadius == 0f)
        {
            deltaToRotation = 360f / (2f * Mathf.PI * 0.01f);
        }
        else
        {
            deltaToRotation = 360f / (2f * Mathf.PI * currentPipe.CurveRadius);
        }
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