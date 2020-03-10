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

        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    private Vector3 GetCenterPoint()
    {
        var bounds = new Bounds(target.position, Vector3.zero);

        bounds.Encapsulate(target.position);

        return bounds.center;
    }

    public IEnumerator Shake()
    {
        yield return new WaitForSeconds(0.1f);

        transform.localPosition += Random.insideUnitSphere * 2f;
        Debug.LogError("Shakey");
    }
}