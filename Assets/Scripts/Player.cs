using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }

    [SerializeField] private float movementSpeed = 7f;
    [SerializeField] private GameInput Game_Input;
    [SerializeField] private LayerMask counterLayer;
    [SerializeField] private Transform kitchenObjectHoldPoint;


    private Vector2 inputVector;
    private bool isWalking;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    // RaycastHit hitInfo;
    Vector3 lastInteractDir;

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Game_Input.OnInteractAction += Game_Input_OnInteractAction;
    }

    private void Game_Input_OnInteractAction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        inputVector = Game_Input.GameInputVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        float interactDistance = 1f;
        // can also use RaycastAll to get an array of objects and then search one by one to see which one is counter
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, counterLayer))
        {
            bool CounterTouching = raycastHit.transform.TryGetComponent(out BaseCounter baseCounter);
            if (CounterTouching) {
                SetSelectedCounter(baseCounter);
            } else {
                SetSelectedCounter(null); 
             }
        } else {
            SetSelectedCounter(null);
            }
    }

    private void HandleMovement()
    {
        inputVector = Game_Input.GameInputVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float playerHeight = 2f;
        float playerRadius = 0.65f;
        float moveDistance = Time.deltaTime * movementSpeed;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            //* can't move in moveDir direction

            //checking if player can move only in x direction
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0);
            canMove = moveDir.x!=0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                // move direction is in x direction and can move is true
                moveDir = moveDirX;
            }
            else
            // * can't move in x
            {
                // checking if the player can move in z direction
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    // if he can then changing the moveDir to z direction. ( can move is still true)
                    moveDir = moveDirZ;
                }
                else
                {
                    // * can't move in any direction
                }
            }
        }
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        isWalking = (moveDir != Vector3.zero);

        float rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);

        /* other methods to rotate are 
        > transform.rotation - uses quaternion
        > transform.eulerAngles 
        > transform.lookAt */
    }

    public bool IsWalking() {
        return isWalking;
    }

    private void SetSelectedCounter(BaseCounter selectedCounter) {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = selectedCounter});
    }

    public Transform GetKitchenObjectFollowTransform() {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }
}
