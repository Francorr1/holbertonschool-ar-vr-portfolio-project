using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    // Global variable decalarations
    public GameObject doorLeft; // Left door game object
    public GameObject doorRight; // Right door game object
    public Collider doorLeftCollider; // Left door collider
    public Collider doorRightCollider; // Right door collider
    public float doorMovementDistance = 1f; // Distance to move the doors
    public float doorMovementSpeed = 0.5f; // Speed to move the doors
    private bool doorsOpen = false; // Boolean to check if the doors are open

    // Runs when an object, the ball or a guard enters the button trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Object" || other.tag == "Ball")
        {
            if (!doorsOpen)
            {
                doorsOpen = true;
                StartCoroutine(OpenDoors());
                Debug.Log("Doors open");
            }
        }
    }

    // Runs when an object, the ball or a guard exits the button trigger
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Object" || other.tag == "Ball")
        {
            if (doorsOpen)
            {
                doorsOpen = false;
                StartCoroutine(CloseDoors());
                Debug.Log("Doors Close");
            }
        }
    }

    // Coroutine that opens the doors
    IEnumerator OpenDoors()
    {
        doorLeftCollider.enabled = false;
        doorRightCollider.enabled = false;
        float elapsedTime = 0f;

        while (elapsedTime < doorMovementSpeed)
        {
            doorLeft.transform.Translate(Vector3.left * doorMovementDistance * (Time.deltaTime / doorMovementSpeed), Space.Self);
            doorRight.transform.Translate(Vector3.right * doorMovementDistance * (Time.deltaTime / doorMovementSpeed), Space.Self);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    // Coroutine that closes the doors
    IEnumerator CloseDoors()
    {
        float elapsedTime = 0f;

        while (elapsedTime < doorMovementSpeed)
        {
            doorLeft.transform.Translate(Vector3.left * -doorMovementDistance * (Time.deltaTime / doorMovementSpeed), Space.Self);
            doorRight.transform.Translate(Vector3.right * -doorMovementDistance * (Time.deltaTime / doorMovementSpeed), Space.Self);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (doorLeft.transform.localPosition.x != -0.26 || doorRight.transform.localPosition.x != 0.26)
        {
            doorRight.transform.localPosition = new Vector3(0.26f, 0, 0);
            doorLeft.transform.localPosition = new Vector3(-0.26f, 0, 0);
        }

        doorLeftCollider.enabled = true;
        doorRightCollider.enabled = true;
    }

    // Update is called once per frame
    // Fixes doors positions to account for the stage tilt
    void Update()
    {
        if (doorLeft.transform.localPosition.y != 0 || doorLeft.transform.localPosition.z != 0)
        {
            doorLeft.transform.localPosition = new Vector3(doorLeft.transform.localPosition.x, 0, 0);
        }

        if (doorRight.transform.localPosition.y != 0 || doorRight.transform.localPosition.z != 0)
        {
            doorRight.transform.localPosition = new Vector3(doorRight.transform.localPosition.x, 0, 0);
        }
    }
}
