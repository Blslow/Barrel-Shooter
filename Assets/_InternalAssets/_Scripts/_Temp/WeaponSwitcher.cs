using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> availableGuns = new();
    [SerializeField]
    private GameObject currentGun;

    private int currentGunIndex;

    public static event Action<Gun> OnWeaponSwitch;

    public void SwitchWeapon(float scrollValue)
    {
        if (availableGuns.Count <= 1 || scrollValue == 0)
            return;

        //if (scrollValue != 0)
        currentGun.SetActive(false);

        if (scrollValue > 0)
        {
            if (currentGunIndex >= availableGuns.Count - 1)
            {
                currentGunIndex = 0;
            }
            else
            {
                currentGunIndex++;
            }
        }
        else if (scrollValue < 0)
        {
            if (currentGunIndex <= 0)
            {
                currentGunIndex = availableGuns.Count - 1;
            }
            else
            {
                currentGunIndex--;
            }
        }
        availableGuns[currentGunIndex].SetActive(true);
        currentGun = availableGuns[currentGunIndex];

        OnWeaponSwitch?.Invoke(currentGun.GetComponent<Gun>());
    }
}
