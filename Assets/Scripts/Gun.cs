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

    public Damagable Shoot() {
        RaycastHit raycastHit;
        audio.Play();
        
        if (Physics.Raycast(rayOrgin.position, rayOrgin.forward, out raycastHit, maxDist, layerMask)) {
            // if (player) {
            //     // Debug.Log(raycastHit.collider.gameObject);
            //     Enemy enemy = raycastHit.collider.gameObject.GetComponentInParent<Enemy>();
            //     if (enemy) enemy.Damage();
            // } 
            // else {
            //     // Debug.Log(rayOrgin.forward.ToString()+":"+this.GetComponentInParent<Enemy>().transform.forward.ToString());
            //     Player player1 = raycastHit.collider.gameObject.GetComponent<Player>();
            //     // Debug.Log("Hit");
            //     if (player1) player1.Damage();   
            // }

            Damagable obj = raycastHit.collider.gameObject.GetComponentInParent<Damagable>();
            if (obj != null) obj.OnDamage();
            return obj;
            // if (TryGetComponent<Enemy>(out Enemy enemy)) {}

        } else return null;
    }
}
