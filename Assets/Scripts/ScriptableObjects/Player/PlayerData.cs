using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Player_Data")]
public class PlayerData : ScriptableObject
{
 //   public WeaponType[] _weaponsInInventory = new WeaponType[4];

    //NEW
    public PlayerWeapon[] WeaponsInInventory = new PlayerWeapon[4];

    public PlayerAmmo playerAmmo = new PlayerAmmo();
    public ObjectHealth playerHealth = new ObjectHealth(100, 100);
    public PlayerLifes playerLifes = new PlayerLifes(3);
    public const int DEFAULT_LIFES_NUM = 3;
    public const int DEFAULT_AMMO_NUM = 8;

    public void TakeLife()
    {
        playerLifes.DecreasePlayerLifes(1);
    }
    public void GiveMaxHealth()
    {
        playerHealth.HealingValue(playerHealth.MaxHealth);
    }

    public void ResetAmmo()
    {
        playerAmmo.CurrentAmmo = DEFAULT_AMMO_NUM;
    }

    public void ResetLifes()
    {
        playerLifes.CurrentLifes = DEFAULT_LIFES_NUM;
    }
}