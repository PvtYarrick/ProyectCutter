using UnityEngine;

public class Shoot : MonoBehaviour
{

    public float shootSpeed = 0.05f;
    private Vector3 shootspeed;

    public float lifeTime = 2f;

    public AudioClip fx_shoot;

    // Use this for initialization
    void Start()
    {
        //GetComponent<Rigidbody>().velocity = Vector3.right * shootSpeed / Time.deltaTime;
        Destroy(transform.parent.parent.gameObject, lifeTime);
        SoundManager.getInstance().playSoundEffect(fx_shoot, 0.6f);
    }

    void OnCollisionEnter(Collision col)
    {
        PipeItem obj = col.gameObject.GetComponent<PipeItem>();
        
        if (obj != null)
        {
            
            obj.hit();
            //if (obj.vida() == 1)
            //{

            //}

            if (obj.vida() <= 0)
            {
                //Destroy(col.gameObject);
                obj.deadEnemy_anim.SetTrigger("enemyDies");
                Score.score = Score.score + (obj.enemyScore() * Multiplier._Multiplier);
                AddPoints.isEnemyDestroyed = true;
                AddPoints.enemy_destroyed = obj;

                Multiplier.MPCounter = Multiplier.MPCounter + (obj.enemyScore() / 10);
                Multiplier.killing_countdown = Multiplier.count;

                Destroy(obj.gameObject, obj.deadEnemy_anim.GetCurrentAnimatorStateInfo(0).length);
                obj.enemyCollider.enabled = false;
            }

            Destroy(transform.parent.gameObject);
        }
    }
}
