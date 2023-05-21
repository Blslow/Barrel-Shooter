using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour
{
    [SerializeField]
    private float health = 50f;
    [SerializeField]
    private TargetMaterialType materialType;
    [SerializeField]
    private GameObject damageText;
    [SerializeField]
    private UnityEvent OnTargetDestroy;

    public TargetMaterialType MaterialType { get => materialType; }

    public void TakeDamage(float amount)
    {
        GameObject damageIndicator = ObjectPooler.GetObject(damageText);
        damageIndicator.name = "DamageIndicator";
        damageIndicator.transform.position = transform.position;
        damageIndicator.transform.rotation = Quaternion.identity;
        damageIndicator.GetComponent<DamageIndicator>().SetDamageText(amount);
        damageIndicator.GetComponent<DamageIndicator>().Initiate();

        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnTargetDestroy?.Invoke();
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