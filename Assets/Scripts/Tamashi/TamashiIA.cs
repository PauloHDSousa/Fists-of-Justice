using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TamashiIA : MonoBehaviour
{

    [Header("General")]
    [SerializeField] GameObject player;
    [SerializeField] ParticleSystem puffEffect;
    [SerializeField] Transform firePosition;
    [SerializeField] float rotateToFacePlayer;
    [SerializeField] AudioClip bossSound;

    [Header("First Phase Skills Fire")]
    [SerializeField] GameObject fireBall;
    [SerializeField] GameObject fireBarrier;
    [SerializeField] GameObject firePlace;
    [SerializeField] float delayFireBall;
    [SerializeField] AudioClip spellSound;
    [SerializeField] AudioClip phaseStart;
    [SerializeField] Transform secondPhasePosition;


    [Header("Water Phase Skills Fire")]
    [SerializeField] GameObject waterBall;
    [SerializeField] GameObject waterBarrier;
    [SerializeField] GameObject waterPlace;
    [SerializeField] float delayWaterBall;
    [SerializeField] AudioClip waterSpellSound;
    [SerializeField] AudioClip waterPhaseStart;



    TamashiAnimator anim;
    float timeToUseSkillAgain;
    int currentPhase = 1;
    bool shieldCreated = false;
    bool isOutOfMana = false;
    AudioSource audioSource;


    bool started = false;

    GameObject fireBarrierRef;
    GameObject firePlaceRef;


    GameObject barrierRef;
    GameObject placeRef;


    TamashiHitDetector tamashiHitDetector;

    TamashiLife tamashiLife;
    void Start()
    {
        timeToUseSkillAgain = Time.time + delayFireBall + 2f;
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<TamashiAnimator>();
        tamashiHitDetector = GetComponent<TamashiHitDetector>();
        tamashiLife = GetComponent<TamashiLife>();
    }



    void Update()
    {
        if (currentPhase == 3 || tamashiLife.isTamashiDead())
            return;

        var distance = Vector3.Distance(transform.position, player.transform.position);
        if(!started && distance <= 29)
            started = true;

        LookToPlayer();

        if (started)
        {

            if (currentPhase == 1)
            {
                FirePhase();
            }
            else if (currentPhase == 2)
            {
                WaterPhase(waterPhaseStart, waterSpellSound, waterBarrier, waterPlace, delayWaterBall);
            }
        }
    }

    /// <summary>
    /// Look to the player
    /// </summary>
    void LookToPlayer()
    {
        //Look To Player
        transform.LookAt(player.transform);
        Quaternion quaternion = transform.rotation;
        quaternion.eulerAngles = new Vector3(0f, transform.rotation.eulerAngles.y - rotateToFacePlayer, 0f);
        transform.rotation = quaternion;
    }

    /// <summary>
    /// Fire Phase
    /// </summary>
    void FirePhase()
    {
        if (!shieldCreated)
        {
            anim.PhaseStart();
            Vector3 barrierPosition = new Vector3(transform.position.x, .2f, transform.position.z + 1f);
            fireBarrierRef = Instantiate(fireBarrier, barrierPosition, Quaternion.identity);
            firePlaceRef = Instantiate(firePlace, barrierPosition, Quaternion.identity);

            shieldCreated = true;

            audioSource.PlayOneShot(phaseStart);
        }

        FireBall[] fireBalls = fireBarrierRef.GetComponentsInChildren<FireBall>();
        if (fireBalls.Length > 0)
        {
            if (Time.time >= timeToUseSkillAgain)
            {
                timeToUseSkillAgain = Time.time + delayFireBall;
                audioSource.PlayOneShot(spellSound);
                anim.ThrowFireBallAnimation();
                Destroy(fireBalls[0].gameObject);
            }
        }
        else if (!isOutOfMana)
        {
            fireBarrierRef.SetActive(false);
            isOutOfMana = true;
            anim.StartOutOfManaAnimation();
        }
    }

    /// <summary>
    /// Generic Phase
    /// </summary>
    /// <param name="_phaseStart"></param>
    /// <param name="_spellSound"></param>
    /// <param name="barrier"></param>
    /// <param name="skillDelay"></param>
    void WaterPhase(AudioClip _phaseStart, AudioClip _spellSound, GameObject barrier, GameObject waterPlace, float skillDelay)
    {
        if (!shieldCreated)
        {
            Instantiate(puffEffect, transform.position, Quaternion.identity);
            transform.position = secondPhasePosition.position;
            anim.PhaseStart();
            Vector3 barrierPosition = new Vector3(transform.position.x - 3f, -1.75f, transform.position.z + 1f);
            barrierRef = Instantiate(barrier, barrierPosition, Quaternion.identity);
            placeRef = Instantiate(waterPlace, barrierPosition, Quaternion.identity);

            shieldCreated = true;
            audioSource.PlayOneShot(_phaseStart);
            tamashiHitDetector.CanBeHittedAgain();
        }

        FireBall[] fireBalls = barrierRef.GetComponentsInChildren<FireBall>();
        if (fireBalls.Length > 0)
        {
            if (Time.time >= timeToUseSkillAgain)
            {
                timeToUseSkillAgain = Time.time + skillDelay;
                audioSource.PlayOneShot(_spellSound);
                anim.ThrowFireBallAnimation();
                Destroy(fireBalls[0].gameObject);
            }
        }
        else if (!isOutOfMana)
        {
            barrierRef.SetActive(false);
            isOutOfMana = true;
            anim.StartOutOfManaAnimation();
        }
    }

    public void GoToTheNextPhase()
    {

        if (currentPhase == 1)
            Destroy(firePlaceRef);
        else if (currentPhase == 2)
            Destroy(placeRef);


        currentPhase++;

        if (currentPhase <= 2)
        {
            isOutOfMana = false;
            shieldCreated = false;
        }
    }
    public GameObject GetFireBall()
    {
        if (currentPhase == 1)
            return fireBall;
        else if (currentPhase == 2)
            return waterBall;

        return fireBall;
    }
    public Transform GetFirePosition()
    {
        return firePosition;
    }
    public int GetCurrentPhase()
    {
        return currentPhase;
    }
}
