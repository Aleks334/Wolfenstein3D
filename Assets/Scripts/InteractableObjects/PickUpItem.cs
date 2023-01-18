using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] PickUpItemType pickUpItemType;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();
            PlayerWeaponManager weaponManager = other.GetComponent<PlayerWeaponManager>();
            switch (pickUpItemType)
            {
                case PickUpItemType.powerUp:
                    if (stats.CanPickUpItem(false, false, true))
                    {
                        stats.HealPlayer((int)PowerUpData.healthVal);
                        stats.AddLives((int)PowerUpData.livesVal);
                        stats.AddAmmo((int)PowerUpData.ammoVal);
                        Destroy(gameObject);
                        UI.healtincreaseeffect();
                    }
                    break;

                case PickUpItemType.dogFood:
                    if (stats.CanPickUpItem(false, true))
                    {
                        stats.HealPlayer((int)pickUpItemType);
                        Destroy(gameObject);
                        UI.healtincreaseeffect();
                    }
                    break;

                case PickUpItemType.food:
                    if (stats.CanPickUpItem(false, true))
                    {
                        stats.HealPlayer((int)pickUpItemType);
                        Destroy(gameObject);
                        UI.healtincreaseeffect();
                    }
                    break;

                case PickUpItemType.healthPack:
                    if (stats.CanPickUpItem(false, true))
                    {
                        stats.HealPlayer((int)pickUpItemType);
                        Destroy(gameObject);
                        UI.healtincreaseeffect();
                    }
                    break;

                case PickUpItemType.ammoClip:
                    if (stats.CanPickUpItem(true, false))
                    {
                        stats.AddAmmo((int)pickUpItemType);
                        Destroy(gameObject);
                        UI.healtincreaseeffect();
                    }
                    break;

                case PickUpItemType.enemyAmmoClip:
                    if (stats.CanPickUpItem(true, false))
                    {
                        stats.AddAmmo((int)pickUpItemType);
                        Destroy(gameObject);
                        UI.healtincreaseeffect();
                    }
                    break;
                case PickUpItemType.machine_gun:
                    if (!weaponManager.HaveThatWeapon(WeaponType.machine_gun))
                    {
                        Debug.Log("test");
                        weaponManager.GiveWeapon(WeaponType.machine_gun);
                        weaponManager.ChangeWeapon(WeaponType.machine_gun);
                        Destroy(gameObject);
                        UI.healtincreaseeffect();
                    }
                    break;
                case PickUpItemType.mini_gun:
                    if (!weaponManager.HaveThatWeapon(WeaponType.mini_gun))
                    {
                        weaponManager.GiveWeapon(WeaponType.mini_gun);
                        weaponManager.ChangeWeapon(WeaponType.mini_gun);
                        Destroy(gameObject);
                        UI.healtincreaseeffect();
                    }
                    break;

                default:
                    Debug.LogWarning("Zebrany element jest innego typu lub nie ma go przydzielonego");
                    break;
            }
        }
    }

    enum PickUpItemType
    {
        powerUp = 0,

        dogFood = 5, 
        food = 10,
        healthPack = 25,

        ammoClip = 8,
        enemyAmmoClip = 4,

        machine_gun = 98,
        mini_gun = 99,
    }

    enum PowerUpData
    {
        healthVal = 99,
        ammoVal = 25,
        livesVal = 1,
    }
}