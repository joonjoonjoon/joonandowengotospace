using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorRoller : MonoBehaviour {

    private Rigidbody rb;
    private Conveyor conveyor;

    void Start()
    {
        var newGO = new GameObject();
        newGO.transform.parent = transform.parent;
        newGO.transform.localPosition = transform.localPosition;
        newGO.transform.localRotation = transform.localRotation;
        newGO.transform.localScale = transform.localScale;
        newGO.name = "Roller";
        var newRB = newGO.AddComponent<Rigidbody>();
        newRB.isKinematic = true;

        transform.parent = newGO.transform;
        transform.ResetLocal();
        rb = gameObject.AddComponent<Rigidbody>();
        var hj = gameObject.AddComponent<HingeJoint>();
        hj.anchor = Vector3.zero;
        hj.connectedBody = newRB;
        rb.constraints = RigidbodyConstraints.FreezePosition;
        rb.angularDrag = 1;
        conveyor = GetComponentInParent<Conveyor>();
        var mc = gameObject.AddComponent<MeshCollider>();
        mc.convex = true;
    }

    void FixedUpdate()
    {
        rb.AddRelativeTorque(conveyor.force, 0, 0);
    }

}
