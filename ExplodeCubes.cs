using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for UI components

public class ExplodeCubes : MonoBehaviour
{
    public Slider forceSlider; // Drag your slider here from the inspector
    public float explosionForce = 500f; // Default force
    public float explosionRadius = 5f;

    void Start()
    {
        // Assign the slider event
        if (forceSlider != null)
        {
            forceSlider.onValueChanged.AddListener(delegate { explosionForce = forceSlider.value; });
        }
    }

    void Update()
    {
        // Your existing logic for handling explosions
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Collider[] colliders = Physics.OverlapSphere(hit.point, explosionRadius);
                foreach (Collider hitCollider in colliders)
                {
                    Rigidbody rb = hitCollider.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.AddExplosionForce(explosionForce, hit.point, explosionRadius);
                    }
                }
            }
        }
    }
}
