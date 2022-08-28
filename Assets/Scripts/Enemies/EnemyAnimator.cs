using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator anim;

    int avaliableAttacks = 4;

    [Header("Attack Config")]
    [SerializeField] float delayBetweenAttacks = .4f;

    float lastAttackedTime = 0f;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayTheShowOffAnimation()
    {
        anim.SetTrigger("ShowOff");
    }

    public void PlayPhaseTwo()
    {
        anim.SetTrigger("PhaseTwo");
        ResetAnimations();
    }

    public void DontSit()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Sitting", false);
    }

    public void Attack()
    {
        if (Time.time < lastAttackedTime)
            return;

        int attack = Random.Range(1, avaliableAttacks + 1);
        anim.SetTrigger("Attack_" + attack);
        lastAttackedTime = Time.time + delayBetweenAttacks;
    }

    public void Walk(bool walking)
    {
        anim.SetBool("Walking", walking);
    }

    public void Die()
    {
        anim.SetTrigger("Die");
    }

    void ResetAnimations()
    {
        anim.ResetTrigger("Attack_1");
        anim.ResetTrigger("Attack_2");
        anim.ResetTrigger("Attack_3");
        anim.ResetTrigger("Attack_4");

        anim.SetBool("H1", false);
        anim.SetBool("H2", false);
        anim.SetBool("H3", false);
    }

}
