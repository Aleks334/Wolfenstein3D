using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossanim : MonoBehaviour
{
    public List<Sprite> Mehwalk;
    public List<Sprite> Mehattack;
    public List<Sprite> Mehdead;
    public List<Sprite> bosswalk;
    public List<Sprite> bossattack;
    public List<Sprite> bossdead;
    public bool meh = true;
    public bool dead = false;
    public string state;
    public bool danim = false;
    float spritetime = 0.0f;
    int iteration = 1;


    // Start is called before the first frame update
    void Start()
    {
        this.GetComponentInParent<bossstats>().Mehdestroyed += OnMehdestroyed;
        this.GetComponentInParent<bossstats>().bossdie += OnBossDie;
        this.GetComponentInParent<bossattack>().shoot += attack;
        this.GetComponentInParent<bossattack>().run += run;
        this.GetComponentInParent<bossattack>().aim += aim;


    }

    // Update is called once per frame
    void Update()
    {

        if (spritetime > 0.0f)
        {
            spritetime -= Time.deltaTime;
        }
        else 
        {
            if (!danim)
            {
               if(!dead) anim();
            }
            else
            {
                deadanim();
            }
        }
    }
    public void anim()
    {
        switch(state)
        {
            case "aiming":
                if(meh)
                {

                    this.GetComponent<SpriteRenderer>().sprite = Mehattack[0];
                    addtime(1f);
                }
                else
                {
                    this.GetComponent<SpriteRenderer>().sprite = bossattack[0];
                    addtime(1f);
                }
                break;
            case "attacking":
                if(meh)
                {
                    if (iteration >= Mehattack.Count)
                        iteration = 0;
                    this.GetComponent<SpriteRenderer>().sprite = Mehattack[iteration];
                    iteration++;
                    addtime(0.5f);
                }
                else
                {
                    if (iteration >= bossattack.Count)
                        iteration = 0;
                    this.GetComponent<SpriteRenderer>().sprite = bossattack[iteration];
                    iteration++;
                    addtime(0.5f);
                }

                break;
            case "running":
                if (meh)
                {
                    if (iteration >=Mehwalk.Count)
                        iteration = 0;
                    this.GetComponent<SpriteRenderer>().sprite = Mehwalk[iteration];
                    iteration++;
                    addtime(0.2f);
                }
                else
                {
                    if (iteration >= bosswalk.Count)
                        iteration = 0;
                    this.GetComponent<SpriteRenderer>().sprite = bosswalk[iteration];
                    iteration++;
                    addtime(0.2f);
                }
                break;
            case "bossstunned":
                this.GetComponent<SpriteRenderer>().sprite = bossdead[0];
                addtime(0.5f);
                break;
            case "standing":
                this.GetComponent<SpriteRenderer>().sprite = Mehwalk[0];
                addtime(0.5f);
                break;
        }
    }
    public void deadanim()
    {
        
        if(dead)
        {
            this.GetComponent<SpriteRenderer>().sprite = bossdead[iteration++];
            if (iteration >= bossdead.Count) danim = false;
            addtime(0.2f);
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = Mehdead[iteration++];
            if (iteration >= Mehdead.Count) danim = false;
            addtime(0.2f);
        }
    }
    void addtime(float time)
    {
        spritetime = time;
    }
    public void OnMehdestroyed()
    {
        iteration = 0;
        meh = false;
        danim = true;
    }
    public void OnBossDie()
    {
        iteration = 0;
        dead = true;
        danim = true;
    }
    public void attack()
    {
        state = "attacking";
        addtime(0);
    }
    public void aim()
    {
        state = "aiming";

    }
    public void run()
    {
        state = "running";
        
    }
}
