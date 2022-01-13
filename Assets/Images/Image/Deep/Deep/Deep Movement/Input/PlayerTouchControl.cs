
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTouchControl : MonoBehaviour
{
    #region Events
    // public static PlayerTouchControl instance;
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;
    private Camera mainCamera;
    protected Vector2 RawMovementInput;

    private void Awake() {
        // instance = this;    
        mainCamera = Camera.main;

    }

         
    #endregion
    public void StartTouchPrimary(InputAction.CallbackContext context){
        if (context.started) OnStartTouch?.Invoke(Utils.ScreenToWorld(mainCamera, RawMovementInput),(float)context.startTime);
        if (context.canceled) OnEndTouch?.Invoke(Utils.ScreenToWorld(mainCamera, RawMovementInput),(float)context.time);
    }
    public void PrimaryPositon(InputAction.CallbackContext context){
        RawMovementInput = context.ReadValue<Vector2>();
    }
    
    public Vector2 PrimaryPosition(){
        return Utils.ScreenToWorld(mainCamera, RawMovementInput);
    }
}
