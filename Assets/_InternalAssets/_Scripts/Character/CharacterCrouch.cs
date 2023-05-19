using UnityEngine;
using UnityEngine.Events;

public class CharacterCrouch : MonoBehaviour
{
    private CharacterController characterController;

    //private bool isSprinting = false;
    private bool isCrouching = false;
    private bool lerpCrouch = false;
    private float crouchTimer = 0;

    [Header("Events")]
    [SerializeField]
    private UnityEvent<bool> OnCrouch;

    private void Awake()
    {
        characterController = GetComponentInParent<CharacterController>();
    }

    private void Update()
    {
        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (isCrouching)
                characterController.height = Mathf.Lerp(characterController.height, 1, p);
            else
                characterController.height = Mathf.Lerp(characterController.height, 2, p);

            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }

    public void Crouch()
    {
        isCrouching = !isCrouching;
        OnCrouch?.Invoke(isCrouching);
        crouchTimer = 0;
        lerpCrouch = true;
    }

}
