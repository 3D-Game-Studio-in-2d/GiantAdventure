using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneSystem : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Image image;
    [SerializeField] private Image backgroundCutsceneImage;
    [SerializeField] private float spriteDisplayTime = 1f;
    [SerializeField] private float transitionTime = 0.3f;
    [SerializeField] private float alphaStop = 0.05f;
    
    void Start()
    {
        StartCoroutine(StartCutscene());
    }

    private IEnumerator StartCutscene()
    {
        float alphaStep = 1f / transitionTime;
        
        image.sprite = sprites[0];
        
        for (int i = 1; i < sprites.Length; i++)
        {
            yield return new WaitForSeconds(spriteDisplayTime);

            while (image.color.a >= alphaStop)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - alphaStep);
                yield return null;
            }
            
            image.sprite = sprites[i];
            
            while (image.color.a <= 1f)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + alphaStep);
                yield return null;
            }
        }
        
        yield return new WaitForSeconds(spriteDisplayTime);

        while (image.color.a >= alphaStop)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - alphaStep);
            yield return null;
        }
        
        image.gameObject.SetActive(false);
        backgroundCutsceneImage.gameObject.SetActive(false);
    }
}
