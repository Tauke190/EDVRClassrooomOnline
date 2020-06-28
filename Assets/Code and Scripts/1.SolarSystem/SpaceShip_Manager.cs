using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject spaceShip;

    public Camera cam;
    public Transform Player;
    public static bool isMoving;

    public static SpaceShip_Manager instance;

    [SerializeField]
    private Vector3 offest;

    private GameObject currentSpaceShip;

    private Vector3 destination;

    [SerializeField]
    private float closetDistance = 15f;

    [SerializeField]
    private float minimumDistance = 15f;

    [SerializeField]
    private float speed = 5f;

    public GameObject WrapDrive;

   

    float currentSpeed = 0;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;

        Cursor.visible = false;
        WrapDrive.SetActive(false);
    }

    public void SpawnShapeShip(Vector3 _destination)
    {
        if (isMoving)
            return;
        destination = _destination;
        currentSpaceShip = Instantiate(spaceShip, cam.transform.position + offest, cam.transform.rotation) as GameObject;
        WrapDrive.SetActive(true);
        currentSpaceShip.transform.forward = (_destination - currentSpaceShip.transform.position).normalized;
        isMoving = true;
    }

    private void Update()
    {     
        if (isMoving)
        {
            currentSpaceShip.transform.Translate(transform.forward * speed);
            if (Vector3.Distance(currentSpaceShip.transform.position, destination) <= closetDistance)
            {
                isMoving = false;
                Destroy(currentSpaceShip);
                WrapDrive.SetActive(false);
            }

            Player.transform.position = Vector3.Lerp(Player.transform.position,currentSpaceShip.transform.position - offest, 0.2f);
            return;
        }
    }

    void CheckPlanet()
    {
        Vector3 pos = new Vector3(cam.pixelWidth/2, cam.pixelWidth/2, 0);
        Ray ray = cam.ScreenPointToRay(pos);
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position,cam.transform.forward,out hit))
        {
            if (hit.transform.tag == "Planet")
            {
                SpawnShapeShip(hit.transform.position);
            }
        }
    }
}
