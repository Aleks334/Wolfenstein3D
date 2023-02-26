using System.Collections;
using UnityEngine;

public class SplashScreensManager : MonoBehaviour
{
    [SerializeField] GameObject[] creatorsPanel = new GameObject[3];
    [SerializeField] ScenesData database;
    [SerializeField] int currentPanel;

    void Start()
    {
        database.UpdateMenuPage(MenuPage.SplashScreen);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        for(int i = 0; i < creatorsPanel.Length; i++)
        {
            if(i != 0)
                creatorsPanel[i].gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            StartCoroutine(Fade());
        }
    }

    IEnumerator Fade()
    {
        CanvasGroup canvas = creatorsPanel[currentPanel].transform.parent.GetComponent<CanvasGroup>();
        while (canvas.alpha > 0)
        {
            canvas.alpha -= Time.deltaTime;
            yield return null;
        }

        if (currentPanel < creatorsPanel.Length - 1)
        {
            
            creatorsPanel[currentPanel + 1].gameObject.SetActive(true);
            creatorsPanel[currentPanel].transform.parent.gameObject.SetActive(false);
            currentPanel++;
        }
        else
        {
            database.UpdateMenuPage(MenuPage.Options);
        }

        yield return null;
    }
}