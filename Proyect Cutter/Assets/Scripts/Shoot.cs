using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public AudioClip fx_shoot;


    SphereCollider sphere;

    // Use this for initialization
    void Start()
    {
        SoundManager.getInstance().playSoundEffect(fx_shoot, 0.6f);
        sphere = GetComponent<SphereCollider>();

        StartCoroutine(ActivateCollider());
    }


    IEnumerator ActivateCollider()
    {
        yield return new WaitForSeconds(0.2f);

        sphere.enabled = true;
    }

    void OnCollisionEnter(Collision col)
    {
        PipeItem obj = col.gameObject.GetComponent<PipeItem>();
        
        if (obj != null)
        {
            
            obj.hit();
            

            if (obj.vida() <= 0)
            {
                //Destroy(col.gameObject);
                obj.deadEnemy_anim.SetTrigger("enemyDies");
                Score.score = Score.score + (obj.enemyScore() * Multiplier._Multiplier);
                AddPoints.isEnemyDestroyed = true;
                AddPoints.enemy_destroyed = obj;
                ScoreAndSpeed.deadEnemies++;

                Multiplier.MPCounter = Multiplier.MPCounter + (obj.enemyScore() / 10);
                Multiplier.killing_countdown = Multiplier.count;

                Destroy(obj.gameObject, obj.deadEnemy_anim.GetCurrentAnimatorStateInfo(0).length);
                obj.enemyCollider.enabled = false;
            }

            
            Destroy(transform.parent.parent.gameObject);
        }
    }
}
