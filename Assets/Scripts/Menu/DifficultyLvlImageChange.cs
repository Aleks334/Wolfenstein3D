using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DifficultyLvlImageChange : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{
    [SerializeField] DifficultyLvlSelection difficultyLvlSelectionManager;

    public void OnPointerEnter(PointerEventData eventData)
    {
        difficultyLvlSelectionManager.difficultyLvlImage.sprite = difficultyLvlSelectionManager.DifficultyLvlSprites[transform.GetSiblingIndex()];
    }

    public void OnSelect(BaseEventData eventData)
    {
        difficultyLvlSelectionManager.difficultyLvlImage.sprite = difficultyLvlSelectionManager.DifficultyLvlSprites[transform.GetSiblingIndex()];
    }
}
