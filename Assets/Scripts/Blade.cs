using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    #region Dependencies
    private Collider bladeCollider;
    private Camera mainCamera;
    private TrailRenderer bladeTrail;
    private bool isSlicing;
    public Vector3 directioin { get; private set; }
    public float minSlicingVelocity = 0.01f;
    public float slicedForce = 5f;
    private SoundManager soundManager;
    #endregion

    #region MonoBehaviour methods
    private void Awake()
    {
        bladeCollider = GetComponent<Collider>();
        mainCamera = Camera.main;
        bladeTrail = GetComponentInChildren<TrailRenderer>();
        soundManager = FindObjectOfType<SoundManager>();
    }
    private void OnEnable()
    {
        StopSlicing();
    }
    void Update()
    {
        // PCInput();
        MobileInput();
    }
  
    private void OnDisable()
    {
        StopSlicing();
    }
    #endregion

    #region Functionality Methods

    private void PCInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSlicing(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlicing();
        }
        else if (isSlicing)
        {
            ContinueSlicing(Input.mousePosition);
        }
    }
    private void MobileInput()
    {
        if (Input.touchCount > 0)
    {
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            StartSlicing(touch.position);
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            StopSlicing();
        }
        else if (isSlicing)
        {
            ContinueSlicing(touch.position);
        }
    }
    }
    private void ContinueSlicing(Vector2 touchPosition)
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(touchPosition);
        newPosition.z = 0f;
        directioin = newPosition - transform.position;
        float velocity = directioin.magnitude / Time.deltaTime;
        bladeCollider.enabled = velocity > minSlicingVelocity;
        transform.position = newPosition;
    }

    private void StopSlicing()
    {
        isSlicing = false;
        bladeCollider.enabled = false;
        bladeTrail.enabled = false;
    }

    private void StartSlicing(Vector2 touchPosition)
    {
        soundManager.PlaySFX("Empty");
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(touchPosition);
        newPosition.z = 0f;
        transform.position = newPosition;
        isSlicing = true;
        bladeCollider.enabled = true;
        bladeTrail.enabled = true;
        bladeTrail.Clear();
    }
    #endregion
}
