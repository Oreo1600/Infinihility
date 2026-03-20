using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
    }

}
