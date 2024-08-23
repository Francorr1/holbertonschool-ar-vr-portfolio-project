using UnityEngine;
using UnityEngine.InputSystem;

public class StageController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float maxRotationAngle = 15f;
    public float rotationSpeed = 5f;

    private float targetAngleX;
    private float targetAngleZ;
    private float currentAngleX;
    private float currentAngleZ;

    private float xInput;
    private float yInput;

    void Update()
    {
        targetAngleX = xInput * maxRotationAngle;
        targetAngleZ = yInput * maxRotationAngle;

        currentAngleX = Mathf.LerpAngle(currentAngleX, targetAngleX, rotationSpeed * Time.deltaTime);
        currentAngleZ = Mathf.LerpAngle(currentAngleZ, targetAngleZ, rotationSpeed * Time.deltaTime);

        transform.localEulerAngles = new Vector3(currentAngleX, 0, currentAngleZ);
    }

    public void X(InputAction.CallbackContext context)
    {
        xInput = context.ReadValue<float>();
    }
    public void Y(InputAction.CallbackContext context)
    {
        yInput = -context.ReadValue<float>();
    }
}
