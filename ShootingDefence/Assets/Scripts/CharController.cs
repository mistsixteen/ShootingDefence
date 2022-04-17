using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    private CharacterController charController;
    private Coroutine m_bCoroutine;

    public float pSpeed = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();
        m_bCoroutine = StartCoroutine(CharacterMoveMent());
    }

    IEnumerator CharacterMoveMent()
    {
        while(true)
        {
            float horizon = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 Movement = new Vector3(horizon, 0.0f - transform.position.y, vertical) * pSpeed;

            charController.Move(Movement);

            // 마우스가 향하는 방향으로 캐릭터 회전
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 targetPosition = new Vector3(hit.point.x, 1.0f, hit.point.z);

                Debug.DrawRay(transform.position, transform.position + transform.forward * 1000, Color.red, 0.01f, false);

                Quaternion rotation = Quaternion.LookRotation(targetPosition - transform.position);

                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10.0f);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
