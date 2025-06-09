using UnityEngine;

public class PinchToZoom : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 initialScale;

    private Vector3 lastScale;
    private float rotationSpeed = 0.2f;
    private float zoomSpeed = 0.005f;

    public float minScale = 1000f;
    public float maxScale = 5000f;

    void Start()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
        initialScale = transform.localScale;
        lastScale = initialScale;
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                float deltaX = touch.deltaPosition.x;
                transform.Rotate(0f, -deltaX * rotationSpeed, 0f, Space.World);
            }
        }
        else if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrev = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrev = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrev - touchOnePrev).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = touchDeltaMag - prevTouchDeltaMag;

            float scaleFactor = 1 + deltaMagnitudeDiff * zoomSpeed;
            Vector3 newScale = lastScale * scaleFactor;

            float clampedX = Mathf.Clamp(newScale.x, minScale, maxScale);
            newScale = new Vector3(clampedX, clampedX, clampedX);

            transform.localScale = newScale;
            lastScale = newScale;
        }
    }

    public void ResetTransform()
    {
        transform.localPosition = initialPosition;
        transform.localRotation = initialRotation;
        transform.localScale = initialScale;
        lastScale = initialScale;
    }
}
