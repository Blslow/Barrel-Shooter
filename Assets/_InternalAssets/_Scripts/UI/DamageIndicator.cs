using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DamageIndicator : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;
    [SerializeField]
    private float lifeTime = .6f;
    [SerializeField]
    private float minDist = 2f;
    [SerializeField]
    private float maxDist = 3f;

    private Vector3 initPos;
    private Vector3 targetPos;
    private float timer = 0f;

    private void Start()
    {
    }

    private void Update()
    {
        timer += Time.deltaTime;

        float fraction = lifeTime / 2f;

        if (timer > lifeTime)
        {
            timer = 0f;
            ObjectPooler.ReturnGameObject(gameObject);
        }
        //else if (timer > fraction)
        //    text.color = Color.Lerp(text.color, Color.clear, (timer - fraction) / (lifeTime - fraction));

        transform.position = Vector3.Lerp(initPos, targetPos, Mathf.Sin(timer / lifeTime));
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, Mathf.Sin(timer / lifeTime));
    }

    public void Initiate()
    {
        transform.LookAt(2 * transform.position - Camera.main.transform.position);

        float direction = UnityEngine.Random.rotation.eulerAngles.z;
        initPos = transform.position;

        float dist = UnityEngine.Random.Range(minDist, maxDist);

        targetPos = initPos + (Quaternion.Euler(0, 0, direction) * new Vector3(dist, dist, 0f)) / 2;

        transform.localScale = Vector3.zero;
    }

    public void SetDamageText(float damage)
    {
        text.text = damage.ToString();
    }
}
