using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 4f;

    [SerializeField]
    private float sprintSpeed = 8f;

    private float speed;

    [SerializeField]
    private float speedOffset = 0.1f;

    [SerializeField]
    private Transform headTransform;

    private Vector3 oldInputPos;
    private Vector3 oldInputRot;

    private CharacterController characterController;

    private float forwardVelocity;
    private float rightVelocity;



    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        speed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        bool updated = ClientInput();
        ClientUpdateTransform();
    }

    private void ClientUpdateTransform()
    {

        characterController.SimpleMove(oldInputPos);

        if (oldInputRot != Vector3.zero)
        {
            //characterController.transform.rotation = Quaternion.Euler(0, oldInputRot.y, 0);
        }
    }

    private bool ClientInput()
    {
        Vector3 inputRot = new Vector3(0, headTransform.rotation.eulerAngles.y, 0);

        Vector3 directionForward = headTransform.TransformDirection(Vector3.forward);
        Vector3 directionRight = headTransform.TransformDirection(Vector3.right);

        float forwardInput = Input.GetAxis("Vertical");
        float rightInput = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (speed < sprintSpeed)
                speed += 0.1f;
        }
        else
        if (speed > walkSpeed)
        {
            speed -= 0.1f;
        }

        forwardVelocity = forwardInput * speed;
        rightVelocity = rightInput * speed;

        //forwardVelocity = Mathf.Clamp(forwardVelocity, -sprintSpeed, sprintSpeed);
        //rightVelocity = Mathf.Clamp(rightVelocity, -6f, 6f);

        Vector3 inputPos = directionForward * forwardVelocity + directionRight * rightVelocity;

        bool changed = false;

        //player transform
        if (oldInputPos != inputPos || oldInputRot != inputRot)
        {
            oldInputRot = inputRot;
            oldInputPos = inputPos;
            changed = true;
        }

        return changed;
    }
}