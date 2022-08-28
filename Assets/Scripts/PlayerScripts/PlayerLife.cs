using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] float life = 100;
    [SerializeField] Image blinkImage;

    [Header("VFX particles")]
    [SerializeField] ParticleSystem hitEffect;

    [Header("UI")]
    [SerializeField] Image lifeBar;

    [Header("SFX")]
    [SerializeField] AudioClip[] punchSounds;
    [SerializeField] AudioClip[] walkSounds;
    [SerializeField] AudioClip wakingUp;
    [SerializeField] AudioClip fireHit;
    [SerializeField] AudioClip waterHit;

    float currentLife;
    Animator anim;
    AudioSource audioSource;

    bool isDead = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        currentLife = life;
        PlayWakingUpSound();
    }

    private void Update()
    {
        if (currentLife <= 0 && !isDead)
        {
            isDead = true;
            anim.SetTrigger("Death");

            Invoke("Blink", 7f);
            Invoke("LoadMainMenu", 12f);
        }
    }

    public void Blink()
    {
        LeanTween.alpha(blinkImage.rectTransform, 1f, 4f).setEase(LeanTweenType.easeSpring);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Map");
    }
    public void ResetLife()
    {
        currentLife = life;
        lifeBar.fillAmount = currentLife / life;
    }

    public void ReceiveHit(int damage)
    {
        anim.SetTrigger("Hitted");

        currentLife -= damage;
        lifeBar.fillAmount = currentLife / life;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyHands"))
        {
            Instantiate(hitEffect, other.transform.position, Quaternion.identity);
            ReceiveHit(5);
            PlayHitSound();
        }
        else if (other.CompareTag("FireSpell"))
        {
            Destroy(other.gameObject);
            Instantiate(hitEffect, other.transform.position, Quaternion.identity);
            ReceiveHit(10);
            audioSource.PlayOneShot(fireHit);
        }
        else if (other.CompareTag("WaterSpell"))
        {
            Destroy(other.gameObject);
            Instantiate(hitEffect, other.transform.position, Quaternion.identity);
            ReceiveHit(10);
            audioSource.PlayOneShot(waterHit);
        }
    }

    void PlayHitSound()
    {
        audioSource.PlayOneShot(GetRandomPunchSound());
    }

    void PlayWakingUpSound()
    {
        audioSource.PlayOneShot(wakingUp);
    }
    AudioClip GetRandomPunchSound()
    {
        int r = Random.Range(0, punchSounds.Length);
        return punchSounds[r];
    }

    void PlayRandomWalkSound()
    {
        audioSource.PlayOneShot(GetRandomWalkSound());
    }

    AudioClip GetRandomWalkSound()
    {
        int r = Random.Range(0, walkSounds.Length);
        return walkSounds[r];
    }
}
