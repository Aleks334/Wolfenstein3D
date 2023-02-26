using UnityEngine;

public class LifesManager : MonoBehaviour, IPlayerProfile
{
    [SerializeField] private PlayerLifeSO _data;

    public PlayerLifeSO Data
    {
        get { return _data; }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            AddLifes(1);
        else if (Input.GetKeyDown(KeyCode.J))
            RemoveLifes(1);
    }
    
    public void AddLifes(int lifesToAdd)
    {
        Data.IncreasePlayerLifes(lifesToAdd);
        Debug.Log("Obecny poziom ¿ycia: " + Data.CurrentLifes);
        UI.ReloadUI();
    }
    public void RemoveLifes(int lifesToRemove)
    {
        Data.DecreasePlayerLifes(lifesToRemove);
        Debug.Log("Obecny poziom ¿ycia: " + Data.CurrentLifes);
        UI.ReloadUI();
    }
}
