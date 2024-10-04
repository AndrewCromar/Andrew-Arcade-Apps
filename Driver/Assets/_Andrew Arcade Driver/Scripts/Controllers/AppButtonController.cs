using UnityEngine;
using UnityEngine.UI;

public class AppButtonController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image image;
    [SerializeField] private App app;

    [Header("Debug")]
    [SerializeField] private bool setupFinished;

    private void Update()
    {
        if (setupFinished) return;
        if (app == null) return;

        image.sprite = app.appIcon;
        setupFinished = true;
    }

    public void Clicked()
    {
        DriverController.instance.StartApp(app);
    }

    public void SetApp(App _app) { app = _app; }
}