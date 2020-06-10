using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{

    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, camera, player;
    private float maxDistance = 100f;
    private SpringJoint joint;
    private bool justGrappled;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }
    }
    // Called after update
    private void LateUpdate()
    {
        DrawRope();
    }

    void StartGrapple()
    {

        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable))
        {
            justGrappled = true;

            player.GetComponent<Rigidbody>().isKinematic = false;
            player.GetComponent<CharacterController>().enabled = false;
            var rb = player.GetComponent<Rigidbody>();
            rb.AddForce(player.transform.forward * 30, ForceMode.Impulse);

            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);


            //The distance grapple will try to keep form grapple point.
            joint.maxDistance = distanceFromPoint = 0.8f;
            joint.minDistance = distanceFromPoint = 0.25f;

            //Change these values to make different stuff
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
        }

    }
    void StopGrapple()
    {
        if (justGrappled)
        {
            player.GetComponent<PlayerMovement>().move = player.transform.forward * 2;
            player.GetComponent<PlayerMovement>().move.y =3;
            lr.positionCount = 0;
            Destroy(joint);
            player.GetComponent<Rigidbody>().isKinematic = true;
            player.GetComponent<CharacterController>().enabled = true;
            player.GetComponent<PlayerMovement>().fallDamageTriggered = false;
            player.GetComponent<PlayerMovement>().gravityVelocity = new Vector3(0, 0, 0);
            justGrappled = false;
        }
    }

    void DrawRope()
    {
        //If not grappling, don't draw rope
        if (!joint) return;
        {
            lr.SetPosition(0, gunTip.position);
            lr.SetPosition(1, grapplePoint);

        }
    }
    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }

}
