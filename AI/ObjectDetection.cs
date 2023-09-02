using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetection : MonoBehaviour
{
    private HashSet<GameObject> detectedObjects = new HashSet<GameObject>();
    public bool isConfirmationPressed = false;
    public void SetConfirmationPressed(bool value)
    {
        isConfirmationPressed = value;
    }

    void Update()
    {
        // Check for new and removed GameObjects in the scene if confirmation is pressed
        if (isConfirmationPressed)
        {
            CheckForNewObjects();
            CheckForRemovedObjects();
        }
    }

    void CheckForNewObjects()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            // Skip if object is already detected
            if (detectedObjects.Contains(obj))
                continue;

            string objectName = obj.name;

            if (objectName.Contains("Road Straight(Clone)") ||
                objectName.Contains("Road Curve(Clone)") ||
                objectName.Contains("Road Intersection(Clone)") ||
                objectName.Contains("Road_crossroadPath(Clone)") ||
                objectName.Contains("Class(Clone)"))
            {
                Debug.Log("New GameObject detected: " + objectName);
                // Do something when a new GameObject with the specific names appears
                detectedObjects.Add(obj);
            }
        }
        isConfirmationPressed = false;
    }

    void CheckForRemovedObjects()
    {
        List<GameObject> objectsToRemove = new List<GameObject>();

        foreach (GameObject obj in detectedObjects)
        {
            // If the object is null or no longer exists in the scene, mark it for removal
            if (obj == null || !obj.activeInHierarchy)
            {
                objectsToRemove.Add(obj);
            }
        }

        // Remove the objects that were marked for removal
        foreach (GameObject obj in objectsToRemove)
        {
            detectedObjects.Remove(obj);
            Debug.Log("GameObject removed ");
            // Do something when a detected GameObject is removed
        }
        isConfirmationPressed = false;
    }
}
    
