using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int max_health;

    private int current_health;

    [HideInInspector]
    public bool is_dead = false;

    // Start is called before the first frame update
    void Start()
    {
        current_health = max_health;
    }

    public void TakeDamage(int amount)
    {
        current_health = Mathf.Clamp(current_health - amount, 0, max_health);
        if (current_health <= 0)
        {
            is_dead = true;
            this.gameObject.SetActive(false);
        }
    }

    public int GetCurrentHealth()
    {
        return current_health;
    }
}
