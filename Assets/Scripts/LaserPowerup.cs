using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPowerup : MonoBehaviour
{
    public float _laserBeamLength;
    private LineRenderer _lineRenderer;
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 endPosition = transform.position + (transform.up * _laserBeamLength);
        _lineRenderer.SetPositions(new Vector3[] { transform.position, endPosition });
    }
}
