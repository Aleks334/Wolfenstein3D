using UnityEngine;

[CreateAssetMenu(menuName = "Player/Stats/Lifes_Stats")]
public class PlayerLifeSO : ScriptableObject
{
    [SerializeField] private int _defaultAmount;
    public int CurrentLifes { get; private set; }

    public void OnEnable()
    {
        CurrentLifes = _defaultAmount;
    }

    public void IncreasePlayerLifes(int lifesToIncrease)
    {
        CurrentLifes += lifesToIncrease;
    }

    public void DecreasePlayerLifes(int lifesToDecrease)
    {
        if (CurrentLifes > 0)
            CurrentLifes -= lifesToDecrease;
            
        if (CurrentLifes < 0)
        {
            CurrentLifes = 0;
        }       
    }

    public void GiveDefaultLifes()
    {
        CurrentLifes = _defaultAmount;
    }
}