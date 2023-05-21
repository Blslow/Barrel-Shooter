using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShoot : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private ParticleSystem muzzleFlash;
    [SerializeField]
    private GameObject impactEffect;

    [SerializeField]
    private float range = 100f;

    [SerializeField]
    private bool autoFire;
    [SerializeField]
    private float fireRate = 0f;
    [SerializeField]
    private float damage = 0f;
    [SerializeField]
    private float maxAmmo = 0f;
    [SerializeField]
    private float ammo = 0f;

    private float recoilAmountY = 3.2f;
    private float recoilAmountX = 4f;
    private float currentRecoilXPos;
    private float currentRecoilYPos;
    private float maxRecoilTime = 4f;
    private float timePressed;
    private bool recoilPositiveYOnly;

    private bool isReloading = false;


    [SerializeField]
    private Gun currentGun;

    //[SerializeField]
    //private List<GameObject> availableGuns = new();

    private Coroutine fireCoroutine;
    private WaitForSeconds rapidFireWait;

    public static event Action<Vector2> OnShoot;
    public static event Action OnReload;
    public static event Action OnStopReloading;

    private void Awake()
    {
        rapidFireWait = new WaitForSeconds(1 / fireRate);
    }

    private void Start()
    {
        UpdateCurrentGunStats();
        ammo = maxAmmo;
    }

    private void OnEnable()
    {
        WeaponSwitcher.OnWeaponSwitch += UpdateCurrentGun;
    }
    private void OnDisable()
    {
        WeaponSwitcher.OnWeaponSwitch -= UpdateCurrentGun;
    }

    public void StartShooting()
    {
        //if (ammo <= 0)
        //{
        //    StartCoroutine(ReloadAmmo());
        //    return;
        //}

        if (autoFire)
            fireCoroutine = StartCoroutine(RapidFire());
        else
            Shoot();
    }
    public void StopShooting()
    {
        if (fireCoroutine != null && autoFire)
            StopCoroutine(fireCoroutine);
    }

    private void Shoot()
    {
        if (ammo <= 0)
        {
            StartCoroutine(ReloadAmmo());
            return;
        }

        if (muzzleFlash)
            muzzleFlash.Play();

        ammo--;

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target)
                target.TakeDamage(damage);

            //Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            GameObject impact = ObjectPooler.GetObject(impactEffect);
            impact.name = "ImpactEffect";
            impact.transform.position = hit.point;
            impact.transform.rotation = Quaternion.LookRotation(hit.normal);

            StartCoroutine(ReturnObjectAfterSeconds(impact, 1f));
        }

        OnShoot?.Invoke(Recoil(recoilPositiveYOnly));
    }

    private IEnumerator ReloadAmmo()
    {
        if (isReloading)
        {
            yield return null;
        }
        else
        {
            Debug.Log("Reloading ammo");
            OnReload?.Invoke();
            isReloading = true;
            yield return new WaitForSeconds(1.7f);
            OnStopReloading?.Invoke();
            yield return new WaitForSeconds(.25f);
            ammo = maxAmmo;
            isReloading = false;

        }
    }

    private Vector2 Recoil(bool positiveYOnly)
    {
        currentRecoilXPos = ((UnityEngine.Random.value - .5f) / 2) * recoilAmountX;
        currentRecoilYPos = ((UnityEngine.Random.value - .5f) / 2) * (timePressed >= maxRecoilTime ? recoilAmountY / 4 : recoilAmountY);
        //Debug.Log(currentRecoilXPos + ", " + currentRecoilYPos);
        if (positiveYOnly)
            return new Vector2(currentRecoilXPos, Math.Abs(currentRecoilYPos));
        else
            return new Vector2(currentRecoilXPos, currentRecoilYPos);
    }

    private IEnumerator ReturnObjectAfterSeconds(GameObject gameObject, float time)
    {
        yield return new WaitForSeconds(time);
        ObjectPooler.ReturnGameObject(gameObject);
    }

    private IEnumerator RapidFire()
    {
        while(true)
        {
            Shoot();
            yield return rapidFireWait;
        }
    }
    
    private void UpdateCurrentGunStats()
    {
        muzzleFlash = currentGun.MuzzleFlash;
        impactEffect = currentGun.ImpactEffect;
        autoFire = currentGun.AutoFire;
        fireRate = currentGun.FireRate;
        damage = currentGun.Damage;
        maxAmmo = currentGun.MaxAmmo;
        recoilAmountY = currentGun.RecoilAmountY;
        recoilAmountX = currentGun.RecoilAmountX;
        recoilPositiveYOnly = currentGun.RecoilPositiveYOnly;
        rapidFireWait = new WaitForSeconds(1 / fireRate);
    }

    private void UpdateCurrentGun(Gun gun)
    {
        StopShooting();
        currentGun.Ammo = ammo;
        currentGun = gun;
        ammo = currentGun.Ammo;
        UpdateCurrentGunStats();
        StopAllCoroutines();
        isReloading = false;
        OnStopReloading?.Invoke();
    }
}
