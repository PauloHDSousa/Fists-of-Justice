using UnityEngine;

public class FightBehavior : MonoBehaviour
{
    [SerializeField] AudioClip airPunch;
    [SerializeField] AudioClip[] screams;
    [SerializeField] bool isTutorial = false;
    Animator anim;

    float nextFireTime = 0f;
    int numOfClicks = 0;
    float lastClickedTime = 0;
    float maxComboDelay = .5f;

    Transform enemyPosition;


    Rigidbody rb;
    AudioSource audioSource;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        enemyPosition = GameObject.FindGameObjectWithTag("Enemy").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

    }

    void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        if (!isTutorial)
        {
            anim.SetTrigger("WakeUp");
            anim.SetBool("Grounded", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ResetAnimationParameters();
        CheckPunch();

        if (Input.GetKeyDown(KeyCode.Q))
            LockOnEnemy();
        else if (Input.GetKeyDown(KeyCode.LeftControl))
            Dodge();
    }

    void CheckPunch()
    {
        if (Time.time > nextFireTime)
            if (Input.GetMouseButtonDown(0))
                OnPunch();
    }

    void OnPunch()
    {
        nextFireTime = Time.time + .2f;
        lastClickedTime = Time.time;

        if (numOfClicks == 0)
        {
            numOfClicks++;
            anim.SetBool("Hit1", true);
            return;
        }

        numOfClicks = Mathf.Clamp(numOfClicks, 0, 3);

        if (numOfClicks >= 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("HIT1"))
        {
            numOfClicks++;
            anim.SetBool("Hit1", false);
            anim.SetBool("Hit2", true);
        }
        else if (numOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).IsName("HIT2"))
        {
            numOfClicks = 3;
            anim.SetBool("Hit2", false);
            anim.SetBool("Hit3", true);
        }
    }

    void ResetAnimationParameters()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("HIT1"))
            anim.SetBool("Hit1", false);
        else if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("HIT2"))
            anim.SetBool("Hit2", false);
        else if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("HIT3"))
        {
            anim.SetBool("Hit3", false);
            numOfClicks = 0;
        }

        if (numOfClicks > 0 && Time.time - lastClickedTime > maxComboDelay)
        {
            numOfClicks = 0;
            anim.SetBool("Hit1", false);
            anim.SetBool("Hit2", false);
            anim.SetBool("Hit3", false);
        }
    }

    void LockOnEnemy()
    {
        //Look To Player
        transform.LookAt(enemyPosition);
    }

    void Dodge()
    {
        anim.SetTrigger("Dodge");

    }

    public void Explosion()
    {
        anim.SetTrigger("PushBack");
    }

    public int GetCurrentComboPosition()
    {
        return numOfClicks;
    }

    public void AirSoundOnDodge()
    {
        audioSource.PlayOneShot(airPunch);
    }
    public void ScreamOnPunch()
    {
        audioSource.PlayOneShot(GetRandomScream());
    }

    AudioClip GetRandomScream()
    {
        int r = Random.Range(0, screams.Length);
        return screams[r];
    }
}
