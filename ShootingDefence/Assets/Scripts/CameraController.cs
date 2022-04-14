using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform pTransform;
    public float minYOffset = 10.0f;
    public float maxYOffset = 20.0f;
    private float currentYOffset = 18.0f;
    private float camOffsetZ;

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
            currentYOffset += Input.mouseScrollDelta.y;
            if (currentYOffset < minYOffset)
                currentYOffset = minYOffset;
            if (currentYOffset > maxYOffset)
                currentYOffset = maxYOffset;

            Vector3 m_cameraPos = new Vector3(pTransform.position.x, currentYOffset, pTransform.position.z + camOffsetZ);

            gameObject.transform.position = m_cameraPos;

            yield return new WaitForSeconds(0.01f);
        }
        
    }
}