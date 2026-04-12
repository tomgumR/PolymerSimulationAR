using UnityEngine;
using Unity.XR.CoreUtils;

public class ScaleXROrigin : MonoBehaviour
{
    public XROrigin xrOrigin;
    public GameObject atmPrefab;
    public Vector3 targetScale = new Vector3(0.5f, 0.5f, 0.5f);

    void Start()
    {
        if (xrOrigin == null)
        {
            xrOrigin = FindObjectOfType<XROrigin>();
        }

        if (xrOrigin != null)
        {
            ScaleContent();
        }
        else
        {
            Debug.LogError("XROrigin is not assigned.");
        }
    }

    void ScaleContent()
    {
        xrOrigin.transform.localScale = targetScale;
    }
}
