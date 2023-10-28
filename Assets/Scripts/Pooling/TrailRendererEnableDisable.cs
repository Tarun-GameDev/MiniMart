using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]

public class TrailRendererEnableDisable : MonoBehaviour
{
    [SerializeField] TrailRenderer trailRenderer;

    private void Start()
    {
        if (trailRenderer == null)
            trailRenderer = GetComponent<TrailRenderer>();
    }

    private void OnEnable()
    {
        trailRenderer.enabled = true;
    }

    private void OnDisable()
    {
        trailRenderer.Clear();
        trailRenderer.enabled = false;
    }
}
