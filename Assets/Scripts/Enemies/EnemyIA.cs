using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] Transform playerPos;
    [SerializeField] float rotateToFacePlayer;
    [SerializeField] float walkSpeed = 0.15f;
    [SerializeField] float attackAfterSecondsInArea = .5f;
    [SerializeField] int enemyNumber = 1;
    //Enemy = Ninja = 1
    //Enemy = Female Ninja = 2
    //Enemy = Tamashi = 3


    [Header("States by Distance")]
    [SerializeField] float distanceToFollowPlayer = 5f;
    [SerializeField] float distanceToStopFollowPlayer = 2f;
    [SerializeField] float distanceToAttack = 1.2f;
    [SerializeField] float distanceToReset = 30f;


    [Header("VFX")]
    [SerializeField] ParticleSystem buffParticle;

    [Header("Player Fight")]
    [SerializeField] FightBehavior playerFight;

    [Header("SFX")]
    [SerializeField] AudioClip powerUPScream;
    [SerializeField] AudioClip powerUPAura;
    [SerializeField] AudioSource soundEffectAudio;

    Rigidbody rb;
    EnemyAnimator enemyAnim;
    EnemyLife enemyLife;

    bool alreadyShowedOff = false;
    bool attackInMemory = false;


    float timeBetwenAttacks = 3f;
    float lastAttackedTime = 0f;

    bool transformed = false;

    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        enemyAnim = GetComponent<EnemyAnimator>();
        enemyLife = GetComponent<EnemyLife>();

        if(enemyNumber == 1)
        {
            enemyAnim.DontSit();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyLife.IsDead())
            return;

        if (enemyLife.isHalfLife() && !transformed)
        {
            transformed = true;
            enemyAnim.PlayPhaseTwo();
            playerFight.Explosion();

            buffParticle.gameObject.SetActive(true);
            buffParticle.Play();


            soundEffectAudio.PlayOneShot(powerUPAura);
        }

        if (playerPos == null)
            return;

        if (enemyNumber == 1)
            LookToPlayer();

        var distance = Vector3.Distance(transform.position, playerPos.position);
        if (enemyNumber == 1 && distance <= distanceToFollowPlayer)
        {
            if (distance < distanceToStopFollowPlayer)
                enemyAnim.Walk(false);
            else
                WalkTwardsThePlayer();
        }

        if (enemyNumber == 1 && distance <= distanceToReset && !alreadyShowedOff)
        {
            alreadyShowedOff = true;
            enemyAnim.PlayTheShowOffAnimation();
            PowerUPScream();
        }
        else if (enemyNumber == 1 && distance <= distanceToAttack && !attackInMemory && Time.time > lastAttackedTime)
        {
            lastAttackedTime = Time.time + timeBetwenAttacks;
            enemyAnim.Attack();
            attackInMemory = false;
        }
    }

    /// <summary>
    /// Look twards? the player
    /// </summary>
    void LookToPlayer()
    {
        //Look To Player
        transform.LookAt(playerPos);
        Quaternion quaternion = transform.rotation;
        quaternion.eulerAngles = new Vector3(0f, transform.rotation.eulerAngles.y - rotateToFacePlayer, 0f);
        transform.rotation = quaternion;
    }

    void WalkTwardsThePlayer()
    {
        Vector3 pos = Vector3.MoveTowards(transform.position, playerPos.position, walkSpeed * Time.deltaTime);
        rb.MovePosition(pos);
        enemyAnim.Walk(true);
    }

    void PowerUPScream()
    {
        audioSource.PlayOneShot(powerUPScream);
    }
}
