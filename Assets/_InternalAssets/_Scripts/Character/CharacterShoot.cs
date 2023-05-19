using UnityEngine;

public class CharacterShoot : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private ParticleSystem muzzleFlash;

    [SerializeField]
    private float damage = 10f;
    [SerializeField]
    private float range = 100f;

    public void Shoot()
    {
        if (muzzleFlash)
            muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target)
                target.TakeDamage(damage);
        }
    }
}
