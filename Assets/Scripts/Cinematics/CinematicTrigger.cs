using UnityEngine;
using UnityEngine.Playables;
using RPG.Control;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool isTriggered = false;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player" && !isTriggered)
            {
                GetComponent<PlayableDirector>().Play();
                isTriggered = true;
            }
        }
    }
}