using UnityEngine;

public class LoadSceneOnKeyPress : MonoBehaviour
{
    [SerializeField] private LoadSceneEventChannelSO _loadSceneEventChannel;
    [SerializeField] private GameSceneData _sceneToLoad;

    [Header("Player Stats to reset")]
    [SerializeField] private HealthSO _playerHealth;
    [SerializeField] private PlayerAmmoSO _playerAmmo;
    [SerializeField] private PlayerLifeSO _playerLifes;
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            _playerHealth.GiveDefaultHealth();
            _playerAmmo.ResetAmmo();
            _playerLifes.GiveDefaultLifes();

            _loadSceneEventChannel.RaiseEvent(new[] { _sceneToLoad }, false);
        }
            
    }
}