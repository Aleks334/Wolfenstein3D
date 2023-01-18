using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

//klasa w której znajduje siê AI przeciwnika, jego ¿ycie oraz funkcja strzelania i zadawania obra¿eñ graczowi
public class EnemyManager : MonoBehaviour
{
    public ObjectHealth Enemyhealth = new ObjectHealth(25, 25);
    //zmienne bool 
    //zmienna czy przeciwnik wie o graczu je¿eli wie to bêdzie kierowaæ siê w jego stronê
    public bool warned = false;
    public bool canshoot = false;
    public bool shoot = false;
    public bool dead = false;
    //sposób chodzenia przeciwnika
    public bool walk = false;
    public bool fastwalk = false;
    public bool run = false;
    public bool stand = true;
    public bool battle = false;
    public int noicevalue = 0;
    public bool haveplacetogo = false;
    public bool onrightplace = false;
    // Start is called before the first frame update
    float timeshoot = 0;
    public float standtime = 0f;
    float noisereactvalue = 0;
    public bool hitEnemy = false;
    public enemyattacktype type;
    [SerializeField] LayerMask allLayers;
    GameObject enemyAmmoClip;


    public Transform[] patrolplaces = new Transform[40];
    GameObject p;
    void Start()
    {
        p = GameObject.FindGameObjectWithTag("Player");
        walk = true;
        System.Random rand = new System.Random();
        noisereactvalue = rand.Next(5, 8);
        
        //this.GetComponent<Enemynevmesh>().destination = patrolplaces[rand.Next(0, patrolplaces.Length)];
        haveplacetogo = true;
        enemyAmmoClip = Resources.Load("EnemyAmmo/Ammo_Clip_Enemy") as GameObject;
        type = enemyattacktype.range;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            
            Shoot();
            if (canshoot && !shoot)
            {
                //battleAI(transform);
                battle = true;
           
                battleAI();
                move();
            }
            else
            {
                battle = false;
                stand = false;
                AI();
                move();

            }
            if (Enemyhealth.CurrentHealth <= 0)
            {
                this.GetComponent<NavMeshAgent>().isStopped = true;
                dead = true;
            }
        }

    }
    //funkcja w której przeciwnik je¿eli wykryje gracza przed sob¹ do strzela do niego
    void Shoot()
    {
            if(this.GetComponentInParent<Sight>().see)
            {
            battle = true;
                canshoot = true;
                if (timeshoot <= 0)
                {
                   // Debug.Log("TRAFIONO GRACZA");
                    timeshoot = 1f;
                    int dmg = (int)Random.Range(2f, 9f);
                    int chance = (int)Random.Range(0, 10);
                    if(chance <8)p.GetComponent<PlayerStats>().DamagePlayer(dmg);
                    shoot = true;
                    
                }
                else
                {
                    timeshoot -= Time.deltaTime;
                    shoot = false;
                    stand = true;
                }

            }
            else
            {
                canshoot = false;
            battle = false;
                timeshoot -= Time.deltaTime;
            }
    } 
            
    
    //Publiczna funkcja dziêki której mo¿na zmniejszyæ iloœæ ¿ycia przeciwnika
    public void dmgenemy(int dmg)
    {
        if (!dead)
        {
            warned = true;
            Enemyhealth.DmgValue(dmg);
            hitEnemy = true;
        }
    }


    //funkcja w której ustalana jest prêdkoœæ z jak¹ porusza siê gracz (nieu¿ywana)
    void move()
    {
        if (battle)
        {
            run = false;
            walk = false;
            fastwalk = false;
            stand = true;
        }
        else if (warned || run)
        {
            run = true;
            walk = false;
            fastwalk = false;
            stand = false;

        }
        else if (fastwalk)
        {
            run = false;
            walk = false;
            fastwalk = true;
            stand = false;

        }
        else if (walk)
        {
            run = false;
            walk = true;
            stand = false;
            fastwalk = false;
        }
        else if (stand)
        {
            stand = true;
            run = false;
            walk = true;
            fastwalk = false;
        }
        if (run && this.GetComponent<NavMeshAgent>().speed != 6f)
        {
            this.GetComponent<NavMeshAgent>().speed = 6f;
        }
        if (walk && this.GetComponent<NavMeshAgent>().speed != 2f)
        {
            this.GetComponent<NavMeshAgent>().speed = 2f;
        }
        if (fastwalk && this.GetComponent<NavMeshAgent>().speed != 4f)
        {
            this.GetComponent<NavMeshAgent>().speed = 4f;
        }
        if (stand && this.GetComponent<NavMeshAgent>().speed != 0f)
        {
            this.GetComponent<NavMeshAgent>().speed = 0f;
        }
        if (battle) this.GetComponent<NavMeshAgent>().speed = 0f;
    }
    //AI przeciwnika w funkcji battle AI przeciwnik obraca siê w stronê gracza aby móg³ strzeliæ
    void battleAI()
    {
        stand = true;
    }
    //w tej funkcji ustalany jest kierunek w jakim pod¹¿a przeciwnik
    void AI()
    {
        //je¿eli przeciwnik jest ostrze¿ony to idzie w stronê gracza
        if (warned && !battle)
        {
            this.GetComponent<Enemynevmesh>().destination = p.transform;
            run = true;
        }
        else
        {

            //je¿eli gracz jest zbyt g³oœny to przeciwnik zostaje ostrze¿ony
            noicevalue = (int)GameManager.instance.playerNoiseLevel;
            noicevalue *= noicevalue;
            double distance = (System.Math.Pow((p.transform.position.x - transform.position.x), 2)+ System.Math.Pow((p.transform.position.z - transform.position.z), 2));
            if(noicevalue / distance >noisereactvalue)
            {
                warned = true;
                
            }
            //je¿eli przeciwnik nie jest ostrze¿ony to chodzi miêdzy wyznaczonymipunktami
            if (onrightplace && haveplacetogo)
            {

                haveplacetogo = false;
                standtime = 3f;
                stand = true;
                walk = false;
                fastwalk = false;
                run = false;
            }
            if (onrightplace && !haveplacetogo)
            {

                standtime -= Time.deltaTime;
                stand = true;
                walk = false;
                fastwalk = false;
                run = false;
                if (standtime <= 0)
                {
                    System.Random rand = new System.Random();
                    Transform lastDest = this.GetComponent<Enemynevmesh>().destination;

                    AssignPatrolPoint:
                    this.GetComponent<Enemynevmesh>().destination = patrolplaces[rand.Next(0, patrolplaces.Length)];
                    if (this.GetComponent<Enemynevmesh>().destination == lastDest)
                    {
                        goto AssignPatrolPoint;
                    }

                    haveplacetogo = true;
                    onrightplace = false;
                    walk = true;
                    stand = false;
                }
            }
        }
    }
    //funkcja wywo³uj¹ca siê raz w monencie œmierci przeciwnika
    public void onenemydeadth()
    {
        int DropChance = Random.Range(0, 4);
        GetComponent<CapsuleCollider>().enabled = false;

        if(DropChance > 1)
            Instantiate(enemyAmmoClip, new Vector3(transform.position.x, 0f, transform.position.z), Quaternion.identity);
    }
    public enum enemyattacktype
    {
        mele,
        range
    }
}

