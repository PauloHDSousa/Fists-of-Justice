using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FemaleHistoryLine : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] TMPro.TMP_Text tmp_history;
    [SerializeField] TMPro.TMP_Text tmp_UI_space_button;
    [SerializeField] GameObject canvas;
    [SerializeField] string[] messages;

    [Header("Sounds")]
    [SerializeField] AudioClip femaleLaugh;
    [SerializeField] AudioClip soundTrack;
    [SerializeField] AudioClip teleport;
    [SerializeField] AudioClip nextMessage;

    [Header("Main Sounds")]
    [SerializeField] AudioSource mainSound;
    [SerializeField] AudioClip normalSoundTrack;

    [Header("Blink image")]
    [SerializeField] Image blinkImage;

    [Header("Female Ninja")]
    [SerializeField] GameObject femaleNinja;


    bool showedMessage = false;
    int currentMessage = 0;

    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && showedMessage)
            NextMessage();
    }

    void NextMessage()
    {
        currentMessage++;

        if (currentMessage == 6)
            audioSource.PlayOneShot(femaleLaugh);

        if (currentMessage == messages.Length)
        {
            mainSound.clip = normalSoundTrack;
            mainSound.Play();

            FadeStart();
            showedMessage = false;
            Destroy(gameObject, 8f);
        

        }
        else
        {
            tmp_history.text = messages[currentMessage];
            audioSource.PlayOneShot(nextMessage);
        }
    }


    void FadeStart()
    {
        LeanTween.alpha(blinkImage.rectTransform, 1f, 1f).setEase(LeanTweenType.easeInOutBack).setOnComplete(Hide);
    }

    void Hide()
    {
        canvas.SetActive(false);
        audioSource.PlayOneShot(teleport);
        Destroy(femaleNinja, 1f);
        LeanTween.alpha(blinkImage.rectTransform, 0f, 8f).setEase(LeanTweenType.easeInOutBack);
    }


    public void ShowUI()
    {
        tmp_history.text = messages[0];
        LeanTween.alpha(canvas.GetComponent<RectTransform>(), 1f, 3f).setEase(LeanTweenType.linear);
        LeanTween.value(tmp_history.gameObject, a => tmp_history.color = a, new Color(0, 0, 0, 0), new Color(1, 1, 1, 1), 3);
        LeanTween.value(tmp_UI_space_button.gameObject, a => tmp_UI_space_button.color = a, new Color(0, 0, 0, 0), new Color(1, 1, 1, 1), 3);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowUI();
            mainSound.clip = soundTrack;
            mainSound.Play();
            showedMessage = true;
        }
    }
}
