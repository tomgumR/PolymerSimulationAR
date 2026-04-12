
using UnityEngine;
using Unity.XR.CoreUtils;

public class PinchZoom : MonoBehaviour
{
    public XROrigin xrOrigin;  // The XR Origin object to scale
    public float zoomSpeed = 0.1f;   // Speed of zooming
    private Vector2 lastTouchPos1;
    private Vector2 lastTouchPos2;

    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            // Check if one of the touches just began
            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                lastTouchPos1 = touch1.position;
                lastTouchPos2 = touch2.position;
            }
            else
            {
                // Calculate the distance between the touches in each frame
                float previousDistance = Vector2.Distance(lastTouchPos1, lastTouchPos2);
                float currentDistance = Vector2.Distance(touch1.position, touch2.position);

                // Calculate the scale factor based on the distance change
                float scaleFactor = (currentDistance - previousDistance) * zoomSpeed;

                // Apply the scaling to the XROrigin
                xrOrigin.transform.localScale += new Vector3(scaleFactor, scaleFactor, scaleFactor);

                // Clamp the scale to prevent it from getting too small or too large
                xrOrigin.transform.localScale = new Vector3(
                    Mathf.Clamp(xrOrigin.transform.localScale.x, 0.1f, 5f),
                    Mathf.Clamp(xrOrigin.transform.localScale.y, 0.1f, 5f),
                    Mathf.Clamp(xrOrigin.transform.localScale.z, 0.1f, 5f)
                );

                // Update the last positions
                lastTouchPos1 = touch1.position;
                lastTouchPos2 = touch2.position;
            }
        }
    }
}
