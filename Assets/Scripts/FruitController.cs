using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour
{
    #region Dependencies
    [SerializeField] private GameObject wholeFruit;
    [SerializeField] private GameObject slicedFruit;
    [SerializeField] private int fruitPoint;
    private Rigidbody fruitRigidbody;
    private Collider fruitCollider;
    private ParticleSystem fruitParticleEffect;
    private SoundManager soundManager;
    private UIController uiController;

    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        fruitRigidbody = GetComponent<Rigidbody>();
        fruitCollider = GetComponent<Collider>();
        fruitParticleEffect=GetComponentInChildren<ParticleSystem>();
        soundManager = FindObjectOfType<SoundManager>();
        uiController = FindObjectOfType<UIController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();
            Sliced(blade.directioin, blade.transform.position, blade.slicedForce);
        }
       
    }
    #endregion

    #region Functionality Methods
    private void Sliced(Vector3 direction, Vector3 position, float force)
    {
        soundManager.PlaySFX("FruitSliced");
        uiController.IncreaseScore(fruitPoint);
        wholeFruit.SetActive(false);
        slicedFruit.SetActive(true);
        fruitCollider.enabled = false;
        fruitParticleEffect.Play();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        slicedFruit.transform.rotation = Quaternion.Euler(0, 0, angle);
        Rigidbody[] slices = slicedFruit.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody slice in slices)
        {
            slice.velocity = fruitRigidbody.velocity;
            slice.AddForceAtPosition(direction*force,position,ForceMode.Impulse);
        }
    }
    #endregion
}
