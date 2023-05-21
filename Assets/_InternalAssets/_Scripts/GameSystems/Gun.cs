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
    private bool autoFire;

    [SerializeField]
    private float fireRate = 6f;
    [SerializeField]
    private float damage = 10f;
    [SerializeField]
    private int maxAmmo = 6;
    [SerializeField]
    private int ammo = 6;
    [SerializeField]
    private List<TargetMaterialType> destroyableMaterials = new();

    [SerializeField][Range(0f, 15f)]
    private float recoilAmountY = 3.2f;
    [SerializeField][Range(0f, 15f)]
    private float recoilAmountX = 4f;
    [SerializeField]
    private bool recoilPositiveYOnly;

    public ParticleSystem MuzzleFlash { get => muzzleFlash; }
    public GameObject ImpactEffect { get => impactEffect; }

    public bool AutoFire { get => autoFire; }
    public float FireRate { get => fireRate; }
    public float Damage { get => damage; }
    public int MaxAmmo { get => maxAmmo; }
    public int Ammo 
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
    public float RecoilAmountY { get => recoilAmountY; }
    public float RecoilAmountX { get => recoilAmountX; }
    public bool RecoilPositiveYOnly { get => recoilPositiveYOnly; }

    public bool CanDestroy(TargetMaterialType materialType)
    {
        if (destroyableMaterials.Contains(materialType))
            return true;

        return false;
    }
}
