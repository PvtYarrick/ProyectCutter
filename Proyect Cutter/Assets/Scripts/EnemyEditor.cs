using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEditor : PipeItem
{
    public uint enemy_score;
    public int life;
    

    void Awake()
    {
        //score_enemy esta en PipeItem, enemy_score hace referencia al score que debe dar cada enemigo,
        //para editarlo desde el inspector
        score_enemy = enemy_score;
        enemyLife = life;
        rotater = transform.GetChild(0);
        bulb_index = 0;
        BrokenBulbParticle = GetComponentInChildren<ParticleSystem>();
        deadEnemy_anim = GetComponentInChildren<Animator>();
        enemyCollider = GetComponentInChildren<BoxCollider>();
    }

    /*protected override void OnCollisionEnter(Collision ShipCol)
    {
        base.OnCollisionEnter(ShipCol);
    }*/
}
