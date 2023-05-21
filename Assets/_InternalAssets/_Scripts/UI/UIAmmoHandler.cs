using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIAmmoHandler : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;

    private void OnEnable()
    {
        CharacterShoot.OnShootWithAmmoAmountReference += UpdateAmmoText;
    }
    private void OnDisable()
    {
        CharacterShoot.OnShootWithAmmoAmountReference -= UpdateAmmoText;
    }

    private void UpdateAmmoText(int ammo, int maxAmmo)
    {
        text.text = ammo.ToString() + "/" + maxAmmo.ToString();
    }
}
