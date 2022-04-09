using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform pTransform;
    float camOffsetZ;
    private Coroutine c_followCamera;
    // Start is called before the first frame update
    void Start()
    {
        //m_player = GameObject.Find("Player");
        camOffsetZ = gameObject.transform.position.z - pTransform.position.z;
        c_followCamera = StartCoroutine(followCamera());
    }

    IEnumerator followCamera()
    {
        while(true)
        {
            Vector3 m_cameraPos = new Vector3(pTransform.position.x, gameObject.transform.position.y, pTransform.position.z + camOffsetZ);

            gameObject.transform.position = m_cameraPos;

            yield return new WaitForSeconds(0.01f);
        }
        
    }
    
}