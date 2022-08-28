using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryLine : MonoBehaviour
{

    [Header("Configs")]
    [SerializeField] TMPro.TMP_Text tmp_history;
    [SerializeField] TMPro.TMP_Text tmp_UI_space_button;
    [SerializeField] GameObject canvas;
    [SerializeField] string message;

    [Header("Messages")]
    [SerializeField] GameObject first_enemy_life;

    bool showedMessage = false;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && showedMessage)
            HideUI();
    }

    void HideUI()
    {
        LeanTween.alpha(canvas.GetComponent<RectTransform>(), 0f, 3f).setEase(LeanTweenType.linear);
        LeanTween.value(tmp_history.gameObject, a => tmp_history.color = a, new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), 3);
        LeanTween.value(tmp_UI_space_button.gameObject, a => tmp_UI_space_button.color = a, new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), 3);
        if (showedMessage)
            Destroy(gameObject);
    }

    public void ShowUI(string text)
    {
        tmp_history.text = text;
        LeanTween.alpha(canvas.GetComponent<RectTransform>(), 1f, 3f).setEase(LeanTweenType.linear);
        LeanTween.value(tmp_history.gameObject, a => tmp_history.color = a, new Color(0, 0, 0, 0), new Color(1, 1, 1, 1), 3);
        LeanTween.value(tmp_UI_space_button.gameObject, a => tmp_UI_space_button.color = a, new Color(0, 0, 0, 0), new Color(1, 1, 1, 1), 3);

        if(first_enemy_life != null)
            first_enemy_life.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowUI(message);
            showedMessage = true;
        }
    }
}
