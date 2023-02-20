using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Player_Data")]
public class PlayerData : ScriptableObject
{
    public WeaponType[] _weaponsInInventory = new WeaponType[4];

    public PlayerAmmo playerAmmo = new PlayerAmmo();
    public ObjectHealth playerHealth = new ObjectHealth(100, 100);
    public PlayerLives playerLives = new PlayerLives(3);
    public const int DEFAULT_LIFES_NUM = 3;
    public const int DEFAULT_AMMO_NUM = 8;

    public void TakeLife()
    {
        playerLives.DecreasePlayerLives(1);
    }
    public void GiveMaxHealth()
    {
        playerHealth.HealingValue(playerHealth.MaxHealth);
    }

    public void ResetAmmo()
    {
        playerAmmo.CurrentAmmo = DEFAULT_AMMO_NUM;
    }

    public void RestLifes()
    {
        playerLives.CurrentLives = DEFAULT_LIFES_NUM;
    }
}