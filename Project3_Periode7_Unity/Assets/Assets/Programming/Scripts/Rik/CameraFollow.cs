using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;
    public Transform target;
    public float smoothTime = .5f;
    public Vector3 offset;
    private Vector3 velocity;
    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float zoomLimiter = 50f;

    private void Update()
    {
        Move();
        instance = this;
    }

    private void Move()
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = new Vector3(
            Mathf.Clamp(centerPoint.x + offset.x, -1.5f, 0.4f),
            Mathf.Clamp(centerPoint.y + offset.y, 8.95f, 32.9f),
            centerPoint.z + offset.z);

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    private Vector3 GetCenterPoint()
    {
        var bounds = new Bounds(target.position, Vector3.zero);

        bounds.Encapsulate(target.position);

        return bounds.center;
    }

    public IEnumerator Shake(float magnitude, float duration)
    {
        while (true)
        {
            Vector3 currentPos = transform.localPosition;
            Vector3 newPos = new Vector3(currentPos.x + Random.Range(-magnitude, magnitude), currentPos.y + Random.Range(-magnitude, magnitude), transform.localPosition.z);

            transform.localPosition = Vector3.Lerp(currentPos, newPos, 0.5f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
}