using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimatorHandler
{
    public enum TrigerAnimationsToPlay
    {
        Idle, Attack, SkilCast,Die
    }

    public enum BoolAnimationsToPlay
    {
        Walk
    }

    public enum AnimationWithSpeedModifiers
    {
        Walk
    }

    public class EnemyAnimatorHandler : MonoBehaviour
    {
        private static readonly int IDLE_HASH = Animator.StringToHash("Idle");
        private static readonly int ATTACK_HASH = Animator.StringToHash("Attack");
        private static readonly int SKIL_CAST_HASH = Animator.StringToHash("SkilCast");
        private static readonly int DIE_HASH = Animator.StringToHash("Die");
        private static readonly int WALK_HASH = Animator.StringToHash("Walk");

        private static readonly int WALK_SPEED_MODIFIER_HASH = Animator.StringToHash("");

        private Animator myAnimator;
        private void Awake()
        {
            myAnimator = GetComponentInChildren<Animator>();
        }

        public void PlayATriggerAnimation(TrigerAnimationsToPlay animationToPlay)
        {
            switch (animationToPlay)
            {
                case TrigerAnimationsToPlay.Idle:
                    myAnimator.SetTrigger(IDLE_HASH);
                    break;
                case TrigerAnimationsToPlay.Attack:
                    myAnimator.SetTrigger(ATTACK_HASH);
                    break;
                case TrigerAnimationsToPlay.SkilCast:
                    myAnimator.SetTrigger(SKIL_CAST_HASH);
                    break;
                case TrigerAnimationsToPlay.Die:
                    myAnimator.SetTrigger(DIE_HASH);
                    break;
                default:
                    break;
            }
        }

        public void PlayABoolAnimation(BoolAnimationsToPlay animationToPlay, bool state)
        {
            switch (animationToPlay)
            {
                case BoolAnimationsToPlay.Walk:
                    myAnimator.SetBool(WALK_HASH,state);
                    break;
                default:
                    break;
            }
        }

        public void ChangeAnimationSpeed(AnimationWithSpeedModifiers animationToModify, float value)
        {
            switch (animationToModify)
            {
                case AnimationWithSpeedModifiers.Walk:
                    myAnimator.SetFloat(WALK_SPEED_MODIFIER_HASH, value);
                    break;
                default:
                    break;
            }
        }
    }
}

