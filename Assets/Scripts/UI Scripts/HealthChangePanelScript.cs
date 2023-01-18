using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthChangePanelScript : MonoBehaviour
{
    [SerializeField]
    private Color healthdecreasecolor;
    [SerializeField]
    private Color healthincreasecolor;
    private Color zeroHealthColor = new Color32(200, 0, 0, 0);
    private Color victoryAlphaColor = new Color32(255, 255, 255, 0);
    private Color nonecolor = new Color32(0, 0, 0, 0);

    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] Sprite victorySprite;

    public static bool healthincrease = false;
    public static bool healthdecrease = false;
    public static bool zeroHealth = false;
    public static bool gameOverEffect = false;
    public static bool victory = false;
    float time = 0;

    void Start()
    {
        gameObject.GetComponent<Image>().color = nonecolor;
        gameOverText.alpha = zeroHealthColor.a;
        this.gameObject.GetComponent<Image>().sprite = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(time >0)
        {
            time -= Time.deltaTime;
        }
        if(!healthdecrease && !healthincrease && time<=0)
        {
            gameObject.GetComponent<Image>().color = nonecolor;
        }
        else if(healthincrease)
        {
            gameObject.GetComponent<Image>().color = healthincreasecolor;
            healthincrease = false;
            time = 0.1f;
        }
        else if(healthdecrease)
        {
            gameObject.GetComponent<Image>().color = healthdecreasecolor;
            healthdecrease = false;
            time = 0.1f;
        } 
        
        if(zeroHealth)
        {
            gameObject.GetComponent<Image>().color = zeroHealthColor;
            if(zeroHealthColor.a < 255)
                zeroHealthColor.a += Time.deltaTime;

        }
        
        if (gameOverEffect && zeroHealth)
        {
            gameObject.GetComponent<Image>().color = zeroHealthColor;
            if (zeroHealthColor.a < 255) {
                gameOverText.alpha = zeroHealthColor.a;
                zeroHealthColor.a += Time.deltaTime;
            } 
        }

        if(victory)
        {
            this.gameObject.GetComponent<Image>().sprite = victorySprite;
            gameObject.GetComponent<Image>().color = victoryAlphaColor;
            if (victoryAlphaColor.a < 255)
            {
                victoryAlphaColor.a += Time.deltaTime;
            }
            Debug.Log("victory");
        }
    }
}
