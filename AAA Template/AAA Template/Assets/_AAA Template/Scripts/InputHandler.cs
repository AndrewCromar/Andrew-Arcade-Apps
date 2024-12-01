using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class InputHandler : MonoBehaviour
{
    [HideInInspector] public static InputHandler instance;

    private void Awake() => instance = this;

    [HideInInspector] public UnityEvent northBtnDown;
    [HideInInspector] public UnityEvent northBtnUp;
    [HideInInspector] public UnityEvent eastBtnDown;
    [HideInInspector] public UnityEvent eastBtnUp;
    [HideInInspector] public UnityEvent southBtnDown;
    [HideInInspector] public UnityEvent southBtnUp;
    [HideInInspector] public UnityEvent westBtnDown;
    [HideInInspector] public UnityEvent westBtnUp;
    [HideInInspector] public UnityEvent positiveBtnDown;
    [HideInInspector] public UnityEvent positiveBtnUp;
    [HideInInspector] public UnityEvent negativeBtnDown;
    [HideInInspector] public UnityEvent negativeBtnUp;
    [HideInInspector] public UnityEvent joystickBtnDown;
    [HideInInspector] public UnityEvent joystickBtnUp;
    [HideInInspector] public UnityEvent<float> joystickXAxys;
    [HideInInspector] public UnityEvent<float> joystickYAxys;

    public void NorthBtn(InputAction.CallbackContext ctx) => (ctx.performed ? northBtnDown : northBtnUp)?.Invoke();
    public void EastBtn(InputAction.CallbackContext ctx) => (ctx.performed ? eastBtnDown : eastBtnUp)?.Invoke();
    public void SouthBtn(InputAction.CallbackContext ctx) => (ctx.performed ? southBtnDown : southBtnUp)?.Invoke();
    public void WestBtn(InputAction.CallbackContext ctx) => (ctx.performed ? westBtnDown : westBtnUp)?.Invoke();
    public void PositiveBtn(InputAction.CallbackContext ctx) => (ctx.performed ? positiveBtnDown : positiveBtnUp)?.Invoke();
    public void NegativeBtn(InputAction.CallbackContext ctx) => (ctx.performed ? negativeBtnDown : negativeBtnUp)?.Invoke();
    public void JoystickBtn(InputAction.CallbackContext ctx) => (ctx.performed ? joystickBtnDown : joystickBtnUp)?.Invoke();
    public void JoystickXAxys(InputAction.CallbackContext ctx) { if (ctx.performed) joystickXAxys?.Invoke(ctx.ReadValue<float>()); }
    public void JoystickYAxys(InputAction.CallbackContext ctx) { if (ctx.performed) joystickYAxys?.Invoke(ctx.ReadValue<float>()); }
}