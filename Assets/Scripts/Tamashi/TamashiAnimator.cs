using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TamashiAnimator : MonoBehaviour
{
    Animator anim;
    AudioSource audioSource;
    TamashiIA tamashiIA;
    void Start()
    {
        tamashiIA = GetComponent<TamashiIA>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    public void Die()
    {
        anim.ResetTrigger("PhaseStart");
        anim.ResetTrigger("ThrowSpell");
        anim.SetBool("Dead", true);
    }

    public void PhaseStart()
    {
        anim.SetTrigger("PhaseStart");
    }


    public void ThrowFireBallAnimation()
    {
        anim.SetTrigger("ThrowSpell");
    }

    public void StartOutOfManaAnimation()
    {
        anim.SetBool("OutOfMana", true);
    }

    public void ReceiveHit()
    {
        anim.SetTrigger("Hit");
        anim.SetBool("OutOfMana", false);

    }

    #region Events Methods
    public void ThrowFireBallSpell()
    {
        Quaternion spawnRotation = Quaternion.identity;
        if (tamashiIA.GetCurrentPhase() == 2)
            spawnRotation = Quaternion.Euler(0, 0, 90);

        Instantiate(tamashiIA.GetFireBall(), tamashiIA.GetFirePosition().position, spawnRotation);
    }
    #endregion
}
