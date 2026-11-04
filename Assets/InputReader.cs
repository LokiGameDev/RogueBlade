using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(fileName = "New Input Reader", menuName = "Input/Input Reader")]
/// <summary>Reads player input actions and exposes them as game events.</summary>
public class InputReader : ScriptableObject, IPlayerActions
{
    private Controls controls;

    /// <summary>Creates and enables the generated input actions.</summary>
    private void OnEnable()
    {
        if(controls == null)
        {
            controls = new Controls();
            controls.Player.SetCallbacks(this);
        }
        controls.Player.Enable();
    }
    /// <summary>Disables the input actions when the asset is unloaded.</summary>
    private void OnDisable()
    {
        controls.Player.Disable();
    }

    public event Action<Vector2> PlayerMovementEvent;
    public event Action OnPlayerAttack;
    public event Action OnPlayerThrow;
    public event Action OnPlayerDash;
    public event Action OnPlayerRefillAmmo;
    public event Action OnPlayerTurretBuild;
    public event Action OnPlayerHomeHealth;
    public event Action OnPlayerSpawnState;
    public event Action OnPlayerSpawnStart;
    public Vector2 movementValue;

    /// <summary>Publishes movement input and stores its current value.</summary>
    public void OnMove(InputAction.CallbackContext context)
    {
        PlayerMovementEvent?.Invoke(context.ReadValue<Vector2>());
        movementValue = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.performed) OnPlayerAttack?.Invoke();
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        if(context.performed) OnPlayerThrow?.Invoke();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if(context.performed) OnPlayerDash?.Invoke();
    }

    public void OnAmmoRefill(InputAction.CallbackContext context)
    {
        if(context.performed) OnPlayerRefillAmmo?.Invoke();
    }

    public void OnTurretBuild(InputAction.CallbackContext context)
    {
        if(context.performed) OnPlayerTurretBuild?.Invoke();
    }

    public void OnHomeHealth(InputAction.CallbackContext context)
    {
        if(context.performed) OnPlayerHomeHealth?.Invoke();
    }

    public void OnSpawnState(InputAction.CallbackContext context)
    {
        if(context.performed) OnPlayerSpawnState.Invoke();
    }

    public void OnSpawnStart(InputAction.CallbackContext context)
    {
        if(context.performed) OnPlayerSpawnStart.Invoke();
    }
}