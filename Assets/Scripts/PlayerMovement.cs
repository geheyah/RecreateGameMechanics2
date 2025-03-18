using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Transform controller;
    public Transform head;
    public Camera cam;
    public Vector3 mousepos;
    public TMP_Text text;

    public Transform pickupObject;
    private float playerspeed = 10f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        text.enabled = false;

        mousepos.x += Input.GetAxis("Mouse X");
        mousepos.y += Input.GetAxis("Mouse Y");
        
        Vector3 mouseworldposition = cam.ScreenToWorldPoint(Input.mousePosition);

        head.transform.rotation = Quaternion.Euler(-mousepos.y, mousepos.x, 0);
        controller.transform.rotation = Quaternion.Euler(0, mousepos.x, 0);
        
        var ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        
        if (Physics.Raycast(ray, out hit))
        {
            var movable = hit.transform;
            
            if (movable.CompareTag("Pickup"))
            {
                var pickup = movable.transform;
                text.enabled = true;
                text.text = "Pick Up";
                
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    pickupObject = pickup;
                } else if (Input.GetKey(KeyCode.Mouse0))
                {
                    if (pickupObject != null)
                    {
                        text.text = "Throw";
                        pickup.transform.position = mouseworldposition + controller.transform.forward;
                    }
                }
                else if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    pickupObject = null;
                }
            }
        }
        
        //Pickup object
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (pickupObject != null)
            {
                pickupObject.GetComponent<Rigidbody>().AddForce((head.transform.forward * 10f), ForceMode.Impulse);
                pickupObject = null;
            }
        }
        //Movement
        if (Input.GetKey(KeyCode.W))
        {
            controller.transform.position = Vector3.Lerp(controller.transform.position, controller.transform.position + this.transform.forward, Time.deltaTime * playerspeed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            controller.transform.position = Vector3.Lerp(controller.transform.position, controller.transform.position - this.transform.forward, Time.deltaTime * playerspeed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            controller.transform.position = Vector3.Lerp(controller.transform.position, controller.transform.position - this.transform.right, Time.deltaTime * playerspeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            controller.transform.position = Vector3.Lerp(controller.transform.position, controller.transform.position + this.transform.right, Time.deltaTime * playerspeed);
        }
    }
}
