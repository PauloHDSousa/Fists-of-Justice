using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitDetector : MonoBehaviour
{

    [SerializeField] float KnockbackForce = 20f;
    [SerializeField] ParticleSystem hitEffect;

    Animator anim;
    float nextKnockoutTime;
    float KnockoutTime = .3f;
    float resetCombo = 3f;

    int comboPosition;

    EnemyLife enemyLife;

    void Start()
    {
        anim = GetComponent<Animator>();
        enemyLife = GetComponent<EnemyLife>();
    }

    void Update()
    {
        ResetComboAnim();
        if (Time.time > resetCombo && comboPosition > 0)
            comboPosition = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(enemyLife.IsDead())
            return;

        if (other.CompareTag("PlayerHands") && Time.time > nextKnockoutTime)
        {
            Instantiate(hitEffect, other.transform.position, Quaternion.identity);
            enemyLife.ReceiveHit(25);

            resetCombo = Time.time + .8f;

            if (comboPosition == 3)
                comboPosition = 0;

            comboPosition++;

            Debug.Log("collider Enter" + comboPosition);
            SetComboAnim(comboPosition);

            nextKnockoutTime = Time.time + KnockoutTime;

            //Look at the player
            other.gameObject.SetActive(false);
        }
    }

    void SetComboAnim(int comboPosition)
    {

        if (comboPosition == 1)
        {
            anim.SetBool("H1", true);
            anim.SetBool("H2", false);
            anim.SetBool("H3", false);

        }
        else if (comboPosition == 2)
        {
            anim.SetBool("H1", false);
            anim.SetBool("H2", true);
            anim.SetBool("H3", false);

        }
        else if (comboPosition == 3)
        {
            anim.SetBool("H1", false);
            anim.SetBool("H2", false);
            anim.SetBool("H3", true);

            comboPosition = 0;
        }
    }

    void ResetComboAnim()
    {
        float currentAnimTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (currentAnimTime > 0.6f && anim.GetCurrentAnimatorStateInfo(0).IsName("Hurt_1"))
            anim.SetBool("H1", false);
        else if (currentAnimTime > 0.66f && anim.GetCurrentAnimatorStateInfo(0).IsName("Hurt_2"))
            anim.SetBool("H2", false);
        else if (currentAnimTime > 0.4f && anim.GetCurrentAnimatorStateInfo(0).IsName("Hurt_3"))
            anim.SetBool("H3", false);
    }
}