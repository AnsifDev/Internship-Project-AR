using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player: MonoBehaviour, Damagable
{
    private CharacterController characterController;
    private Vector3 movementDirection;
    [SerializeField] private float walkSpeed, runSpeed, jumpPower, gravity = 9.8f;
    [SerializeField] private Transform cameraTransform;
    private float cameraRotX;
    [SerializeField] private float lookSpeed, lookXLimit = 45;
    [SerializeField] private bool btn_down = false, mouse_controll = false;
    [SerializeField] private Gun gun;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject BG, Mains;
    private Enemy[] enemies;
    private int numberOfEnemies;
    private Vector3 SpwanPosition;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        if (mouse_controll) Cursor.lockState = CursorLockMode.Locked;
        else Cursor.lockState = CursorLockMode.None;
        Cursor.visible = !mouse_controll; 
        enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies) enemy.gameObject.SetActive(false);
        numberOfEnemies = enemies.Length;
        Debug.Log(numberOfEnemies);
        SpwanPosition = transform.position;
    }

    private void reset(bool controll) {
        btn_down = false;
        if (controll) Cursor.lockState = CursorLockMode.Locked;
        else Cursor.lockState = CursorLockMode.None;
        Cursor.visible = !controll; 
        foreach (Enemy enemy in enemies) enemy.gameObject.SetActive(controll);
        Mains.SetActive(!controll);
        BG.SetActive(!controll);
        slider.gameObject.SetActive(controll);
        slider.value = 10;
        transform.position = SpwanPosition;
    }

    public void OnBtnClicked(string id) {
        switch (id)
        {
            case "play": 
                Debug.Log("Play"); 
                reset(true);
                mouse_controll = true;
                break;
            case "quit": Debug.Log("Quit"); break;
        }
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

        if (Input.GetMouseButtonDown(0)) {
            if (mouse_controll) {
                Enemy enemy = (Enemy) gun.Shoot();
                if (enemy != null && enemy.GetDamage() <= 0) {
                    numberOfEnemies--;
                    Debug.Log(numberOfEnemies);
                    if (numberOfEnemies <= 0) {
                        foreach (Enemy enemy1 in enemies)
                        {
                            enemy1.gameObject.SetActive(true);
                            numberOfEnemies++;                            
                        }
                    }
                }
            }
        }

        if (!characterController.isGrounded)
        {
            movementDirection.y -= gravity * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Escape)) btn_down = true;
        else if (btn_down) {
            mouse_controll = !mouse_controll;
            reset(mouse_controll);
        }
        

        characterController.Move(movementDirection * Time.deltaTime);

        if (mouse_controll) cameraRotX += -Input.GetAxis("Mouse Y") * lookSpeed;
        // Debug.Log(cameraRotX);
        
        cameraRotX = Mathf.Clamp(cameraRotX, -lookXLimit, lookXLimit);
        cameraTransform.localRotation = Quaternion.Euler(cameraRotX, 0, 0);
        if (mouse_controll) transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    public void Damage() {
        slider.value -= 1;
        if (slider.value <= 0) UnityEditor.EditorApplication.isPlaying = false;
    }

    public void OnDamage()
    {
        slider.value -= 1;
        if (slider.value <= 0) {
            reset(false);
        }
    }

    public int GetDamage()
    {
        return (int) slider.value;
    }
}