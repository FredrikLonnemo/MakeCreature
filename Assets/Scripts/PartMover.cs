using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PolygonCollider2D))]
public class PartMover : MonoBehaviour
{
    public Sprite[] Parts;

    public SpriteRenderer Renderer;

    public int PartIndex = 0;

    // The plane the object is currently being dragged on
    private Plane dragPlane;

    // The difference between where the mouse is on the drag plane and 
    // where the origin of the object is on the drag plane
    private Vector3 offset;

    private Camera myMainCamera;
    private void Start()
    {
        myMainCamera = Camera.main; // Camera.main is expensive ; cache it here
    }
    void OnMouseDown()
    {
        dragPlane = new Plane(myMainCamera.transform.forward, gameObject.transform.parent.transform.position);
        Ray camRay = myMainCamera.ScreenPointToRay(Input.mousePosition);

        float planeDist;
        dragPlane.Raycast(camRay, out planeDist);
        offset = gameObject.transform.parent.transform.position - camRay.GetPoint(planeDist);
    }

    void OnMouseDrag()
    {
        Ray camRay = myMainCamera.ScreenPointToRay(Input.mousePosition);

        float planeDist;
        dragPlane.Raycast(camRay, out planeDist);
        gameObject.transform.parent.transform.position = camRay.GetPoint(planeDist) + offset;

        if (Input.GetButtonDown("RotateRight"))
            gameObject.transform.parent.transform.Rotate(0, 0, 45);
        if (Input.GetButtonDown("RotateLeft"))
            gameObject.transform.parent.transform.Rotate(0, 0, -45);

        if (Input.GetButtonDown("SwapPart"))
        {
            PartIndex++;
            if (PartIndex >= Parts.Length)
            {
                PartIndex = 0;
            }
            Renderer.sprite = Parts[PartIndex];

            Destroy(GetComponent<PolygonCollider2D>());
            gameObject.AddComponent<PolygonCollider2D>();
        }
    }
}
