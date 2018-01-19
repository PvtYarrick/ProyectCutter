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
    public Animator deadEnemy_anim;
    public BoxCollider enemyCollider;
    public AudioClip fx_brokenglass;
    

    private void Awake () {
		rotater = transform.GetChild(0);
	}

	public void Position (Pipe pipe, float curveRotation, float ringRotation) {
        transform.SetParent(pipe.transform, false);
		transform.localRotation = Quaternion.Euler(0f, 0f, -curveRotation);
		rotater.localPosition = new Vector3(0f, pipe.CurveRadius);
		rotater.localRotation = Quaternion.Euler(ringRotation, 0f, 0f);
	}


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
            BrokenBulbParticle.Play();
            Bulb_lights[bulb_index].enabled = false;
            Bulb_meshes[bulb_index].GetComponent<MeshFilter>().sharedMesh = bulb_broken.GetComponent<MeshFilter>().sharedMesh;
            bulb_index += 1;
            SoundManager.getInstance().playSoundEffect(fx_brokenglass, 0.5f, 1f);

        
            if (bulb_index >= Bulb_lights.Count)
            {
                return;
            }
        } 

}