using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLife : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] float life = 500;

    [Header("UI")]
    [SerializeField] Image lifeBar;
    [SerializeField] TMPro.TMP_Text tmp_defeated_boss;


    [Header("SFX")]
    [SerializeField] AudioClip[] punchSounds;
    [SerializeField] AudioClip deathSound;

    float currentLife;
    AudioSource audioSource;
    EnemyAnimator enemyAnim;
    Rigidbody rb;
    bool dead = false;

    public bool IsDead()
    {
        return dead;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemyAnim = GetComponent<EnemyAnimator>();
        audioSource = GetComponent<AudioSource>();
        currentLife = life;
    }
    private void Update()
    {
        if (currentLife <= 0 && !dead)
        {
            dead = true;
            enemyAnim.Die();
            audioSource.PlayOneShot(deathSound);
            var bossEvents = FindObjectOfType<BossEvents>();
            bossEvents.FirstBossKilled();
            LeanTween.value(tmp_defeated_boss.gameObject, a => tmp_defeated_boss.color = a, new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), 10);

            var playerLife = FindObjectOfType<PlayerLife>();
            playerLife.ResetLife();
        }
    }

    void HideBossDefeated()
    {
        LeanTween.value(tmp_defeated_boss.gameObject, a => tmp_defeated_boss.color = a, new Color(1, 1, 1, 0), new Color(1, 1, 1, 0), 15);
    }

    public void ReceiveHit(int damage)
    {
        currentLife -= damage;
        lifeBar.fillAmount = currentLife / life;
        PlayHitSound();
    }

    public bool isHalfLife()
    {
        return currentLife < life / 2;
    }

    void PlayHitSound()
    {
        audioSource.PlayOneShot(GetRandomPunchSound());
    }
    AudioClip GetRandomPunchSound()
    {
        int r = Random.Range(0, punchSounds.Length);
        return punchSounds[r];
    }
}
