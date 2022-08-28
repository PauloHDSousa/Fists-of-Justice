using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class LoadScene : MonoBehaviour
{
    Image image;
    void Start()
    {
        image = GetComponent<Image>();
        FadeStart();
    }

    void FadeStart()
    {
        LeanTween.alpha(image.rectTransform, 0f, 5f).setEase(LeanTweenType.easeInOutBack);
    }
}
