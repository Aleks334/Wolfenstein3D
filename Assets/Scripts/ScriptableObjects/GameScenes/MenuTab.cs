using UnityEngine;

[CreateAssetMenu(fileName = "NewMenuTab", menuName = "Game_Data/MenuTab")]
public class MenuTab : GameSceneData
{
    [Header("Menu specific")]

    [SerializeField] private MenuTab _previousMenuTab;
    public MenuTab PreviousMenuTab => 
        _previousMenuTab;
}