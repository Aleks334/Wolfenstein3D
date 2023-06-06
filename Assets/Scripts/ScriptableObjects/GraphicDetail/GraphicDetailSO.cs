using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Graphic Detail/Database")]
public class GraphicDetailSO : ScriptableObject
{
    [SerializeField] private List<ResolutionSO> _availableResolutions;

    public List<ResolutionSO> AvailableResolutions
    {
        get => _availableResolutions;
    }

    private ResolutionSO _currentResolution;
    public ResolutionSO CurrentResolution
    {
        get
        {
            if(_currentResolution == null)
            {
                Debug.LogError("_currentResolution is null");
            }     

            return _currentResolution;
        }
        set => _currentResolution = value;
    }

    public void AssignResolution()
    {
        foreach (var resolution in AvailableResolutions)
        {
            if (Screen.currentResolution.width == resolution.Width &&
               Screen.currentResolution.height == resolution.Height)
            {
                CurrentResolution = resolution;
               // Debug.Log(CurrentResolution);

                return;
            }
        }

        CurrentResolution = AvailableResolutions[AvailableResolutions.Count - 1];
        Screen.SetResolution(CurrentResolution.Width, CurrentResolution.Height, false);
        //Debug.Log(CurrentResolution);
    }
}