using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform rayOrgin;
    [SerializeField] private int maxDist;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private AudioSource audio;
    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(rayOrgin.position, rayOrgin.forward*maxDist, Color.red);
    }

    public void Shoot(bool player) {
        RaycastHit raycastHit;
        audio.Play();
        if (Physics.Raycast(rayOrgin.position, rayOrgin.forward, out raycastHit, maxDist, layerMask)) {
            // Debug.Log(raycastHit.collider.gameObject);
            if (player) {
                Enemy enemy = raycastHit.collider.gameObject.GetComponent<Enemy>();
                if (enemy) enemy.Damage();
            } 
            else {
                Player player1 = raycastHit.collider.gameObject.GetComponent<Player>();
                if (player1) player1.Damage();
            }


            // if (TryGetComponent<Enemy>(out Enemy enemy)) {}

        }
    }
}
