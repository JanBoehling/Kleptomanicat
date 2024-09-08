using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource source;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponent <PlayerMovement>();
        if (!player)
            return;

        source.Play();
    }
}
