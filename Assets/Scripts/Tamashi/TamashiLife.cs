using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TamashiLife : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] float life = 2;

    [Header("UI")]
    [SerializeField] Image lifeBar;
    [SerializeField] TMPro.TMP_Text tmp_defeated_boss;
    [SerializeField] GameObject enemyCanvas;

    [Header("SFX")]
    [SerializeField] AudioClip deathSound;

    [Header("Blink image")]
    [SerializeField] Image blinkImage;

    [Header("Final message")]
    [SerializeField] TMPro.TMP_Text tmp_history;
    [SerializeField] TMPro.TMP_Text tmp_UI_space_button;
    [SerializeField] GameObject canvas;

    float currentLife;
    AudioSource audioSource;
    TamashiAnimator tamashiAnim;

    bool isDead = false;

    public bool isTamashiDead()
    {
        return isDead;
    }

    private void Start()
    {
        tamashiAnim = GetComponent<TamashiAnimator>();
        audioSource = GetComponent<AudioSource>();
        currentLife = life;
    }

    public void ReceiveHit()
    {
        currentLife -= 1;
        lifeBar.fillAmount = currentLife / life;


        if (currentLife == 0)
        {
            isDead = true;
            tamashiAnim.Die();
            audioSource.PlayOneShot(deathSound);
            LeanTween.value(tmp_defeated_boss.gameObject, a => tmp_defeated_boss.color = a, new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), 10);
            
            enemyCanvas.SetActive(false);

            tmp_history.text = "That's it! you're not going to destroy villages anymore!";
            canvas.SetActive(true);
            LeanTween.alpha(canvas.GetComponent<RectTransform>(), 1f, 3f).setEase(LeanTweenType.linear);
            LeanTween.value(tmp_history.gameObject, a => tmp_history.color = a, new Color(0, 0, 0, 0), new Color(1, 1, 1, 1), 1);
            LeanTween.value(tmp_UI_space_button.gameObject, a => tmp_UI_space_button.color = a, new Color(0, 0, 0, 0), new Color(1, 1, 1, 1), 1);

            Invoke("Blink", 7f);
            Invoke("LoadMainMenu", 12f);
        }
        else
            tamashiAnim.ReceiveHit();
    }

    public void Blink()
    {
        LeanTween.alpha(blinkImage.rectTransform, 1f, 4f).setEase(LeanTweenType.easeSpring);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}