using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class RoomTrigger : AudioPlayer
{
    private void OnTriggerEnter(Collider other)
    {
        PlayAudio();
    }
}
