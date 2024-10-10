using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollToSelectedImproved : MonoBehaviour
{
    public ScrollRect scrollRect;     // The ScrollRect component
    public RectTransform content;     // The content of the ScrollView
    public RectTransform viewport;    // The Viewport of the ScrollView

    private RectTransform selectedButton; // The currently selected button

    void Update()
    {
        // Get the currently selected GameObject
        GameObject currentSelected = EventSystem.current.currentSelectedGameObject;

        // Check if it's a button and if it's different from the last selected button
        if (currentSelected != null && currentSelected.GetComponent<Button>() != null)
        {
            RectTransform selectedRectTransform = currentSelected.GetComponent<RectTransform>();

            // Scroll only if a new button is selected or it's off-screen
            if (selectedButton != selectedRectTransform)
            {
                selectedButton = selectedRectTransform;
                ScrollToEnsureButtonIsVisible(selectedButton);
            }
        }
    }

    void ScrollToEnsureButtonIsVisible(RectTransform button)
    {
        // Calculate the world position of the selected button
        Vector3[] buttonCorners = new Vector3[4];
        button.GetWorldCorners(buttonCorners);

        // Get the world corners of the viewport
        Vector3[] viewportCorners = new Vector3[4];
        viewport.GetWorldCorners(viewportCorners);

        // If the button is above or below the viewport, scroll accordingly
        float buttonTop = buttonCorners[1].y; // Top-left corner in world space
        float buttonBottom = buttonCorners[0].y; // Bottom-left corner in world space
        float viewportTop = viewportCorners[1].y; // Top-left corner of viewport in world space
        float viewportBottom = viewportCorners[0].y; // Bottom-left corner of viewport in world space

        // Scroll down if the button is below the viewport
        if (buttonBottom < viewportBottom)
        {
            float scrollAmount = viewportBottom - buttonBottom;
            scrollRect.content.localPosition += new Vector3(0, scrollAmount);
        }
        // Scroll up if the button is above the viewport
        else if (buttonTop > viewportTop)
        {
            float scrollAmount = buttonTop - viewportTop;
            scrollRect.content.localPosition -= new Vector3(0, scrollAmount);
        }
    }
}