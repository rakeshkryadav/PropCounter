using UnityEngine;

public class DragAndDropManager : MonoBehaviour
{
    private new Camera camera;
    [SerializeField] private Transform selectedObject;

    private float lockXPosition;
    private Vector3 offset;

    private void Start()
    {
        // Initialize main camera
        camera = Camera.main;
    }

    private void Update()
    {
        // On Mouse Down
        if (Input.GetMouseButtonDown(0))
        {
            // Trigger a raycast
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.CompareTag("Object"))
                {
                    // Selected Object
                    selectedObject = hit.transform;

                    // Turn off the gravity and on isKinematic
                    selectedObject.GetComponent<Rigidbody>().useGravity = false;
                    selectedObject.GetComponent<Rigidbody>().isKinematic = true;

                    // Lock the x-axis
                    lockXPosition = selectedObject.position.x;

                    // Convert the world position to screen
                    Vector3 screenPosition = camera.WorldToScreenPoint(selectedObject.position);

                    // Convert screen point to world position
                    Vector3 worldPosition = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z));

                    offset = selectedObject.position - worldPosition;
                }
            }
        }

        if (Input.GetMouseButton(0) && selectedObject != null)
        {
            Vector3 screenPosition = camera.WorldToScreenPoint(selectedObject.position);

            Vector3 worldPosition = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z));

            // Calculate the current position
            Vector3 currentPosition = worldPosition + offset;

            // Drag the selected object
            selectedObject.position = new Vector3(lockXPosition, currentPosition.y, currentPosition.z);
        }

        // Mouse Up
        if (Input.GetMouseButtonUp(0))
        {
            // Turn off the gravity
            selectedObject.GetComponent<Rigidbody>().useGravity = true;
            selectedObject.GetComponent<Rigidbody>().isKinematic = false;

            // Remove the selected object
            selectedObject = null;
        }
    }
}
