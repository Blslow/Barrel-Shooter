using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnExplosionEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject particles;

    public void Spawn()
    {
        GameObject newGameObject = ObjectPooler.GetObject(particles);
        newGameObject.name = particles.name;
        newGameObject.transform.position = transform.position;
        StartCoroutine(DisableEffectAfterSeconds(newGameObject, 1.5f));
    }

    private IEnumerator DisableEffectAfterSeconds(GameObject effectGameObject, float time)
    {
        yield return new WaitForSeconds(time);
        ObjectPooler.ReturnGameObject(effectGameObject);
    }
}
