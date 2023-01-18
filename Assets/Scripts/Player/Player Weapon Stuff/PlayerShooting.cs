using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    PlayerWeaponManager weaponManager;
    PlayerStats playerStats;
    float timeToNextShot = 0f;

    [SerializeField] PlayerData playerData;

    bool isShooting;
    bool canShoot;
    bool isKnife;
    bool isFullAuto;

    //for raycast
    Camera playerCam;
    int ignoreDoorMask = 1 << 8; //bitwise left shift operation

    void Start()
    {
        weaponManager = GetComponent<PlayerWeaponManager>();
        playerStats = GetComponent<PlayerStats>();
        playerCam = Camera.main;
        ignoreDoorMask = ~ignoreDoorMask; //invert bitmask
    }

    // Update is called once per frame
    void Update()
    {
        timeToNextShot -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftControl) && PlayerWeapon.playerWeapons[weaponManager._currentWeapon]._shootingMode != ShootingMode.full_auto)
        {
            // Debug.Log("cooldown: " + timeToNextShot);
            if (timeToNextShot > 0)
                return;

            TryShoot(weaponManager._currentWeapon);
            Debug.Log("tryb strzelania aktualnej broni: " + PlayerWeapon.playerWeapons[weaponManager._currentWeapon]._shootingMode);
        }

        if (Input.GetKey(KeyCode.LeftControl) && PlayerWeapon.playerWeapons[weaponManager._currentWeapon]._shootingMode == ShootingMode.full_auto)
        {
            if (timeToNextShot > 0)
                return;
  
            TryShoot(weaponManager._currentWeapon);
            Debug.Log("Gracz chce strzeliæ z broni automatycznej");
            
        }
        // Animation after full auto shot.
        else if (Input.GetKeyUp(KeyCode.LeftControl) && PlayerWeapon.playerWeapons[weaponManager._currentWeapon]._shootingMode == ShootingMode.full_auto)
            weaponManager.PlayAfterFullAutoShootAnim();
    }

    //SHOOTING Method
    void TryShoot(WeaponType weaponInUse)
    {

        canShoot = playerData.playerAmmo.CurrentAmmo > 0;
        isKnife = PlayerWeapon.playerWeapons[weaponInUse]._shootingMode == ShootingMode.meele;
        isFullAuto = PlayerWeapon.playerWeapons[weaponInUse]._shootingMode == ShootingMode.full_auto;

         if(canShoot && isFullAuto) //allows calling shooting animation for full auto weapon when player can shoot again.
            weaponManager.CanFullAutoShootAnim(true);

        if (isKnife)
        {
            // isShooting = true;
            Fire(weaponInUse, false);
            Debug.Log("Player used knife. Ammo is the same: " + playerData.playerAmmo.CurrentAmmo);
          
        }

        if (canShoot && !isKnife)
        {
            Debug.Log("Player shot");
            Fire(weaponInUse);
            // isShooting = true;
            // playerStats.RemoveAmmo();
            // GameManager.instance.playerNoiseLevel = PlayerNoiseLevel.shooting;
        }
        else if (!canShoot && !isKnife && isFullAuto)
        {
            weaponManager.CanFullAutoShootAnim(false);
            Debug.Log("Player doesn't have enough ammo to shoot - he has full auto weapon");
        }
             //it prevents calling shooting animation for full auto weapon when player can't shoot.
        else if (!canShoot && !isKnife)
        {
            Debug.Log("Player doesn't have enough ammo to shoot");
            //weaponManager.PlayBeforeAfterShootAnims(false);
        }
        
        //weaponManager.StopFullAutoShootingAnim(false);

        //Decreasing time to next shot and playing animation after gunshot
        /*  if (isShooting)
          {

              weaponManager.PlayShootAnim(weaponManager.currentAnim);
              timeToNextShot = PlayerWeapon.playerWeapons[weaponInUse]._rof;
              //Raycast here
              CreateRay(PlayerWeapon.playerWeapons[weaponInUse]._range);
              ShootNoise(PlayerWeapon.playerWeapons[weaponInUse]._range);
              isShooting = false;
          }*/
        // weaponManager.SetBoolWeaponAnim(isShooting);
    }

    void ShootNoise(float noiseRange)
    {
        Collider[] enemyColliders;
        enemyColliders = Physics.OverlapSphere(transform.position, noiseRange / 1.5f, 1 << 7);

        foreach (Collider enemyCollider in enemyColliders)
        {
            //Debug.Log("Enemy: " + enemyCollider);
            enemyCollider.GetComponent<EnemyManager>().warned = true;
        }
    }

    void CreateRay(float range)
    {
        RaycastHit hit;

        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range, ignoreDoorMask))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                hit.transform.gameObject.GetComponentInParent<EnemyManager>().dmgenemy(PlayerWeapon.playerWeapons[weaponManager._currentWeapon]._damage);
                Debug.Log("trafiono: " + hit.transform.gameObject.name + ". Obra¿enia dla przeciwnika: " + PlayerWeapon.playerWeapons[weaponManager._currentWeapon]._damage);
            }

            Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward * range, Color.red, 0.25f);
        }
        else
        {
            Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward * range, Color.red, 0.25f);
        }
    }

    void Fire(WeaponType weaponInUse, bool DecreaseAmmo = true)
    {
        if(DecreaseAmmo)
            playerStats.RemoveAmmo();
        weaponManager.PlayShootAnim(weaponManager.currentAnim);
        timeToNextShot = PlayerWeapon.playerWeapons[weaponInUse]._rof;
     
        CreateRay(PlayerWeapon.playerWeapons[weaponInUse]._range);
        ShootNoise(PlayerWeapon.playerWeapons[weaponInUse]._range);
        //isShooting = false;
    }  
}
