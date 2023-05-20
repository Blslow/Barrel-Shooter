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
    private float damage = 10f;
    [SerializeField]
    private float range = 100f;
    [SerializeField]
    private float fireRate = 6f;

    [SerializeField]
    private Gun currentGun;

    [SerializeField]
    private List<GameObject> availableGuns = new();

    private Coroutine fireCoroutine;
    private WaitForSeconds rapidFireWait;

    private void Awake()
    {
        rapidFireWait = new WaitForSeconds(1 / fireRate);
    }

    public void StartShooting()
    {
        fireCoroutine = StartCoroutine(RapidFire());
    }
    public void StopShooting()
    {
        if (fireCoroutine != null)
            StopCoroutine(fireCoroutine);
    }

    private void Shoot()
    {
        if (muzzleFlash)
            muzzleFlash.Play();

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
    }

    private IEnumerator ReturnObjectAfterSeconds(GameObject gameObject, float time)
    {
        yield return new WaitForSeconds(time);
        ObjectPooler.ReturnGameObject(gameObject);
    }

    //private IEnumerator WaitBeforeNextShoot()
    //{
    //    canShoot = false;
    //    yield return rapidFireWait;
    //    canShoot = true;
    //}

    private IEnumerator RapidFire()
    {
        while(true)
        {
            Shoot();
            yield return rapidFireWait;
        }
    }
}
