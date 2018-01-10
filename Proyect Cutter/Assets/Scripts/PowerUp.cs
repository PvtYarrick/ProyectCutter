using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    protected Transform rotater;

   

    private void Awake()
    {
        rotater = transform.GetChild(0);
    }

    public void Position(Pipe pipe, float curveRotation, float ringRotation)
    {
        transform.SetParent(pipe.transform, false);
        transform.localRotation = Quaternion.Euler(0f, 0f, -curveRotation);
        rotater.localPosition = new Vector3(0f, pipe.CurveRadius);
        rotater.localRotation = Quaternion.Euler(ringRotation, 0f, 0f);
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name != "Shot")
        {
            Debug.Log("ShieldPickedUp");
            /*enemyLife = 0;
            YellowPowerup._shielded = false;
            Score.score = Score.score + (score_enemy * Multiplier._Multiplier);
            PointsAdder.isEnemyDestroyed = true;
            PointsAdder.enemy_destroyed = this;
            Multiplier.MPCounter = Multiplier.MPCounter + (score_enemy / 10);
            Instantiate(DeathEnemyParticle, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
            Multiplier.killing_countdown = Multiplier.count;
            */
        }
    }

}
