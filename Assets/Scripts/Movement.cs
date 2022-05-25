using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public static PlayerMovement controls;
    public static bool canRotate;

    Vector2 move;
    public CharacterController cc;


    public float speed = 5f;
    public float verticalSpeed = 0;

    public float Gravity = -9.8f;
   

    float rotY;
    Vector3 movement;

    Vector2 rotate;

    private void Awake()
    {
        controls = new PlayerMovement();

        controls.Player.Move.performed += tgb => move = tgb.ReadValue<Vector2>();
        controls.Player.Move.canceled += tgb => move = Vector2.zero;
        controls.Player.Look.performed += tgb => rotate = tgb.ReadValue<Vector2>();
        controls.Player.Look.canceled += tgb => rotate = Vector2.zero;


    }
    private void OnEnable()
    {
        controls.Player.Enable();
    }
    private void OnDisable()
    {
        controls.Player.Disable();
    }
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        canRotate = true;
        cc = GetComponent<CharacterController>();

    }
   
    void FixedUpdate()
    {
        //Movement
        movement = Vector3.zero;


        float xSpeed = move.y * speed * Time.deltaTime;
        movement += transform.forward * xSpeed;
        float ySpeed = move.x * speed * Time.deltaTime;
        movement += transform.right * ySpeed;

        //Gravtity
        verticalSpeed += Gravity * Time.deltaTime;

        movement += transform.up * verticalSpeed * Time.deltaTime;


        /*
        //Player Rotation
        if (canRotate)
        {
            Vector2 r = new Vector2(0, rotate.x) * mouseSensitivity * Time.deltaTime * 10;
            transform.Rotate(r, Space.Self);
            Quaternion q = transform.rotation;
            q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, 0);
            transform.rotation = q;

            //Camera Rotation

            rotY += -rotate.y * mouseSensitivity * Time.deltaTime * 10;
            rotY = Mathf.Clamp(rotY, -90, 90);
            camTransform.transform.localRotation = Quaternion.Euler(rotY, 0, 0);
        }*/
        cc.Move(movement);


    }
}
