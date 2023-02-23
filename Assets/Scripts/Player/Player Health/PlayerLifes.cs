public class PlayerLifes
{
    private int _currentLifes;

    public int CurrentLifes
    { 
        get
        { 
            return _currentLifes; 
        } 

        set
        {
            _currentLifes = value;
        }
    }

    public PlayerLifes(int lifesCount)
    {
        CurrentLifes = lifesCount;
    }

    public void IncreasePlayerLifes(int lifesToIncrease)
    {
        CurrentLifes += lifesToIncrease;
    }

    public void DecreasePlayerLifes(int lifesToDecrease)
    {
        if(CurrentLifes > 0)
            CurrentLifes -= lifesToDecrease;
        if (CurrentLifes < 0)
            CurrentLifes = 0;
    }
}
