using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public float shootSpeed = 0.05f;
    private Vector3 shootspeed;

    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody>().velocity = Vector3.right * shootSpeed / Time.deltaTime;
        Destroy(gameObject, 2f);
    }

    void OnCollisionEnter(Collision col)
    {
        PipeItem obj = col.gameObject.GetComponent<PipeItem>();
        
        if (obj != null)
        {
            
            obj.hit(1);
            /*if (obj.vida() == 1)
            {
                e2.DamageFlash();
            }*/
            //EnemySpawner.damageTaken.Play();
            if (obj.vida() <= 0)
            {
                Destroy(col.gameObject);
                /*Score.score = Score.score + (obj.enemyScore() * Multiplier._Multiplier);
                PointsAdder.isEnemyDestroyed = true;
                PointsAdder.enemy_destroyed = obj;
                Multiplier.MPCounter = Multiplier.MPCounter + (obj.enemyScore() / 10);
                Multiplier.killing_countdown = Multiplier.count;*/
            }
            Destroy(gameObject);
        }
    }
}
