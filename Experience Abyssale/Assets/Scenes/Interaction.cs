using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    [Header("Variables")]
    public float interactDistance = 2f;
    public TextMeshProUGUI interactibleText;
    [Header("GameObject")]
    public GameObject interactMessage, playerController;
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip doorOpen, doorClose;
    [Header("Doors and Buttons")]
    public GameObject[] doors;
    public GameObject[] doorButtons;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

        Ray ray = new Ray(playerController.transform.position, playerController.transform.forward);
        RaycastHit hitInfo;
        Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.red);
        if (Physics.Raycast(ray, out hitInfo, interactDistance) && hitInfo.collider.CompareTag("Interact"))
        {
            if (hitInfo.distance <= interactDistance)
            {
                if (!interactMessage.activeSelf)
                {
                    interactMessage.SetActive(true);
                    interactibleText.text = "Press E to interact.";
                }
            }
        }
        else
        {
            if (interactMessage.activeSelf)
                interactMessage.SetActive(false);
        }

        // Commande pour intéragir
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (interactMessage.activeSelf)
            {
                for (int i = 0; i < doorButtons.Length; i++)
                {
                    if (hitInfo.collider.gameObject.name == doorButtons[i].name)
                    {
                        if (doors[i] != null)
                        {
                            if(doors[i].activeSelf)
                            {
                                doors[i].SetActive(false);
                                audioSource.PlayOneShot(doorOpen);
                            }
                            else if(!doors[i].activeSelf)
                            {
                                doors[i].SetActive(true);
                                audioSource.PlayOneShot(doorClose);
                            }
                        }
                        break;
                    }
                }
            }
        }
    }
}
