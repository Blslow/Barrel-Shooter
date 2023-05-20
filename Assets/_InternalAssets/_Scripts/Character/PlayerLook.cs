using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    //private float recoilAmountY = 3.2f;
    //private float recoilAmountX = 4f;
    //private float currentRecoilXPos;
    //private float currentRecoilYPos;
    //private float maxRecoilTime = 4f;
    //private float timePressed;

    private float xRotation = 0f;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float xSensitivity = 120f;
    [SerializeField]
    private float ySensitivity = 120f;

    private void OnEnable()
    {
        /// recoil
        CharacterShoot.OnShoot += ProcessLook;
    }
    private void OnDisable()
    {
        CharacterShoot.OnShoot -= ProcessLook;
    }

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.parent.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }

    //private void Recoil(Vector2 value)
    //{
    //    currentRecoilXPos = ((Random.value - .5f) / 2) * recoilAmountX;
    //    currentRecoilYPos = ((Random.value - .5f) / 2) * (timePressed >= maxRecoilTime ? recoilAmountY / 4 : recoilAmountY);
    //    //Debug.Log(currentRecoilXPos + ", " + currentRecoilYPos);
    //    ProcessLook(new Vector2(currentRecoilXPos, currentRecoilYPos));
    //}
}
