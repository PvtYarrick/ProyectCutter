using System.Collections.Generic;
using UnityEngine;

public class PipeItem : MonoBehaviour {

	protected Transform rotater;

    //Variables para vida/comportamiento de enemigos metido por nosotros
    protected uint score_enemy;
    protected int enemyLife;
    //public static int shield_count = 3;
    //public ParticleSystem DeathEnemyParticle;
    //hasta aquí

    public List<GameObject> Bulb_meshes;
    public List<Light> Bulb_lights;
    protected int bulb_index;
    public GameObject bulb_broken;
    protected ParticleSystem BrokenBulbParticle;

    private void Awake () {
		rotater = transform.GetChild(0);
	}

	public void Position (Pipe pipe, float curveRotation, float ringRotation) {
        transform.SetParent(pipe.transform, false);
		transform.localRotation = Quaternion.Euler(0f, 0f, -curveRotation);
		rotater.localPosition = new Vector3(0f, pipe.CurveRadius);
		rotater.localRotation = Quaternion.Euler(ringRotation, 0f, 0f);
	}

    /*protected virtual void OnCollisionEnter(Collision ShipCol)
    {
        if (ShipCol.transform.tag == "Ship" && YellowPowerup._shielded == false && enemyLife > 0)
        {

            Destroy(ShipCol.gameObject);
            Levels.dead_ship = true;

        }
        else if (ShipCol.transform.tag == "Ship" && YellowPowerup._shielded == true)
        {
            enemyLife = 0;
            YellowPowerup._shielded = false;
            Score.score = Score.score + (score_enemy * Multiplier._Multiplier);
            PointsAdder.isEnemyDestroyed = true;
            PointsAdder.enemy_destroyed = this;
            Multiplier.MPCounter = Multiplier.MPCounter + (score_enemy / 10);
            Instantiate(DeathEnemyParticle, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
            Multiplier.killing_countdown = Multiplier.count;
        }
    }*/

    public int vida()
    {
        return enemyLife;
    }
    public uint enemyScore()
    {
        return score_enemy;
    }
    public void hit()
    {
        enemyLife -= 1;
        if (bulb_index >= Bulb_lights.Count) {
            return;
        }
        BrokenBulbParticle.Play();
        Bulb_lights[bulb_index].enabled = false;
        Bulb_meshes[bulb_index].GetComponent<MeshFilter>().sharedMesh = bulb_broken.GetComponent<MeshFilter>().sharedMesh;
        bulb_index += 1;
    }  
}