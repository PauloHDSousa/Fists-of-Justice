using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TamashiHitDetector : MonoBehaviour
{
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] AudioClip punchSoud;

    TamashiAnimator tamashiAnimator;
    AudioSource audioSource;
    TamashiLife tamashiLife;
    bool wasHit  = false;

    void Start()
    {
        tamashiLife = GetComponent<TamashiLife>();
        audioSource = GetComponent<AudioSource>();
        tamashiAnimator = GetComponent<TamashiAnimator>();
    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHands") && !wasHit)
        {
            wasHit = true;
            Instantiate(hitEffect, other.transform.position, Quaternion.identity);
            audioSource.PlayOneShot(punchSoud);
       
            tamashiLife.ReceiveHit();
        }
    }


    public void CanBeHittedAgain()
    {
        wasHit = false;
    }
}