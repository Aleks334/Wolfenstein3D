using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public  TextMeshProUGUI healthtext;
    public  TextMeshProUGUI Lifecounttext;
    public static bool anim = false;
    static bool reload = false;

    [SerializeField] private PlayerHealthSO _health;
    [SerializeField] private PlayerLifeSO _lifes;

    [SerializeField] private VoidEventChannelSO _onPlayerDeath;
    [SerializeField] private VoidEventChannelSO _onGameOver;
    [SerializeField] private VoidEventChannelSO _onPlayerVictory;

    //subscribe to event OnGameStateChange in order to affect on game state change.
    void OnEnable()
    {
        _onPlayerDeath.OnEventRaised += OnPlayerDeath;
        _onGameOver.OnEventRaised += OnGameOver;
        _onPlayerVictory.OnEventRaised += OnPlayerVictory;
    }

    //unsubscribe to event OnGameStateChange when object is destroyed or after loading next scene.
    void OnDisable()
    {
        _onPlayerDeath.OnEventRaised -= OnPlayerDeath;
        _onGameOver.OnEventRaised -= OnGameOver;
        _onPlayerVictory.OnEventRaised -= OnPlayerVictory;
    }
    private void Start()
    {
        healthtext.text = _health.playerHealth.CurrentHealth.ToString() + "%";
        Lifecounttext.text = _lifes.CurrentLifes.ToString();
    }

    void OnPlayerDeath()
    {
       HealthChangePanelScript.zeroHealth = true;
       Debug.Log("Utrata 1 ¿ycia. Wyœwietlenie efektu panelu, który staje siê coraz bardziej czerwony");     
    }

    void OnGameOver()
    {
       HealthChangePanelScript.zeroHealth = true;
       HealthChangePanelScript.gameOverEffect = true;
       Debug.Log("Wyœwietlenie efektu panelu jak wczeœniej, tylko z napisem w stylu GameOver");    
    }
    void OnPlayerVictory()
    {
        HealthChangePanelScript.victory = true;
        Debug.Log("Wyœwietlenie panelu dot. wygranej gracza");
    }

    void Update()
    {
        anim = false;
        if(reload)
        {
            
            healthtext.text = _health.playerHealth.CurrentHealth.ToString() + "%";
            Lifecounttext.text = _lifes.CurrentLifes.ToString();
            anim = true;
            reload = false;
            WaeponScript.reload = true;
        }
    }

    public static void ReloadUI()
    {
        reload = true;

    }
    public static void ReloadUI(int ammocount)
    {
        reload = true;
        WaeponScript.ammoamount = ammocount.ToString();
    }
    public static void healtincreaseeffect()
    {
        HealthChangePanelScript.healthincrease = true;
    }
    public static void healthdecreaseeffect()
    {
        HealthChangePanelScript.healthdecrease = true;
    }
}