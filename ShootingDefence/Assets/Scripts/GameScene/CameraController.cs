using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform pTransform;
    public Camera cam;
    
    public float minYOffset = 10.0f;
    public float maxYOffset = 20.0f;
    private float currentYOffset = 18.0f;

    public float threshold = 5.0f;


    private Coroutine c_followCamera;
    // Start is called before the first frame update
    void Start()
    {
        c_followCamera = StartCoroutine(CameraRoutine());
    }

    IEnumerator CameraRoutine()
    {
        Vector3 screenPosition = new Vector3();
        Vector3 targetPosition;

        while(true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //마우스 포인터의 위치를 Raycast하여, 마우스 위치 - 캐릭터 위치의 중간값으로 카메라 이동
            if (Physics.Raycast(ray, out hit))
            {
                screenPosition = new Vector3(hit.point.x, 0.0f, hit.point.z);
                targetPosition = (pTransform.position + screenPosition) / 2.0f;

                targetPosition.x = Mathf.Clamp(targetPosition.x, pTransform.position.x - threshold, pTransform.position.x + threshold);
                targetPosition.z = Mathf.Clamp(targetPosition.z, pTransform.position.z - threshold, pTransform.position.z + threshold);
            }
            else // Raycast 실패시 Camera 위치 유지
                targetPosition = transform.position;
            //카메라 높이는 Wheel로 수정
            currentYOffset += Input.mouseScrollDelta.y;
            currentYOffset = Mathf.Clamp(currentYOffset, minYOffset, maxYOffset);
            targetPosition.y = currentYOffset;

            gameObject.transform.position = targetPosition;

            yield return new WaitForSeconds(0.01f);
        }
        
    }
}