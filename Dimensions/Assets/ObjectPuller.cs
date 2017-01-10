using UnityEngine;
using System.Collections;

public class ObjectPuller : MonoBehaviour {
    public float pullRadius = 2;
    public float pullForce = 1;

    public void FixedUpdate()
    {
        foreach (var collider in Physics.OverlapSphere(transform.position, pullRadius)){
            // calculate direction from target to me
            Vector3 forceDirection = transform.position - collider.transform.position;

            // apply force on target towards me
            GetComponent<Rigidbody>().AddForce(forceDirection.normalized * pullForce * Time.fixedDeltaTime);
        }
    }
}
