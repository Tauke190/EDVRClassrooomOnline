using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bone : MonoBehaviour
{
    public int boneIndex;
    [Range(0.02f, 0.2f)]
    public float minimumDis = 0.1f;
    public bool isVertebraeBone;

    private bool pickingUp;
    private Transform handToGo;
    private Rigidbody rb;
    private Vector3 desiredPos;
    private Collider collisionCollider;
    private Vector3 placingPos;
    [HideInInspector]
    public Target currentTarget;
    private Transform parentTransform;

    public bool isTour = false;
    public bool addExplosion = false;

    private void Start()
    {
        gameObject.tag = "Bone";
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        Invoke("DisbaleKinematics", 1f);

        foreach (Collider item in GetComponents<Collider>())
        {
            if (!item.isTrigger)
            {
                collisionCollider = item;
                Physics.IgnoreCollision(item, FindObjectOfType<OVRPlayerController>().GetComponent<Collider>());
            }
        }
        
        parentTransform = transform.parent;

        foreach (Target item in FindObjectsOfType<Target>())
        {
            if (item.index == boneIndex)
            {
                if (item.GetComponent<MeshRenderer>())
                {
                    item.GetComponent<MeshRenderer>().material = Refrences.instance.boneNormalMaterial;
                    item.GetComponent<MeshRenderer>().enabled = false;
                }
                else
                {
                    foreach (MeshRenderer mr in item.GetComponentsInChildren<MeshRenderer>())
                    {
                        mr.material = Refrences.instance.boneNormalMaterial;
                        mr.enabled = false;
                    }
                }
            }
        }
    }

    private void DisbaleKinematics()
    {
        rb.isKinematic = false;
        if (isTour)
        {
            if (addExplosion)
            {
                rb.AddExplosionForce(200f, transform.position, 10f);
            }
            StartCoroutine(Reaggrange());
        }
    }

    IEnumerator Reaggrange()
    {
        foreach (Target item in FindObjectsOfType<Target>())
        {
            if (item.index == boneIndex)
            {
                if (item.GetComponent<MeshRenderer>())
                {
                    item.GetComponent<MeshRenderer>().enabled = true;
                }
                else
                {
                    foreach (MeshRenderer mr in item.GetComponentsInChildren<MeshRenderer>())
                    {
                        mr.enabled = true;
                    }
                }
            }
        }

        yield return new WaitForSeconds(Random.Range(2f, 9f));

        rb.isKinematic = true;
        bool going = true;

        Transform destinationTransform = null;

        foreach (Target item in FindObjectsOfType<Target>())
        {
            if (item.index == boneIndex)
            {
                destinationTransform = item.transform;
            }
        }

        while (going)
        {
            transform.position = Vector3.Lerp(transform.position, destinationTransform.position, Time.deltaTime * 2);
            transform.rotation = Quaternion.Slerp(transform.rotation, destinationTransform.rotation, Time.deltaTime * 2);
            if (Vector3.Distance(transform.position, destinationTransform.position) < 0.01f)
            {
                transform.position = destinationTransform.position;
                transform.rotation = destinationTransform.rotation;
                if (destinationTransform.GetComponent<MeshRenderer>())
                {
                    destinationTransform.GetComponent<MeshRenderer>().enabled = false;
                }
                else
                {
                    foreach (MeshRenderer mr in destinationTransform.transform.GetComponentsInChildren<MeshRenderer>())
                    {
                        mr.enabled = false;
                    }
                }
                going = false;
            }
            yield return null;
        }

    }

    public void PickUp(Transform hand)
    {
        desiredPos = hand.GetComponent<PickUpHand>().holder.position;
        handToGo = hand;
        pickingUp = true;
        rb.isKinematic = true;
    }

    private void Update()
    {
        if(pickingUp)
        {
            transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * handToGo.GetComponent<PickUpHand>().speed);
            transform.rotation = Quaternion.Slerp(transform.rotation, handToGo.rotation, Time.deltaTime * handToGo.GetComponent<PickUpHand>().speed);
            if (Vector3.Distance(transform.position, desiredPos) < 0.1f)
            {
                transform.SetParent(handToGo.GetComponent<PickUpHand>().holder);
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;
                foreach (Target item in FindObjectsOfType<Target>())
                {
                    if(item.index == boneIndex)
                    {
                        if (item.GetComponent<MeshRenderer>())
                        {
                            item.GetComponent<MeshRenderer>().material = Refrences.instance.boneHighLightMaterial;
                        }
                        else
                        {
                            foreach (MeshRenderer mr in item.GetComponentsInChildren<MeshRenderer>())
                            {
                                mr.material = Refrences.instance.boneHighLightMaterial;
                            }
                        }
                    }
                }
                pickingUp = false;
            }
        }
    }

    public void Drop()
    {
        if (!handToGo)
            return;
        pickingUp = false;
        handToGo.GetComponent<PickUpHand>().currentBone = null;
        transform.SetParent(parentTransform);
        handToGo = null;
        if (currentTarget)
        {
            if (currentTarget.GetComponent<MeshRenderer>())
            {
                currentTarget.GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                foreach (MeshRenderer mr in currentTarget.transform.GetComponentsInChildren<MeshRenderer>())
                {
                    mr.enabled = true;
                }
            }
            currentTarget = null;
        }
        bool placed = false;

        foreach (Target item in FindObjectsOfType<Target>())
        {
            if(item.index == boneIndex)
            {
                foreach (Target target in FindObjectsOfType<Target>())
                {
                    if (target.GetComponent<MeshRenderer>())
                    {
                        target.GetComponent<MeshRenderer>().material = Refrences.instance.boneNormalMaterial;
                    }
                    else
                    {
                        foreach (MeshRenderer mr in target.GetComponentsInChildren<MeshRenderer>())
                        {
                            mr.material = Refrences.instance.boneNormalMaterial;
                        }
                    }
                }

                if (Vector3.Distance(item.transform.position,transform.position) <= minimumDis && Quaternion.Angle(transform.rotation,item.transform.rotation)<= 45f)
                {
                    transform.position = item.transform.position;
                    transform.rotation = item.transform.rotation;
                    placed = true;
                    if (item.GetComponent<MeshRenderer>())
                    {
                        item.GetComponent<MeshRenderer>().enabled = false;
                    }
                    else
                    {
                        foreach (MeshRenderer mr in item.transform.GetComponentsInChildren<MeshRenderer>())
                        {
                            mr.enabled = false;
                        }
                    }
                    currentTarget = item;
                }
            }
        }

        if (!placed)
        {
            rb.isKinematic = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Target>() || other.tag == "Bone")
        {
            Physics.IgnoreCollision(collisionCollider, other);
        }
    }
}
