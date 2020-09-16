using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawningPool : MonoBehaviour
{
    [SerializeField]
    int _monsterCount = 0;
    int _reserveCount = 0;
    [SerializeField]
    int _keepMonsterCount = 0;


    [SerializeField]
    Vector3 _SpawnPosition;
    [SerializeField]
    float _SpawnRadius = 15.0f;
    [SerializeField]
    float _spawnTime = 5.0f;

    public void AddMonsterCount(int value)
    {
        _monsterCount += value;
    }

    public void SetKeepMonsterCount(int count)
    {
        _keepMonsterCount = count;
    }

    void Start()
    {
        Managers.Game.OnSpawnEvent -= AddMonsterCount;
        Managers.Game.OnSpawnEvent += AddMonsterCount;
    }

    
    void Update()
    {
        while(_monsterCount + _reserveCount < _keepMonsterCount)
        {
            StartCoroutine("ReserveSpawn");
        }
    }

    IEnumerator ReserveSpawn()
    {
        ++_reserveCount;

        yield return new WaitForSeconds(UnityEngine.Random.Range(0, _spawnTime));

        GameObject Obj = Managers.Game.Spawn(Define.eWorldObject.Monster, "Knight");

        NavMeshAgent nma = Obj.GetComponent<NavMeshAgent>();

        Vector3 randPos;

        while(true)
        {
            Vector3 randDir = Random.insideUnitSphere * Random.Range(0, _SpawnRadius);
            randPos.y = 0;
            randPos = _SpawnPosition + randDir;

            // 갈 수 있나
            NavMeshPath path = new NavMeshPath();
            if(nma.CalculatePath(randPos, path))
            {
                break;
            }
        }

        Obj.transform.position = randPos;
        --_reserveCount;
    }
}
