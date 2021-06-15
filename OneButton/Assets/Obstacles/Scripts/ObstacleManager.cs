using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Got the idea of random spawn based on distance from https://answers.unity.com/questions/1693874/random-spawn-for-multible-objects.html
//Spawn area idea from https://www.youtube.com/watch?v=kTvBRkPTvRY

[RequireComponent(typeof(ObjectPooler))]
public class ObstacleManager : MonoBehaviour
{
    [SerializeField]
    private Vector3 spawn_area_center;

    [SerializeField]
    private Vector3 spawn_area_size;

    [SerializeField]
    private int max_obstacle_amount = 6;

    [SerializeField]
    private float min_distance_between_obstacles = 2f;

    Vector3 last_spawn_position = Vector3.zero;

    [SerializeField]
    private int current_obstacle_amount = 0;

    ObjectPooler obstacle_pool;

    void Awake()
    {
        obstacle_pool = GetComponent<ObjectPooler>();
        obstacle_pool.amount = max_obstacle_amount;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Invoke("SpawnObstacle", 4f);
    }

    void LateUpdate()
    {
        if(current_obstacle_amount < (max_obstacle_amount / 2) )
        {
            SpawnObstacle();
        }
    }

    void SpawnObstacle()
    {
        GameObject obstacle_instance = obstacle_pool.GetPooledObject();

        if (obstacle_instance != null)
        {
            Vector3 spawn_position;
            Vector3 new_position = spawn_area_center + new Vector3(Random.Range(-spawn_area_size.x/2, spawn_area_size.x/2), 0.0f,
                Random.Range(-spawn_area_size.z/2, spawn_area_size.z/2));

            spawn_position = new Vector3(new_position.x,
                obstacle_instance.GetComponent<ObstacleBase>().origin_height, new_position.z);

            if (Vector3.Distance(spawn_position, last_spawn_position) > min_distance_between_obstacles)
            {

                obstacle_instance.transform.position = spawn_position;
                obstacle_instance.transform.rotation = Quaternion.identity;
                obstacle_instance.SetActive(true);

                last_spawn_position = spawn_position;

                current_obstacle_amount += 1;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(spawn_area_center, spawn_area_size);
    }
}
