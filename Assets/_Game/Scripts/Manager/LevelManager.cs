using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] LevelBase[] levels;
    LevelBase currentLevel;
    List<Enemy> enemiesActive = new List<Enemy>();

    public int numberOfEnemy;
    public int numberOfEnemyOnScreen;
    public float minDistance = 5f;
    public float maxDistance = 10f;
    public float distanceWithinEnemies;
    int countEnemyTotal;
    int remainingEnemy;
    public Player player;
    Vector3 ramdomPos;
    public float distanceEnemyMove;



    public void Start()
    {
        OnLoadLevel(0);
        OnInit();
    }

    //khoi tao trang thai bat dau game
    public void OnInit()
    {
        countEnemyTotal = 0;
        remainingEnemy = numberOfEnemy;
        SwarmEnemies();   
        //player.OnInit();
    }

    //reset trang thai khi ket thuc game
    public void OnReset()
    {
        //player.OnDespawn();
        //for (int i = 0; i < bots.Count; i++)
        //{
        //    bots[i].OnDespawn();
        //}

        //bots.Clear();
        //SimplePool.CollectAll();
    }

    //tao prefab level moi
    public void OnLoadLevel(int level)
    {
        //if (currentLevel != null)
        //{
        //    Destroy(currentLevel.gameObject);
        //}

        //currentLevel = Instantiate(levels[level]);
    }

    Vector3 GetRandomPosition()
    {
        Vector2 point = Random.insideUnitCircle.normalized * Random.Range(minDistance, maxDistance);
        return new Vector3(point.x, 0, point.y);
    }

    public Vector3 GetPosEnemyMove(Vector3 posEnemy)
    {
        Vector3 randomDirection;
        Vector3 targetPosition;

        do
        {
            randomDirection = Random.insideUnitSphere.normalized * distanceEnemyMove;
            randomDirection.y = 0;

            targetPosition = posEnemy + randomDirection;
        } while (!NavMeshChecker.ins.IsPointOnNavMesh(targetPosition));

        return targetPosition;
    }    

    public void SwarmEnemies()
    {
        for (int i = 0; i < numberOfEnemyOnScreen; i++)
        {
            SwarmEnemy();
        }
    }

    public void SwarmEnemy()
    {
        if(countEnemyTotal >= numberOfEnemy) return;
        bool isSwarm = false;
        int attempts = 0;

        while (!isSwarm && attempts < 1000)
        {
            attempts++;
            Vector3 randomPos = GetRandomPosition() + player.transform.position;
            isSwarm = true;

            foreach (var enemy in enemiesActive)
            {
                if (Vector3.Distance(randomPos, enemy.transform.position) < distanceWithinEnemies)
                {
                    isSwarm = false;
                    break;
                }
            }

            if (isSwarm)
            {
                Enemy E = PoolingEnemy.ins.SpawnFromPool(Constain.TAG_ENEMY);
                E.gameObject.transform.localPosition = randomPos;
                E.OnInit();
                enemiesActive.Add(E);
                countEnemyTotal++;
            }
        }
    }

    public int RemainingEnemy
    {
        get { return remainingEnemy; }
        set { remainingEnemy = value; }
    }
}
