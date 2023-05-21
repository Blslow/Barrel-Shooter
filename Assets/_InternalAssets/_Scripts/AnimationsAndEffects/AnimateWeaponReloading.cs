using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateWeaponReloading : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private void OnEnable()
    {
        CharacterShoot.OnReload += StartReloadingAnimation;
        CharacterShoot.OnStopReloading += StopReloadingAnimation;
    }
    private void OnDisable()
    {
        CharacterShoot.OnReload -= StartReloadingAnimation;
        CharacterShoot.OnStopReloading -= StopReloadingAnimation;
    }

    private void StartReloadingAnimation()
    {
        animator.SetBool("Reloading", true);
    }

    private void StopReloadingAnimation()
    {
        animator.SetBool("Reloading", false);
    }
}
