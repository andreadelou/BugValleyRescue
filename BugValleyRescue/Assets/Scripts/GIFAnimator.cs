using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GifAnimator : MonoBehaviour
{
    public Sprite[] frames; // Array de sprites para los fotogramas del GIF
    public float frameRate = 0.1f; // Tiempo entre fotogramas (en segundos)

    private Image imageComponent;
    private int currentFrame;

    void Start()
    {
        imageComponent = GetComponent<Image>();
        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        while (true)
        {
            imageComponent.sprite = frames[currentFrame];
            currentFrame = (currentFrame + 1) % frames.Length;
            yield return new WaitForSeconds(frameRate);
        }
    }
}
