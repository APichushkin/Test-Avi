using UnityEngine;
using UnityEngine.EventSystems;

public class LoadSpritesButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Animator _buttonAnimator;
    public void OnPointerClick(PointerEventData eventData) 
    {
        _buttonAnimator.SetBool("anim", true);
    }
}
