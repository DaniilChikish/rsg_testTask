using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    /// <summary>
	/// Класс-утилита для переопределения <see cref="AnimatorOverrideController"/> по некому List из <see cref="AnimationClip"/>,  
    /// и переопределяет их если у них совпадают имена анимаций
	/// </summary>
    public static class AnimatorsOverrideUtility
    {
#if UNITY_EDITOR
        //editmode:
        /// <summary>
        /// Заменяет анимации, перегружая метод при аргументе типа <see cref="AnimatorController"/> 
        /// Для Editmode
        /// </summary>
        public static void OverrideAnimationsClipsWithSameNames(AnimatorOverrideController animatorOverride, List<AnimationClip> animationsClipTryToChange, 
                                                                    UnityEditor.Animations.AnimatorController mainAnimatorController)
        {
            var animatorOverrideApplied = OverrideAnimationsClipsWithSameNames(animatorOverride, animationsClipTryToChange, mainAnimatorController.animationClips);
            UnityEditor.EditorUtility.SetDirty(animatorOverrideApplied);
        }
#endif

        /// <summary>
        /// Заменяет анимации, перегружая метод при аргументе типа A<see cref="Animator"/> 
        /// Для Runtime Playmode
        /// </summary>
        public static void OverrideAnimationsClipsWithSameNames(AnimatorOverrideController animatorOverride, List<AnimationClip> animationsClipTryToChange, Animator mainAnimatorController)
        {
            var animatorOverrideApplied = OverrideAnimationsClipsWithSameNames(animatorOverride, animationsClipTryToChange, mainAnimatorController.runtimeAnimatorController.animationClips);
            mainAnimatorController.runtimeAnimatorController = animatorOverrideApplied;
        }

        /// <summary>
        /// Логика непосредственного переопределения для <see cref="AnimatorOverrideController"/> . при Нахождения совпадающих имен для анимационных клипов
        /// </summary>
        public static AnimatorOverrideController OverrideAnimationsClipsWithSameNames(AnimatorOverrideController animatorOverride, List<AnimationClip> animationsClipTryToChange, AnimationClip[] animationClipsFromAnimator)
        {
            var anims = new List<KeyValuePair<AnimationClip, AnimationClip>>();

            foreach (var clip in animationClipsFromAnimator)
            {
                foreach (var otherClip in animationsClipTryToChange)
                {
                    if (otherClip == null)
                        continue;

                    if (clip.name == otherClip.name)
                        anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(clip, otherClip));
                }
            }

            animatorOverride.ApplyOverrides(anims);

            return animatorOverride;
        }
    }
}