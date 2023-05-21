using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDamageTypeIconsHandler : MonoBehaviour
{
    [SerializeField]
    private Image woodIcon;
    [SerializeField]
    private Image silverIcon;
    [SerializeField]
    private Image goldIcon;
    [SerializeField]
    private Image platinumIcon;
    [SerializeField]
    private Image diamondIcon;

    private List<Image> allIcons = new();

    private void Start()
    {
        allIcons.Add(woodIcon);
        allIcons.Add(silverIcon);
        allIcons.Add(goldIcon);
        allIcons.Add(platinumIcon);
        allIcons.Add(diamondIcon);
    }

    private void OnEnable()
    {
        WeaponSwitcher.OnWeaponSwitch += UpdateIcons;
    }
    private void OnDisable()
    {
        WeaponSwitcher.OnWeaponSwitch -= UpdateIcons;
    }

    private void UpdateIcons(Gun gun)
    {
        foreach (Image icon in allIcons)
        {
            icon.gameObject.SetActive(false);
        }

        int countOfActiveIcons = 0;
        Image currentlyEditedIcon;

        foreach (TargetMaterialType material in gun.DestroyableMaterials)
        {
            switch (material)
            {
                case TargetMaterialType.WOOD:
                    currentlyEditedIcon = woodIcon;
                    break;
                case TargetMaterialType.SILVER:
                    currentlyEditedIcon = silverIcon;
                    break;
                case TargetMaterialType.GOLD:
                    currentlyEditedIcon = goldIcon;
                    break;
                case TargetMaterialType.PLATINUM:
                    currentlyEditedIcon = platinumIcon;
                    break;
                case TargetMaterialType.DIAMOND:
                    currentlyEditedIcon = diamondIcon;
                    break;
                case TargetMaterialType.URANIUM:
                    return;
                default:
                    return;
            }

            currentlyEditedIcon.gameObject.SetActive(true);
            currentlyEditedIcon.rectTransform.position = new Vector3(50 * countOfActiveIcons, 0, 0);
            countOfActiveIcons++;
        }
    }
}
