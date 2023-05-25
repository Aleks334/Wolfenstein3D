using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Faceanim : MonoBehaviour
{
    //To skrypt animuj¹cy ruch twarzy  UI
    System.Random rand = new System.Random();
    float time = 2;
    int image;
    bool anim;
    bool lookforward = false;
    public Sprite[] spritesarray = new Sprite[22];

    [SerializeField] PlayerHealthSO _health;
    void Update()
    {

        if (time <= 0 || UI.anim)
        {
            animface();
            this.GetComponent<Image>().sprite = spritesarray[image];
           
        }
        time -= Time.deltaTime;
    }
    public void animface()
    {

        image = 0;
        anim = true;

        time = rand.Next(1, 3);
        time -= (float)0.5;

        int currenthp = _health.playerHealth.CurrentHealth;
        if (currenthp > 90)
        {
            image =0;
        }
        else if (currenthp > 75)
        {
            image =1;
        }
        else if (currenthp > 60)
        {
            image =2;
        }
        else if (currenthp > 45)
        {
            image =3;
        }
        else if (currenthp > 30)
        {
            image =4;
        }
        else if (currenthp > 15)
        {
            image =5;
        }
        else if (currenthp > 0)
        {
            image =6;
        }
        else
        {
            image = 21;
            anim = false;
        }
        if(anim)
        {
            if(lookforward)
            {
                lookforward = false;
                image = image * 3 + 1;
            }
            else
            {
                lookforward = true;
                int look = rand.Next(1, 3);
                if(look == 1)
                {
                    image = image * 3;
                }
                else
                {
                    image = image * 3 + 2;
                }
            }
        }
    }
}
