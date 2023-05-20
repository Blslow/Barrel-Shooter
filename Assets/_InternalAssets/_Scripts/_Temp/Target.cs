using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    private float health = 50f;
    [SerializeField]
    private TargetMaterialType materialType;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}

public enum TargetMaterialType
{
    WOOD,
    SILVER,
    GOLD,
    PLATINUM,
    DIAMOND,
    URANIUM,
}