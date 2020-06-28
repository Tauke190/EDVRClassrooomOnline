using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraCollision : MonoBehaviour
{
    private string name;
    public GameObject pointer;

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log(col);
        if (col.CompareTag("PortalRing"))
        {
           
        }
        if (col.CompareTag("PortalSphere"))
        {
            name = col.gameObject.GetComponent<PortalObjectMover>()._name;
            pointer.SetActive(false);
            GetComponent<OVRScreenFade>().fadeTime = 0.5f;
            GetComponent<OVRScreenFade>().FadeOut();
            Invoke("SceneChanger", 0.5f);
            Destroy(col.gameObject);
        }
        if (col.CompareTag("Tutorial"))
        {

            Destroy(col.gameObject);
        }
    }

    //private void OnTriggerStay(Collider col)
    //{
    //    if (col.CompareTag("PortalSphere"))
    //    {
    //        col.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
    //    }
    //}

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("PortalSphere"))
        {
            Destroy(col.gameObject);
        }
    }

    private void SceneChanger()
    {
        if(name!=null)
        {
            //SteamVR_LoadLevel.Begin(name);
            //SceneManager.LoadScene(name);         

            PhotonNetwork.LoadLevel(name);
        }

    }

    public void SceneChanger(string sceneName)
    {
        if (sceneName != null)
        {
            //SteamVR_LoadLevel.Begin(sceneName);
            //SceneManager.LoadScene(name);
        }

    }

    public void Changer(string sceneName)
    {
       //SteamVR_LoadLevel.Begin(sceneName);
    }
}

    
