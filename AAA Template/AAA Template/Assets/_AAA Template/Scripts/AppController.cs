using UnityEngine;
using UnityEngine.UI;

public class AppController : MonoBehaviour
{
    [SerializeField] private Text inputText;

    [SerializeField] private bool northBtn;
    [SerializeField] private bool eastBtn;
    [SerializeField] private bool southBtn;
    [SerializeField] private bool westBtn;
    [SerializeField] private bool positiveBtn;
    [SerializeField] private bool negativeBtn;
    [SerializeField] private bool joystickBtn;

    [SerializeField] private float joystickX = 0f;
    [SerializeField] private float joystickY = 0f;

    private void Start()
    {
        InputHandler.instance.northBtnDown.AddListener(() => OnButtonDown(ref northBtn));
        InputHandler.instance.northBtnUp.AddListener(() => OnButtonUp(ref northBtn));

        InputHandler.instance.eastBtnDown.AddListener(() => OnButtonDown(ref eastBtn));
        InputHandler.instance.eastBtnUp.AddListener(() => OnButtonUp(ref eastBtn));

        InputHandler.instance.southBtnDown.AddListener(() => OnButtonDown(ref southBtn));
        InputHandler.instance.southBtnUp.AddListener(() => OnButtonUp(ref southBtn));

        InputHandler.instance.westBtnDown.AddListener(() => OnButtonDown(ref westBtn));
        InputHandler.instance.westBtnUp.AddListener(() => OnButtonUp(ref westBtn));

        InputHandler.instance.positiveBtnDown.AddListener(() => OnButtonDown(ref positiveBtn));
        InputHandler.instance.positiveBtnUp.AddListener(() => OnButtonUp(ref positiveBtn));

        InputHandler.instance.negativeBtnDown.AddListener(() => OnButtonDown(ref negativeBtn));
        InputHandler.instance.negativeBtnUp.AddListener(() => OnButtonUp(ref negativeBtn));

        InputHandler.instance.joystickBtnDown.AddListener(() => OnButtonDown(ref joystickBtn));
        InputHandler.instance.joystickBtnUp.AddListener(() => OnButtonUp(ref joystickBtn));

        InputHandler.instance.joystickXAxys.AddListener(OnJoystickXAxisChange);
        InputHandler.instance.joystickYAxys.AddListener(OnJoystickYAxisChange);
    }

    private void Update()
    {
        inputText.text = $"North: {northBtn}\n" +
                         $"East: {eastBtn}\n" +
                         $"South: {southBtn}\n" +
                         $"West: {westBtn}\n" +
                         $"Positive: {positiveBtn}\n" +
                         $"Negative: {negativeBtn}\n" +
                         $"Joystick: {joystickBtn}\n" +
                         $"Joystick X Axis: {joystickX:F2}\n" +
                         $"Joystick Y Axis: {joystickY:F2}";
    }

    private void OnButtonDown(ref bool buttonState) => buttonState = true;
    private void OnButtonUp(ref bool buttonState) => buttonState = false;

    private void OnJoystickXAxisChange(float value) => joystickX = value;
    private void OnJoystickYAxisChange(float value) => joystickY = value;
}
