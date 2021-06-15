using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float power = 0.2f;
    public float duration = 0.2f;
    public float slow_down_amount = 0.1f;

    [HideInInspector]
    public bool should_shake;

    private float initial_duration;
    private Vector3 start_position;

    // Start is called before the first frame update
    void Start()
    {
        start_position = transform.localPosition;
        initial_duration = duration;
    }

    // Update is called once per frame
    void Update()
    {
        Shake();

        //if (!PauseControl.game_is_paused)
        //{
        //    Shake();
        //}
        //else
        //{
        //    duration = initial_duration;
        //    transform.localPosition = start_position;
        //}
    }

    void Shake()
    {
        if (should_shake)
        {
            if (duration > 0f) //if we still have time to shake
            {
                //distorting localposition
                transform.localPosition = start_position + Random.insideUnitSphere * power;

                duration -= Time.deltaTime * slow_down_amount; //smooth out the shaking
            }
            else
            {
                //if shaking is over (less or equal to 0) then reset the duration and localposition and stop
                should_shake = false;
                duration = initial_duration;
                transform.localPosition = start_position;
            }
        }
    }
}
