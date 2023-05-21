using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleExplosionEffects : MonoBehaviour
{
    //[SerializeField]
    //private GameObject explosionEffect1;
    //[SerializeField]
    //private GameObject explosionEffect2;

    [SerializeField]
    private List<GameObject> explosionsEffectsList = new();

    private void OnEnable()
    {
        Target.OnTargetDestroyStaticEventWithReference += SpawnExplosionEffect;
    }
    private void OnDisable()
    {
        Target.OnTargetDestroyStaticEventWithReference -= SpawnExplosionEffect;
    }

    private void SpawnExplosionEffect(GameObject gameObject)
    {
        //Debug.Log("lol");
        int explosionIndex = Random.Range(0, explosionsEffectsList.Count);
        Debug.Log(explosionIndex);

        GameObject newGameObject = ObjectPooler.GetObject(explosionsEffectsList[explosionIndex]);
        newGameObject.name = explosionsEffectsList[explosionIndex].name;
        newGameObject.transform.position = gameObject.transform.position;

        StartCoroutine(DisableEffectAfterSeconds(newGameObject, 1.5f));
    }

    private IEnumerator DisableEffectAfterSeconds(GameObject effectGameObject, float time)
    {
        yield return new WaitForSeconds(time);
        ObjectPooler.ReturnGameObject(effectGameObject);
    }
}

//public enum ExplosionEffects
//{
//    EXPLOSION1,
//    EXPLOSION2
//}