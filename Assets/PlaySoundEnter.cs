using UnityEngine;

public class PlaySoundEnter : StateMachineBehaviour
{
    [SerializeField] private SoundType sound;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SoundManager.Playsound(sound);
    }
    
}
