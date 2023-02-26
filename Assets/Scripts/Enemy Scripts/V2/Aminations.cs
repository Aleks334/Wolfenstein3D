using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aminations : MonoBehaviour
{
    public List<Sprite> movesprites;
    public List<Sprite> attacksprites;
    public List<Sprite> deathsprites;
    public bool mele;
    int deathcount;
    float spritetime =0.0f;
    int iteration = 1;
    int attaccount;

    // Start is called before the first frame update
    void Start()
    {
        deathcount = 0;
         attaccount = 0;
        Debug.Log(attaccount);
    }

    // Update is called once per frame
    void Update()
    {
        if(!mele)anim();
        else meleanim();
    }
    public void anim()
    {
        string state = this.gameObject.GetComponentInParent<Enemy>().state;
        if (spritetime <= 0 || state == "Attacking")
        {
            
            int index = getindex();
            if(state== "Standing") { this.GetComponent<SpriteRenderer>().sprite = movesprites[index * 5];addtime(0.5f); }
            if(state== "Walking") { this.GetComponent<SpriteRenderer>().sprite = movesprites[index * 5 + iteration];addtime(0.5f); }
            if(state== "Running") { this.GetComponent<SpriteRenderer>().sprite = movesprites[index * 5 + iteration];addtime(0.25f); }
            if(state== "Aiming") { this.GetComponent<SpriteRenderer>().sprite = attacksprites[0];addtime(0.2f); }
            if(state== "Attacking") { this.GetComponent<SpriteRenderer>().sprite = attacksprites[1];addtime(0.15f); }
            if(state== "Stunned") { this.GetComponent<SpriteRenderer>().sprite = deathsprites[0];addtime(0.2f); }
            if(state== "Dead") { this.GetComponent<SpriteRenderer>().sprite = deathsprites[deathcount];addtime(0.2f); deathcount++; }
            iteration++;
            if (iteration > 4) iteration = 1;
            if (deathcount == deathsprites.Count) deathcount--;

            
        }
        else
        {
            spritetime -= Time.deltaTime;
        }

    }
    public void meleanim()
    {
        
        if (spritetime <= 0)
        {
            
            string state = this.gameObject.GetComponentInParent<Enemy>().state;
            if (state == "Attacking" || (attaccount < attacksprites.Count - 1 && attaccount >= 0)) { this.GetComponent<SpriteRenderer>().sprite = attacksprites[attaccount]; addtime(0.334f); attaccount++; }
            else
            {
                int index = getindex();
                if (state == "Walking") { this.GetComponent<SpriteRenderer>().sprite = movesprites[index * 4 + iteration]; addtime(0.2f); }
                if (state == "Running") { this.GetComponent<SpriteRenderer>().sprite = movesprites[index * 4 + iteration]; addtime(0.1f); }
                if (state == "Stunned") { this.GetComponent<SpriteRenderer>().sprite = deathsprites[0]; addtime(0.2f); }
                if (state == "Dead") { this.GetComponent<SpriteRenderer>().sprite = deathsprites[deathcount]; addtime(0.2f); deathcount++; }
            }
            iteration++;
            if (iteration > 3) iteration = 0;
            if (attaccount == attacksprites.Count) attaccount = 0;
            if (deathcount == deathsprites.Count) deathcount--;


        }
        else
        {
            spritetime -= Time.deltaTime;
        }

    }
    float getangle()
    {
        Vector3 targetpos = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, this.gameObject.GetComponentInParent<enemystats>().transform.position.y, GameObject.FindGameObjectWithTag("Player").transform.position.z);
        Vector3 targetdir = targetpos - this.gameObject.GetComponentInParent<enemystats>().transform.position;
        float angle;
        angle = Vector3.SignedAngle(targetdir, this.gameObject.GetComponentInParent<enemystats>().transform.forward, Vector3.up);
        return angle;
    }
    //funkcja wybieraj¹ca na podstawie k¹ta miêdzy graczem a przeciwnikem odpowiedni¹ serie klatek do animacji
    int getindex()
    {
        float angle;
        angle = getangle();
        int index = 0;
        if (angle <= 22.5 && angle >= -22.5) index = 0;
        if (angle < -22.5 && angle >= -67.5) index = 1;
        if (angle < -67.5 && angle >= -112.5) index = 2;
        if (angle < -112.5 && angle > -157.5) index = 3;
        if (angle <= -157.5 && angle >= -180) index = 4;
        if (angle <= 180 && angle >= 157.5) index = 4;
        if (angle < 157.5 && angle >= 112.5) index = 5;
        if (angle < 112.5 && angle >= 67.5) index = 6;
        if (angle < 67.5 && angle > 22.5) index = 7;

        return index;
    }
    void addtime(float time)
    {
        spritetime = time;
    }
}
