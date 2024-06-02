using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private Player player;
    private float footstepTime;
    private float footstepTimerMax = 0.1f;
    float volume = 1f;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        footstepTime -= Time.deltaTime;
        if (footstepTime < 0f)
        {
            footstepTime = footstepTimerMax;

            if (player.IsWalking())
            {
                SoundManager.Instance.PlayFootstepSound(player.transform.position, volume);
            }
        }
    }
}
