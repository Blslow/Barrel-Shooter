using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem muzzleFlash;
    [SerializeField]
    private GameObject impactEffect;

    [SerializeField]
    private float fireRate = 6f;
    [SerializeField]
    private float damage = 10f;
    [SerializeField]
    private float maxAmmo = 6f;
    [SerializeField]
    private float ammo = 6f;
    [SerializeField]
    private List<TargetMaterialType> destroyableMaterials = new();

    public ParticleSystem MuzzleFlash { get => muzzleFlash; }
    public GameObject ImpactEffect { get => impactEffect; }

    public float FireRate { get => fireRate; }
    public float Damage { get => damage; }
    public float MaxAmmo { get => maxAmmo; }
    public float Ammo 
    { 
        get => ammo;
        set
        {
            if (value > maxAmmo)
                ammo = maxAmmo;
            else
                ammo = value;
        }
    }
    public IReadOnlyList<TargetMaterialType> DestroyableMaterials { get => destroyableMaterials; }
}
