using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MultiplayerManager : MonoBehaviour
{
    [SerializeField]
    private Transform headTransform;
    [SerializeField]
    private Transform leftHandPrefab;
    [SerializeField]
    private Transform rightHandPrefab;

    private void Start()
    {
        GameObject head = PhotonNetwork.Instantiate("AvinashBody", new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
        GameObject lHand = PhotonNetwork.Instantiate("HandL", new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
        GameObject rHand = PhotonNetwork.Instantiate("HandR", new Vector3(0f, 5f, 0f), Quaternion.identity, 0);

        if (head.GetComponent<Renderer>())
        {
            head.GetComponent<Renderer>().enabled = false;
        }
        foreach (Renderer renderer in head.GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = false;
        }

        Assign(head.transform, lHand.transform, rHand.transform);
    }

    void Assign(Transform head,Transform lHand,Transform rHand)
    {
        head.transform.SetParent(headTransform);
        head.transform.localPosition = Vector3.zero;
        head.transform.localRotation = Quaternion.identity;

        lHand.transform.SetParent(leftHandPrefab);
        lHand.transform.localPosition = Vector3.zero;
        lHand.transform.localRotation = Quaternion.identity;

        rHand.transform.SetParent(rightHandPrefab);
        rHand.transform.localPosition = Vector3.zero;
        rHand.transform.localRotation = Quaternion.identity;
    }
}
