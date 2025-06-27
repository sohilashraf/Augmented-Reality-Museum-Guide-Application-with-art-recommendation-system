using UnityEngine;

public class TouchInteraction : MonoBehaviour
{
    private Vector2 prevTouchPos0, prevTouchPos1;

    private float rotationSpeed = 0.2f;
    private float zoomSpeed = 0.01f;

    private float minScale = 0.3f;
    private float maxScale = 3f;

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                float rotX = touch.deltaPosition.x * rotationSpeed;
                transform.Rotate(0, -rotX, 0, Space.World);  
            }
        }

        else if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            Vector2 currDist = touch0.position - touch1.position;

            Vector2 prevDist = (touch0.position - touch0.deltaPosition) - (touch1.position - touch1.deltaPosition);

            float deltaMagnitude = currDist.magnitude - prevDist.magnitude;

            float scaleFactor = 1 + deltaMagnitude * zoomSpeed;
            Vector3 newScale = transform.localScale * scaleFactor;

            newScale = ClampScale(newScale);
            transform.localScale = newScale;
        }
    }

    private Vector3 ClampScale(Vector3 scale)
    {
        float clamped = Mathf.Clamp(scale.x, minScale, maxScale);
        return new Vector3(clamped, clamped, clamped);
    }
}
