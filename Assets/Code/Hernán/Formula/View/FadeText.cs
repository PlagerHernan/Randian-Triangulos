using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeText : MonoBehaviour
{
    Text[] _childrenTexts;
    [SerializeField] [Range(0,1)] float _semiFaded;

    //llamado desde RollAndSheet_UnfoldRoll.anim
    IEnumerator ShowTextSequentially()
    {
        _childrenTexts = GetComponentsInChildren<Text>();

        for (int i = 0; i < _childrenTexts.Length; i++)
        {
            yield return new WaitForSeconds(0.15f);

            StartCoroutine(FadeIn(_childrenTexts[i])); 
        }
    }

    IEnumerator FadeIn(Text text)
    {
        float currentColorAlpha = 0f;

        while (currentColorAlpha < 1f) 
        {
            currentColorAlpha += 0.01f;
            text.color = new Color(text.color.r, text.color.g, text.color.b, currentColorAlpha); 
            yield return new WaitForSeconds(0.001f);
        }
    }

    public IEnumerator FadeOut(Text text, bool semiFaded)
    {
        float currentColorAlpha = 1f;
        float targetColorAlpha;

        if (semiFaded)
        {
            targetColorAlpha = _semiFaded;
        }
        else
        {
            targetColorAlpha = 0f;
        }
        

        while (currentColorAlpha > targetColorAlpha) 
        {
            currentColorAlpha -= 0.01f;
            text.color = new Color(text.color.r, text.color.g, text.color.b, currentColorAlpha); 
            yield return new WaitForSeconds(0.001f);
        }
    }
}
