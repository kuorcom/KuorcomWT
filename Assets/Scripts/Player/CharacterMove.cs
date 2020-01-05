using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public bool detectPlatform = false;
    public static bool isMoving = false;

    [Header("Head Movement")]
    public Transform headTransform;
    public float xAxisSpeed = 1.0f;
    public float yAxisSpeed = 1.0f;

    [Header("Player Movement")]
    public CharacterController playerController;
    public float forwardMovementSpeed = 1.0f;
    public float sideMovementSpeed = 1.0f;
    public float gravity = 9.8f;

    [Header("UI")]
    bool useUiJoysticks = true;
    public Joystick LeftJoystick;
    public Joystick RightJoystick;

    // Start is called before the first frame update
    void Awake()
    {
        if(!headTransform)
        {
            headTransform = transform.GetChild(0);
        }
        //
        if(!playerController)
        {
            playerController = GetComponent<CharacterController>();
        }
        //
        if (detectPlatform)
        {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                useUiJoysticks = true;
            }
            else
            {
                useUiJoysticks = false;
            }
        }
    }

    private void Start()
    {
        if (!useUiJoysticks)
        {
            LeftJoystick.gameObject.SetActive(false);
            RightJoystick.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isInIsoMode || GameManager.isInMenu || GameManager.isInInteractionMenu || GameManager.isInTutorial)
            return;

        float playerRotation = 0;
        float playerForwardMovement = 0;
        float playerSideMovement = 0;
        float playerPitch = 0;

        if(useUiJoysticks)
        {
            playerSideMovement = LeftJoystick.Horizontal * Time.deltaTime * sideMovementSpeed;
            playerForwardMovement = LeftJoystick.Vertical * Time.deltaTime * forwardMovementSpeed;
            //
            playerPitch = -RightJoystick.Vertical * Time.deltaTime * xAxisSpeed;
            playerRotation = RightJoystick.Horizontal * Time.deltaTime * yAxisSpeed;
        }
        else
        {
            if (Input.GetMouseButton(1))
            {
                playerSideMovement = Input.GetAxis("Horizontal") * Time.deltaTime * sideMovementSpeed;
                playerForwardMovement = Input.GetAxis("Vertical") * Time.deltaTime * forwardMovementSpeed;
                //
                playerPitch = -Input.GetAxis("Mouse Y") * Time.deltaTime * xAxisSpeed;
                playerRotation = Input.GetAxis("Mouse X") * Time.deltaTime * yAxisSpeed;
                //
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                isMoving = true;
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                isMoving = false;
            }
        }

        //

        float actualPitch = headTransform.localEulerAngles.x;

        Vector3 playerFowardVector = transform.forward * playerForwardMovement;
        Vector3 playerRightVector = transform.right * playerSideMovement;
        Vector3 playerMovement = playerFowardVector + playerRightVector;
        playerMovement.y -= gravity * Time.deltaTime;

        playerController.Move(playerMovement);
        playerController.transform.Rotate(0, playerRotation, 0);
        headTransform.localRotation = Quaternion.Euler(actualPitch + playerPitch,0,0);
    }
}
