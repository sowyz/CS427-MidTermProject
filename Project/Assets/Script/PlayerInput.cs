using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    private Cinemachine.CinemachineVirtualCamera vcam;

    public UnityEvent onShoot = new UnityEvent();
    public UnityEvent<Vector2> onMoveBody = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> onMoveTurret = new UnityEvent<Vector2>();

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        GetBodyMovement();
        GetTurretMovement();
        GetShootingInput();
    }

    private void GetBodyMovement()
    {
        Vector2 movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        onMoveBody?.Invoke(movementVector.normalized);
    }

    private void GetTurretMovement()
    {
        onMoveTurret?.Invoke(GetMousePosition());
    }

    private Vector2 GetMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.nearClipPlane;
        Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        return mouseWorldPosition;
    }

    private void GetShootingInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            onShoot?.Invoke();
        }
    }

    public void OnTankChanged(GameObject newTank)
    {
        Debug.Log("OnTankChanged called.");
        onMoveBody.RemoveAllListeners();
        onMoveTurret.RemoveAllListeners();
        onShoot.RemoveAllListeners();

        if (newTank != null)
        {
            Debug.Log("Adding listeners for new tank.");
            TankController newTankController = newTank.GetComponent<TankController>();
            onMoveBody.AddListener(newTankController.HandleMoveBody);
            onMoveTurret.AddListener(newTankController.HandleTurretMovement);
            onShoot.AddListener(newTankController.HandleShoot);

            vcam = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
            if (vcam != null)
            {
                vcam.Follow = newTank.transform;
                vcam.LookAt = newTank.transform;
            }
            else
            {
                Debug.LogError("CinemachineVirtualCamera not found.");
            }
        }
        else
        {
            Debug.LogError("NewTank is null.");
        }
    }
}
