using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Event Channels")]
    [SerializeField] private VoidEventChannelSO _onPlayerDeath;
    [SerializeField] private VoidEventChannelSO _onGameOver;
    // Start is called before the first frame update
    void Start()
    {
        _onPlayerDeath.OnEventRaised += stop_enemies;
        _onGameOver.OnEventRaised += stop_enemies;
    }

    public void stop_enemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log(enemies.Length);
        foreach(GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().Stop_enemy();
        }
        GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");
        foreach (GameObject boss in bosses)
        {
            boss.GetComponent<boss>().Stop_enemy();
        }
    }
    public void recover_enemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().Recover_enemy();
        }
        GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");
        foreach (GameObject boss in bosses)
        {
            boss.GetComponent<boss>().Recover_enemy();
        }
    }
}
