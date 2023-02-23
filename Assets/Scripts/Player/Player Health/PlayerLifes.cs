public class PlayerLifes
{
    //Fields
    private int _currentLifes;

    //Properties
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

    //Constructor
    public PlayerLifes(int currentLifes)
    {
        CurrentLifes = currentLifes;
    }

    //Methods
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
