using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 movementDirection;
    [SerializeField] private float walkSpeed, runSpeed, jumpPower, gravity = 9.8f;
    [SerializeField] private Transform cameraTransform;
    private float cameraRotX;
    [SerializeField] private float lookSpeed, lookXLimit = 45;
    [SerializeField] private bool btn_down = false, mouse_controll = false;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        if (mouse_controll) Cursor.lockState = CursorLockMode.Locked;
        else Cursor.lockState = CursorLockMode.None;
        Cursor.visible = !mouse_controll; 
    }

    private void Update()
    {
        bool isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        float currentSpeedX = (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical");
        float currentSpeedY = (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal");
        Vector3 tempDirection = (transform.forward * currentSpeedX) + (transform.right * currentSpeedY);
        movementDirection.x = tempDirection.x;
        movementDirection.z = tempDirection.z;

        if (Input.GetButton("Jump") && characterController.isGrounded)
        {
            movementDirection.y = jumpPower;
        }

        if (!characterController.isGrounded)
        {
            movementDirection.y -= gravity * Time.deltaTime;
            //movementDirection.y = movementDirection.y - gravity * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Tab)) btn_down = true;
        else if (btn_down) {
            mouse_controll = !mouse_controll;
            btn_down = false;
            if (mouse_controll) Cursor.lockState = CursorLockMode.Locked;
            else Cursor.lockState = CursorLockMode.None;
            Cursor.visible = !mouse_controll; 
        }
        

        characterController.Move(movementDirection * Time.deltaTime);

        if (mouse_controll) cameraRotX += -Input.GetAxis("Mouse Y") * lookSpeed;
        // Debug.Log(cameraRotX);
        
        cameraRotX = Mathf.Clamp(cameraRotX, -lookXLimit, lookXLimit);
        cameraTransform.localRotation = Quaternion.Euler(cameraRotX, 0, 0);
        if (mouse_controll) transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }
}