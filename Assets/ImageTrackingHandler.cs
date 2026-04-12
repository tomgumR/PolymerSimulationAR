using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTrackingHandler : MonoBehaviour
{
    private ARTrackedImageManager trackedImageManager;

    // Reference to the ATM prefab
    public GameObject atmPrefab; // This should now be the instance in the scene
    public GameObject anotherPrefab; // Another prefab for a different tracked image

    void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();

        // Ensure the prefabs are initially inactive
        atmPrefab.SetActive(false);
        anotherPrefab.SetActive(false);
    }

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            UpdateARObject(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            UpdateARObject(trackedImage);
        }
    }

    void UpdateARObject(ARTrackedImage trackedImage)
    {
        // Use the name of the reference image to identify which objects to activate
        if (trackedImage.referenceImage.name == "YourMarkerImageName")
        {
            Vector3 position = trackedImage.transform.position;
            Quaternion rotation = trackedImage.transform.rotation;
            Debug.Log("Image position: " + position);

            // Activate and update the ATM prefab's position and rotation
            atmPrefab.SetActive(true);
            atmPrefab.transform.position = position;
            atmPrefab.transform.rotation = rotation;
            Debug.Log("Activated and updated ATM Prefab position and rotation");
        }
        else if (trackedImage.referenceImage.name == "AnotherMarkerImageName")
        {
            Vector3 position = trackedImage.transform.position;
            Quaternion rotation = trackedImage.transform.rotation;
            Debug.Log("Image position: " + position);

            // Activate and update the another prefab's position and rotation
            anotherPrefab.SetActive(true);
            anotherPrefab.transform.position = position;
            anotherPrefab.transform.rotation = rotation;
            Debug.Log("Activated and updated Another Prefab position and rotation");
        }
    }
}
