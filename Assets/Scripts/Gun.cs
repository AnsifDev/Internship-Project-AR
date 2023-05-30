using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform rayOrgin;
    [SerializeField] private int maxDist;
    [SerializeField] private LayerMask layerMask;

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(rayOrgin.position, rayOrgin.forward*maxDist, Color.red);
    }

    public void shoot() {
        RaycastHit raycastHit;
        if (Physics.Raycast(rayOrgin.position, rayOrgin.forward, out raycastHit, maxDist, layerMask)) {
            Debug.Log(raycastHit.collider.gameObject);
        }
    }
}
