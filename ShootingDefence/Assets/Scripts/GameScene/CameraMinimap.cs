using UnityEngine;

public class CameraMinimap : MonoBehaviour
{

    [SerializeField]
    private Transform pTransform;

    [SerializeField]
    private float yOffset;

    private void Update()
    {
        Vector3 vectorPlayerPos = pTransform.position;
        vectorPlayerPos.y += yOffset;
        gameObject.transform.position = vectorPlayerPos;
    }

}