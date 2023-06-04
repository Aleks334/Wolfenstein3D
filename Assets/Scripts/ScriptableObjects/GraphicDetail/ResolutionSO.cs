using UnityEngine;

[CreateAssetMenu(menuName = "Graphic Detail/Screen Resolution")]
public class ResolutionSO : ScriptableObject
{
    [SerializeField] private int _width;
    public int Width
    {
        get => _width;
    }

    [SerializeField] private int _height;
    public int Height
    {
        get => _height;
    }
}