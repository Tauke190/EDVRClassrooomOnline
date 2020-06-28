using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalObjectMover : MonoBehaviour
{
    [HideInInspector]
    public string _name;
    public string mainScene;
    public string tourScene;
    bool pickingUp;
    Transform handToGo;
    Vector3 desiredPos;

    [HideInInspector]
    public GameObject labelObject = null;

    void Start()
    {
        Physics.IgnoreCollision(FindObjectOfType<OVRPlayerController>().GetComponent<CharacterController>(), GetComponent<Collider>());
        _name = mainScene;
    }

    public void PickUp(Transform hand)
    {
        desiredPos = hand.GetComponent<PickUpHand>().holder.position;
        handToGo = hand;
        pickingUp = true;
    }
    void Update()
    {
        if (pickingUp)
        {
            transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * handToGo.GetComponent<PickUpHand>().speed);
            transform.rotation = Quaternion.Slerp(transform.rotation, handToGo.rotation, Time.deltaTime * handToGo.GetComponent<PickUpHand>().speed);
            if (Vector3.Distance(transform.position, desiredPos) < 0.1f)
            {
                transform.SetParent(handToGo.GetComponent<PickUpHand>().holder);
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.Euler(90, 0, 0);
                pickingUp = false;
                if (labelObject)
                {
                    labelObject.SetActive(true);
                }
            }
        }
    }
    public void SceneChanger(string SceneChange)
    {
        //SceneManager.LoadScene(SceneChange);
    }
}
