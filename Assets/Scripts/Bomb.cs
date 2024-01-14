using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    #region Dependencies
    [SerializeField] private GameObject bombEffect;
    private UIController uiController;
    private SoundManager soundManager;
    #endregion
    #region MonoBehaviour Methods
    private void Awake()
    {
        uiController=FindObjectOfType<UIController>();
        soundManager = FindObjectOfType<SoundManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bombEffect.SetActive(true);
            soundManager.PlaySFX("BombExplode");
            uiController.Explode();
        }
    }
    #endregion
}
