// These 'using' statements import code libraries so we can use their features
using UnityEngine;  // Core Unity functionality (MonoBehaviour, Debug, etc.)
using FMODUnity;    // FMOD audio engine integration for Unity

// This class inherits from MonoBehaviour, which means it can be
// attached to a GameObject in the Unity scene as a component.
public class FMODAnimationEventTriggers : MonoBehaviour
{
    // [System.Serializable] makes this struct visible in the Unity Inspector,
    // so you can fill in the values directly in the editor without writing code.
    [System.Serializable]
    public struct AnimationEventTriggers
    {
        // A string label that matches the event name you set on the animation timeline.
        // This is how we connect an animation event to the right sound.
        public string animationTimelineEventString;

        // A reference to an FMOD event (a sound). You assign this in the Inspector
        // by browsing your FMOD project's events.
        public EventReference fmodEvent;
    }

    // An array of animation-to-sound mappings. Each entry pairs an animation
    // event string with an FMOD sound. 'public' makes it show up in the Inspector
    // so you can add as many entries as you need.
    public AnimationEventTriggers[] animationEventTriggers;

    // This method gets called by Unity's animation system. When you add an
    // Animation Event to a timeline, you point it at this method and pass
    // a string that identifies which sound to play.
    public void TriggerFMODEvent(string eventString)
    {
        // Loop through every entry in our array looking for a matching string
        for (int i = 0; i < animationEventTriggers.Length; i++)
        {
            // Check if this entry's string matches the one from the animation event
            if (animationEventTriggers[i].animationTimelineEventString == eventString)
            {
                // Match found — play the FMOD sound at this GameObject's position.
                // PlayOneShot is fire-and-forget: it plays the sound once and we
                // don't need to manage it afterward.
                RuntimeManager.PlayOneShot(animationEventTriggers[i].fmodEvent, transform.position);

                // 'return' exits the method immediately. We found our match,
                // so there's no reason to keep looping.
                return;
            }
        }

        // If we get here, the loop finished without finding a match.
        // Log a warning so we know something is misconfigured.
        // The '$' before the string lets us insert variables with {curly braces}.
        Debug.LogWarning($"No FMOD event found for animation event string: {eventString}");
    }
}
