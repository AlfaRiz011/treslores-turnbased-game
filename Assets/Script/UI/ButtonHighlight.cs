using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHighlight : MonoBehaviour, IPointerEnterHandler
{
    public AudioSource Highlight;
    public AudioClip highlightSound;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Play the highlight sound when mouse enters the button
        Highlight.PlayOneShot(highlightSound);
    }
}