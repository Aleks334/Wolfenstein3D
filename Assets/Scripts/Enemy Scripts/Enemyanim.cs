using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
// w tej klasie odbywa siê animowanie przeciwnika, jego chodzenia, strzelania i œmierci

public class Enemyanim : MonoBehaviour
{
    public Sprite[] movesprites = new Sprite[40];
    public Sprite[] shootsprite = new Sprite[2];
    public Sprite[] deadsprite = new Sprite[5];
    public Sprite hitEnemy;
    bool justdeadth = false;
    public Transform player;
    public Transform parent;
    int iteration = 1;
    float time = 0.3f;

    // Update is called once per frame
    private void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = movesprites[0];
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {
        if(!this.GetComponentInParent<EnemyManager>().dead)
        {
            if (this.GetComponentInParent<EnemyManager>().hitEnemy)
            {
                StartCoroutine(ShowHitEnemySprite()); 
            }
                
            anim();
           
        }
        else
        {
            if (!justdeadth)
            {
                this.GetComponentInParent<EnemyManager>().onenemydeadth();
                iteration = 0;
                justdeadth = true;
            }
            if(iteration <5)
            deadanim();
        }
        if(time>0) time -= Time.deltaTime;

    }
    //funkcjie ustalaj¹ce czas do zmiany klatki animacji
    void addtime()
    {
        time += 0.3f;
    }
    void addtime(float t)
    {
        time += t;
    }
    //funkcja zmieniaj¹ca klatki animacji przeciwnika
    public void anim()
    {
        if (this.GetComponentInParent<EnemyManager>().canshoot)
        {
            if (time <= 0 || this.GetComponentInParent<EnemyManager>().shoot)
            {
                if (this.GetComponentInParent<EnemyManager>().shoot)
                {
                    this.GetComponent<SpriteRenderer>().sprite = shootsprite[0];
                    addtime(0.15f);
                }
                else
                {
                    this.GetComponent<SpriteRenderer>().sprite = shootsprite[1];
                    addtime(0.15f);
                }
            }

        }
        if(this.GetComponentInParent<EnemyManager>().warned && time <0)
        {
            this.GetComponent<SpriteRenderer>().sprite = movesprites[iteration];
            addtime(0.15f);
            iteration++;
            if (iteration >= 5) iteration = 1;
        }

        if (time < 0)
        {
            if (this.GetComponentInParent<EnemyManager>().stand)
            {
                this.GetComponent<SpriteRenderer>().sprite = movesprites[getindex()*5];
                addtime();
            }
            else
            {
                this.GetComponent<SpriteRenderer>().sprite = movesprites[getindex() * 5+iteration];
                if(this.GetComponentInParent<EnemyManager>().walk)addtime(0.4f);
                if(this.GetComponentInParent<EnemyManager>().fastwalk)addtime(0.2f);
                if(this.GetComponentInParent<EnemyManager>().run)addtime(0.15f);
                iteration++;
                if (iteration == 5) iteration = 1;
            }

        }
        
    }

    //Funkcja wyœwietlaj¹ca sprite trafienia i wystrzymuj¹ca dzia³anie przeciwnika dopóki ten nie wróci do poprzedniego stanu
    public IEnumerator ShowHitEnemySprite()
    {
        GetComponent<SpriteRenderer>().sprite = hitEnemy;
        transform.parent.GetComponent<EnemyManager>().enabled = false;
        transform.parent.GetComponent<Enemynevmesh>().enabled = false;

        yield return new WaitForSeconds(0.2f);

        transform.parent.GetComponent<EnemyManager>().enabled = true;
        transform.parent.GetComponent<Enemynevmesh>().enabled = true;
        GetComponentInParent<EnemyManager>().hitEnemy = false;
    }
    //funkcja animuj¹ca œmieræ przeciwnika
    void deadanim()
    {
        if (time <= 0)
        {
            this.GetComponent<SpriteRenderer>().sprite = deadsprite[iteration];
            addtime(0.1f);
            iteration++;
        }
        else
        {
            time -= Time.deltaTime;
        }
    }
    //funkcja zwracaj¹ca k¹t pomiêdzy przeciwnikiem i graczem
    float getangle()
    {
        Vector3 targetpos = new Vector3(player.position.x, parent.position.y, player.position.z);
        Vector3 targetdir = targetpos - parent.position;
        float angle;
        angle = Vector3.SignedAngle(targetdir,parent.forward, Vector3.up);
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
    
}
