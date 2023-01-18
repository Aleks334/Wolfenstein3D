using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WaeponScript : MonoBehaviour
{

    public Sprite[] weaponspritesarray = new Sprite[4]; //tablica przechowuj¹ca sprity do broni w UI
    public TextMeshProUGUI ammocounttext;
    public Image weaponimg;
    public static bool reload = false;
    public static string ammoamount = "8";
    string kammoamount = "-";

    [SerializeField] PlayerData playerData;

    private void Start()
    {
        PlayerWeaponManager weaponmanager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerWeaponManager>();
        for (int i = 0; i < 4; i++)
        {
            if (weaponmanager._currentWeapon == playerData._weaponsInInventory[i])
            {

                weaponimg.sprite = weaponspritesarray[i];
                if (i == 0)
                {
                    ammocounttext.text = kammoamount;

                }
                else
                {
                    ammocounttext.text = ammoamount;
                }
                break;
            }
        }
        weaponimg.sprite = weaponspritesarray[3];
    }

    void Update()
    {
        if(reload)
        {
           
            Reload();
            reload = false;
        }
    }
    public void Reload()
    {

        PlayerWeaponManager weaponmanager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerWeaponManager>();
        for(int i=0;i<4;i++)
        {
            if(weaponmanager._currentWeapon == playerData._weaponsInInventory[i])
            {
                
               weaponimg.sprite = weaponspritesarray[i];
                if(i==0)
                {
                    ammocounttext.text = kammoamount;
                    
                }
                else
                {
                    ammocounttext.text = ammoamount;
                }
                break;
            }
        }
        
    }
}
