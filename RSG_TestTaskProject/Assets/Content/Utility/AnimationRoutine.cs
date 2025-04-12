using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utility
{
    public static class AnimationRoutine
    {
        #region Position
        /// <summary>
        /// Modular corutine
        /// </summary>
        /// <param name="objTransform"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="duration"></param>
        /// <param name="formula"></param>
        /// <param name="onFinish"></param>
        /// <param name="space"></param>
        /// <returns></returns>
        private static IEnumerator Move(Transform objTransform, Vector3 start, Vector3 end, float duration, Func<float, float, float> formula, Action onFinish, Space space = Space.Self)
        {
            float movingTime = 0;
            while (movingTime <= duration)
            {
                if (objTransform == null)
                {
                    yield break;
                }
                if (space == Space.Self)
                {
                    objTransform.localPosition = Vector3.Lerp(start, end, formula(movingTime, duration));
                }
                else
                {
                    objTransform.position = Vector3.Lerp(start, end, formula(movingTime, duration));
                }
                movingTime += Time.deltaTime;
                yield return null;
            }
            if (space == Space.Self)
            {
                objTransform.localPosition = end;
            }
            else
            {
                objTransform.position = end;
            }

            if (onFinish != null)
            {
                onFinish();
            }
        }
        public static IEnumerator Move(Transform objTransform, Vector3 start, Vector3 end, float duration, AnimationCurve curve, Action onFinish, Space space = Space.Self)
        {
            float movingTime = 0;
            while (movingTime <= duration)
            {
                if (objTransform == null)
                {
                    yield break;
                }
                if (space == Space.Self)
                {
                    objTransform.localPosition = Vector3.Lerp(start, end, curve.Evaluate(movingTime / duration));
                }
                else
                {
                    objTransform.position = Vector3.Lerp(start, end, curve.Evaluate(movingTime / duration));
                }
                movingTime += Time.deltaTime;
                yield return null;
            }
            if (space == Space.Self)
            {
                objTransform.localPosition = end;
            }
            else
            {
                objTransform.position = end;
            }

            if (onFinish != null)
            {
                onFinish();
            }
        }

        /// <summary>
        /// Interpolate local position linear.
        /// ! Corutine need start !
        /// </summary>
        /// <param name="objTransform"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="duration"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator LinearMove(Transform objTransform, Vector3 start, Vector3 end, float duration, Action onFinish)
        {
            return Move(objTransform, start, end, duration, Linear, onFinish, Space.Self);
        }

        /// <summary>
        /// Interpolate world position linear.
        /// ! Corutine need start !
        /// </summary>
        /// <param name="objTransform"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="duration"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator LinearMoveWorld(Transform objTransform, Vector3 start, Vector3 end, float duration, Action onFinish)
        {
            return Move(objTransform, start, end, duration, Linear, onFinish, Space.World);
        }
        /// <summary>
        /// Interpolate local position exponential.
        /// ! Corutine need start !
        /// </summary>
        /// <param name="objTransform"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="duration"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator ExponentMove(Transform objTransform, Vector3 start, Vector3 end, float duration, Action onFinish)
        {
            return Move(objTransform, start, end, duration, Exponent, onFinish, Space.Self);
        }

        /// <summary>
        /// Interpolate world position exponential.
        /// ! Corutine need start !
        /// </summary>
        /// <param name="objTransform"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="duration"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator ExponentMoveWorld(Transform objTransform, Vector3 start, Vector3 end, float duration, Action onFinish)
        {
            return Move(objTransform, start, end, duration, Exponent, onFinish, Space.World);
        }
        /// <summary>
        /// Interpolate local position. Sinus curve.
        /// ! Corutine need start !
        /// </summary>
        /// <param name="objTransform"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="duration"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator SinusMove(Transform objTransform, Vector3 start, Vector3 end, float duration, Action onFinish)
        {
            return Move(objTransform, start, end, duration, Sinus, onFinish, Space.Self);
        }

        /// <summary>
        /// SinusMove with additional callback
        /// ! Corutine need start !
        /// </summary>
        /// <param name="objTransform"></param>
        /// <param name="start"></param>
        /// <param name="p1"></param>
        /// <param name="end"></param>
        /// <param name="duration"></param>
        /// <param name="onFinish"></param>
        /// <param name="onMiddle"></param>
        /// <returns></returns>
        public static IEnumerator SinusMove(Transform objTransform, Vector3 start, Vector3 p1, Vector3 end, float duration, Action onFinish, Action onMiddle)
        {
            float movingTime = 0;
            while (movingTime <= (duration / 2))
            {
                movingTime += Time.deltaTime;
                objTransform.localPosition = Vector3.Lerp(start, p1, Asseleration(movingTime, duration / 2));
                yield return null;
            }
            if (onMiddle != null)
            {
                onMiddle();
            }
            while (movingTime <= duration)
            {
                objTransform.localPosition = Vector3.Lerp(p1, end, Braking(movingTime - duration / 2, duration / 2));
                movingTime += Time.deltaTime;
                yield return null;
            }
            objTransform.localPosition = end;

            if (onFinish != null)
            {
                onFinish();
            }
        }

        /// <summary>
        /// Interpolate world position. Sinus curve.
        /// ! Corutine need start !
        /// </summary>
        /// <param name="objTransform"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="duration"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator SinusMoveWorld(Transform objTransform, Vector3 start, Vector3 end, float duration, Action onFinish)
        {
            return Move(objTransform, start, end, duration, Sinus, onFinish, Space.World);
        }

        /// <summary>
        /// ! Corutine need start !
        /// </summary>
        /// <param name="objTransform"></param>
        /// <param name="start"></param>
        /// <param name="p1"></param>
        /// <param name="end"></param>
        /// <param name="duration"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        private static IEnumerator BezierMove(Transform objTransform, Vector3 start, Vector3 p1, Vector3 end, float duration, Func<float, float, float> formula, Action onFinish, Space space = Space.Self)
        {
            float movingTime = 0;
            while (movingTime <= duration)
            {
                if (space == Space.Self)
                {
                    objTransform.localPosition = Bezier(start, p1, end, formula(movingTime, duration));
                }
                else
                {
                    objTransform.position = Bezier(start, p1, end, formula(movingTime, duration));
                }
                movingTime += Time.deltaTime;
                yield return null;
            }
            if (space == Space.Self)
            {
                objTransform.localPosition = end;
            }
            else
            {
                objTransform.position = end;
            }

            if (onFinish != null)
            {
                onFinish();
            }
        }
        /// <summary>
        /// ! Corutine need start !
        /// </summary>
        /// <param name="objTransform"></param>
        /// <param name="start"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="end"></param>
        /// <param name="duration"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        private static IEnumerator BezierMove(Transform objTransform, Vector3 start, Vector3 p1, Vector3 p2, Vector3 end, float duration, Func<float, float, float> formula, Action onFinish, Space space = Space.Self)
        {
            float movingTime = 0;
            while (movingTime <= duration)
            {
                if (space == Space.Self)
                {
                    objTransform.localPosition = Bezier(start, p1, p2, end, formula(movingTime, duration));
                }
                else
                {
                    objTransform.position = Bezier(start, p1, p2, end, formula(movingTime, duration));
                }
                movingTime += Time.deltaTime;
                yield return null;
            }
            if (space == Space.Self)
            {
                objTransform.localPosition = end;
            }
            else
            {
                objTransform.position = end;
            }

            if (onFinish != null)
            {
                onFinish();
            }
        }

        /// <summary>
        /// Interpolated move on bezier. Linear curve.
        /// ! Corutine need start !
        /// </summary>
        /// <param name="objTransform"></param>
        /// <param name="start"></param>
        /// <param name="p1"></param>
        /// <param name="end"></param>
        /// <param name="duration"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator LinearBezierMove(Transform objTransform, Vector3 start, Vector3 p1, Vector3 end, float duration, Action onFinish)
        {
            return BezierMove(objTransform, start, p1, end, duration, Linear, onFinish, Space.Self);
        }

        /// <summary>
        /// Interpolated move on bezier. Exponent curve.
        /// ! Corutine need start !
        /// </summary>
        /// <param name="objTransform"></param>
        /// <param name="start"></param>
        /// <param name="p1"></param>
        /// <param name="end"></param>
        /// <param name="duration"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator ExponentBezierMove(Transform objTransform, Vector3 start, Vector3 p1, Vector3 end, float duration, Action onFinish)
        {
            return BezierMove(objTransform, start, p1, end, duration, Exponent, onFinish, Space.Self);
        }

        /// <summary>
        /// Interpolated move on bezier. Sinus curve. 
        /// ! Corutine need start !
        /// </summary>
        /// <param name="objTransform"></param>
        /// <param name="start"></param>
        /// <param name="p1"></param>
        /// <param name="end"></param>
        /// <param name="duration"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator SinusBezierMove(Transform objTransform, Vector3 start, Vector3 p1, Vector3 end, float duration, Action onFinish)
        {
            return BezierMove(objTransform, start, p1, end, duration, Sinus, onFinish, Space.Self);
        }

        /// <summary>
        /// Interpolated move on bezier. Sinus curve. 
        /// ! Corutine need start !
        /// </summary>
        /// <param name="objTransform"></param>
        /// <param name="start"></param>
        /// <param name="p1"></param>
        /// <param name="end"></param>
        /// <param name="duration"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator SinusBezierMove(Transform objTransform, Vector3 start, Vector3 p1, Vector3 p2, Vector3 end, float duration, Action onFinish)
        {
            return BezierMove(objTransform, start, p1, p2, end, duration, Sinus, onFinish, Space.Self);
        }

        /// <summary>
        /// Interpolated move on bezier. Sinus curve. 
        /// ! Corutine need start !
        /// </summary>
        /// <param name="objTransform"></param>
        /// <param name="start"></param>
        /// <param name="p1"></param>
        /// <param name="end"></param>
        /// <param name="duration"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator SinusBezierMoveWorld(Transform objTransform, Vector3 start, Vector3 p1, Vector3 end, float duration, Action onFinish)
        {
            return BezierMove(objTransform, start, p1, end, duration, Sinus, onFinish, Space.World);
        }
        /// <summary>
        /// Interpolated move on bezier. Tangens curve. 
        /// The speed decreases to zero in the middle of the trajectory, and then increases.
        /// ! Corutine need start !
        /// </summary>
        /// <param name="objTransform"></param>
        /// <param name="start"></param>
        /// <param name="p1"></param>
        /// <param name="end"></param>
        /// <param name="duration"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator TangensBezierMove(Transform objTransform, Vector3 start, Vector3 p1, Vector3 end, float duration, Action onFinish)
        {
            return BezierMove(objTransform, start, p1, end, duration, Tangens, onFinish, Space.Self);
        }
        /// <summary>
        /// ! Corutine need start !
        /// </summary>
        /// <param name="objTransform"></param>
        /// <param name="start"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="end"></param>
        /// <param name="duration"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator ExponentBezierMove(Transform objTransform, Vector3 start, Vector3 p1, Vector3 p2, Vector3 end, float duration, Action onFinish)
        {
            return BezierMove(objTransform, start, p1, p2, end, duration, Exponent, onFinish, Space.Self);
        }

        /// <summary>
        /// Sinus oscillation betveen two points
        /// ! Corutine need start !
        /// </summary>
        /// <param name="objTransform"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public static IEnumerator Oscillation(Transform objTransform, Vector3 start, Vector3 end, float period)
        {
            float movingTime = 0;
            while (true)
            {
                if (objTransform == null)
                {
                    yield break;
                }
                objTransform.localPosition = Vector3.Lerp(start, end, Sinus(movingTime, period / 2));
                movingTime += Time.deltaTime;
                yield return null;
            }
        }

        /// <summary>
        /// Sinus oscillation betveen two points
        /// ! Corutine need start !
        /// </summary>
        /// <param name="objTransform"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public static IEnumerator OscillationWorld(Transform objTransform, Vector3 start, Vector3 end, float period)
        {
            float movingTime = 0;
            while (true)
            {
                if (objTransform == null)
                {
                    yield break;
                }
                objTransform.position = Vector3.Lerp(start, end, Sinus(movingTime, period / 2));
                movingTime += Time.deltaTime;
                yield return null;
            }
        }

        #endregion
        #region Color
        /// <summary>
        /// Intarpolate color alpha.
        /// ! Corutine need start !
        /// </summary>
        /// <param name="image"></param>
        /// <param name="direction"></param>
        /// <param name="duration"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator Fade(Graphic image, byte direction, float duration, Action onFinish)
        {
            float movingTime = 0;
            Vector4 start = new Vector4(image.color.r, image.color.g, image.color.b, 1 - direction);
            Vector4 destination = new Vector4(image.color.r, image.color.g, image.color.b, direction);
            while (movingTime <= duration)
            {
                image.color = Vector4.Lerp(start, destination, Sinus(movingTime, duration));
                movingTime += Time.deltaTime;
                yield return null;
            }
            image.color = destination;

            if (onFinish != null)
            {
                onFinish();
            }
        }

        /// <summary>
        /// Intarpolate color alpha. 1 => fade in, 0 => fade out.
        /// ! Corutine need start !
        /// </summary>
        /// <param name="image"></param>
        /// <param name="direction"> 1 => fade in, 0 => fade out.</param>
        /// <param name="duration"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator Fade(SpriteRenderer image, byte direction, float duration, Action onFinish)
        {
            float movingTime = 0;
            Vector4 start = new Vector4(image.color.r, image.color.g, image.color.b, 1 - direction);
            Vector4 destination = new Vector4(image.color.r, image.color.g, image.color.b, direction);
            while (movingTime <= duration)
            {
                image.color = Vector4.Lerp(start, destination, Sinus(movingTime, duration));
                movingTime += Time.deltaTime;
                yield return null;
            }
            image.color = destination;

            if (onFinish != null)
            {
                onFinish();
            }
        }

        /// <summary>
        /// Intarpolate color alpha. 1 => fade in, 0 => fade out.
        /// ! Corutine need start !
        /// </summary>
        /// <param name="image"></param>
        /// <param name="direction"> 1 => fade in, 0 => fade out.</param>
        /// <param name="duration"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator Fade(List<SpriteRenderer> images, byte direction, float duration, Action onFinish)
        {
            float movingTime = 0;
            Vector4 start = new Vector4(images[0].color.r, images[0].color.g, images[0].color.b, 1 - direction);
            Vector4 destination = new Vector4(images[0].color.r, images[0].color.g, images[0].color.b, direction);
            while (movingTime <= duration)
            {
                for (int i = 0; i < images.Count; i++)
                {
                    images[i].color = Vector4.Lerp(start, destination, Sinus(movingTime, duration));
                }
                movingTime += Time.deltaTime;
                yield return null;
            }
            if (onFinish != null)
            {
                onFinish();
            }
        }

        /// <summary>
        /// Intarpolate color alpha. 1 => fade in, 0 => fade out.
        /// ! Corutine need start !
        /// </summary>
        /// <param name="image"></param>
        /// <param name="direction"> 1 => fade in, 0 => fade out.</param>
        /// <param name="duration"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator Fade(CanvasGroup group, byte direction, float duration, Action onFinish)
        {
            float movingTime = 0;
            float start = 1 - direction;
            float destination = direction;
            while (movingTime <= duration)
            {
                group.alpha = Mathf.Lerp(start, destination, Sinus(movingTime, duration));
                movingTime += Time.deltaTime;
                yield return null;
            }
            group.alpha = destination;

            if (onFinish != null)
            {
                onFinish();
            }
        }

        /// <summary>
        /// Intarpolate color alpha. 1 => fade in, 0 => fade out. Unscaled deltaTime.
        /// ! Corutine need start !
        /// </summary>
        /// <param name="image"></param>
        /// <param name="direction"> 1 => fade in, 0 => fade out.</param>
        /// <param name="duration"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator FadeRealtime(CanvasGroup group, byte direction, float duration, Action onFinish)
        {
            float movingTime = 0;
            float start = 1 - direction;
            float destination = direction;
            while (movingTime <= duration)
            {
                group.alpha = Mathf.Lerp(start, destination, Sinus(movingTime, duration));
                movingTime += Time.unscaledDeltaTime;
                yield return null;
            }
            group.alpha = destination;

            if (onFinish != null)
            {
                onFinish();
            }
        }
        /// <summary>
        /// Sinus fade loop to value
        /// ! Corutine need start !
        /// </summary>
        /// <param name="objTransform"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public static IEnumerator SinusFadeLoop(Graphic image, float min, float max, float period)
        {
            float movingTime = 0;
            Vector4 start = new Vector4(image.color.r, image.color.g, image.color.b, min);
            Vector4 end = new Vector4(image.color.r, image.color.g, image.color.b, max);
            while (true)
            {
                if (image == null)
                {
                    yield break;
                }

                image.color = Vector4.Lerp(start, end, Sinus(movingTime, period / 2));
                movingTime += Time.deltaTime;
                yield return null;
            }
        }
        /// <summary>
        /// Interpolate betveen two colors.
        /// ! Corutine need start !
        /// </summary>
        /// <param name="image"></param>
        /// <param name="start"></param>
        /// <param name="destination"></param>
        /// <param name="duration"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator ColorInterpolate(Graphic image, Vector4 start, Vector4 destination, float duration, Action onFinish)
        {
            float movingTime = 0;
            while (movingTime <= duration)
            {
                image.color = Vector4.Lerp(start, destination, Sinus(movingTime, duration));
                movingTime += Time.deltaTime;
                yield return null;
            }
            image.color = destination;

            if (onFinish != null)
            {
                onFinish();
            }
        }
        /// <summary>
        /// Interpolate betveen two colors.
        /// ! Corutine need start !
        /// </summary>
        /// <param name="renderer"></param>
        /// <param name="start"></param>
        /// <param name="destination"></param>
        /// <param name="duration"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator ColorInterpolate(SpriteRenderer renderer, Vector4 start, Vector4 destination, float duration, Action onFinish)
        {
            float movingTime = 0;
            while (movingTime <= duration)
            {
                renderer.color = Vector4.Lerp(start, destination, Sinus(movingTime, duration));
                movingTime += Time.deltaTime;
                yield return null;
            }
            renderer.color = destination;

            if (onFinish != null)
            {
                onFinish();
            }
        }
        #endregion
        #region Rotation
        /// <summary>
        /// Rotate to target euler angles.
        /// ! Corutine need start !
        /// </summary>
        /// <param name="objTransform"></param>
        /// <param name="targetRotation"></param>
        /// <param name="speed"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator Rotate(Transform objTransform, Vector3 targetRotation, float speed, Action onFinish)
        {
            bool isRotate = true;
            Quaternion target = Quaternion.Euler(targetRotation);
            while (isRotate)
            {
                objTransform.rotation = Quaternion.RotateTowards(objTransform.rotation, target, speed * Time.deltaTime);
                if (Quaternion.Angle(objTransform.rotation, target) <= 1)
                {
                    isRotate = false;
                    if (onFinish != null)
                    {
                        onFinish();
                    }
                }

                yield return null;
            }
        }
        /// <summary>
        /// Rotate on angle by axis
        /// ! Corutine need start !
        /// </summary>
        /// <param name="objTransform"></param>
        /// <param name="axis"></param>
        /// <param name="angle"></param>
        /// <param name="duration"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator Rotate(Transform objTransform, Vector3 axis, float angle, float duration, Action onFinish)
        {
            float movingTime = 0;
            while (movingTime <= duration)
            {
                objTransform.Rotate(axis, (angle / duration) * Time.deltaTime);
                movingTime += Time.deltaTime;
                yield return null;
            }

            if (onFinish != null)
            {
                onFinish();
            }
        }
        /// <summary>
        /// Rotate on angle by axis around point
        /// ! Corutine need start !
        /// </summary>
        /// <param name="objTransform"></param>
        /// <param name="point"></param>
        /// <param name="axis"></param>
        /// <param name="angle"></param>
        /// <param name="duration"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator RotateAround(Transform objTransform, Vector3 point, Vector3 axis, float angle, float duration, Action onFinish)
        {
            float movingTime = 0;
            while (movingTime <= duration)
            {
                objTransform.RotateAround(point, axis, angle * Sinus(movingTime, duration));
                movingTime += Time.deltaTime;
                yield return null;
            }

            if (onFinish != null)
            {
                onFinish();
            }
        }
        /// <summary>
        /// Interpolate wetveen two euler angles
        /// Asound world axis only.
        /// ! Corutine need start !
        /// </summary>
        /// <param name="objTransform"></param>
        /// <param name="fromEulerRotation"></param>
        /// <param name="toEulerRotation"></param>
        /// <param name="duration"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator RotateEuler(Transform objTransform, Vector3 startEulerRotation, Vector3 destinationEulerRotation, float duration, Action onFinish)
        {
            float movingTime = 0;
            while (movingTime <= duration)
            {
                objTransform.eulerAngles = Vector3.Lerp(startEulerRotation, destinationEulerRotation, Sinus(movingTime, duration)); ;
                movingTime += Time.deltaTime;
                yield return null;
            }

            if (onFinish != null)
            {
                onFinish();
            }
        }
        #endregion
        #region Fill

        /// <summary>
        /// Interpolate image fill
        /// ! Corutine need start !
        /// </summary>
        /// <param name="image"></param>
        /// <param name="startValue"></param>
        /// <param name="endValue"></param>
        /// <param name="duration"></param>
        /// <param name="onFinish"></param>
        /// <param name="formula"></param>
        /// <returns></returns>
        private static IEnumerator FillAmountInterpolation(Image image, float startValue, float endValue, float duration, Action onFinish, Func<float, float, float> formula)
        {
            float movingTime = 0;
            while (movingTime <= duration)
            {
                image.fillAmount = Mathf.Lerp(startValue, endValue, formula(movingTime, duration));
                movingTime += Time.deltaTime;
                yield return null;
            }
            image.fillAmount = endValue;

            onFinish?.Invoke();
        }

        /// <summary>
        /// Interpolate image fill linear.
        /// ! Corutine need start !
        /// </summary>
        /// <param name="image"></param>
        /// <param name="startValue"></param>
        /// <param name="endValue"></param>
        /// <param name="duration"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator FillAmountLinearInterpolation(Image image, float startValue, float endValue, float duration, Action onFinish)
        {
            return FillAmountInterpolation(image, startValue, endValue, duration, onFinish, Linear);
        }
        /// <summary>
        /// Interpolate image fill linear.
        /// ! Corutine need start !
        /// </summary>
        /// <param name="image"></param>
        /// <param name="startValue"></param>
        /// <param name="endValue"></param>
        /// <param name="duration"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator FillAmountSinusInterpolation(Image image, float startValue, float endValue, float duration, Action onFinish)
        {
            return FillAmountInterpolation(image, startValue, endValue, duration, onFinish, Sinus);
        }

        /// <summary>
        /// Interpolate image fill
        /// ! Corutine need start !
        /// </summary>
        /// <param name="image"></param>
        /// <param name="startValue"></param>
        /// <param name="endValue"></param>
        /// <param name="duration"></param>
        /// <param name="onFinish"></param>
        /// <param name="formula"></param>
        /// <returns></returns>
        private static IEnumerator FillAmountInterpolation(Slider image, float startValue, float endValue, float duration, Action onFinish, Func<float, float, float> formula)
        {
            float movingTime = 0;
            while (movingTime <= duration)
            {
                image.value = Mathf.Lerp(startValue, endValue, formula(movingTime, duration));
                movingTime += Time.deltaTime;
                yield return null;
            }
            image.value = endValue;

            onFinish?.Invoke();
        }

        /// <summary>
        /// Interpolate image fill linear.
        /// ! Corutine need start !
        /// </summary>
        /// <param name="image"></param>
        /// <param name="startValue"></param>
        /// <param name="endValue"></param>
        /// <param name="duration"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator FillAmountLinearInterpolation(Slider image, float startValue, float endValue, float duration, Action onFinish)
        {
            return FillAmountInterpolation(image, startValue, endValue, duration, onFinish, Linear);
        }
        /// <summary>
        /// Interpolate image fill linear.
        /// ! Corutine need start !
        /// </summary>
        /// <param name="image"></param>
        /// <param name="startValue"></param>
        /// <param name="endValue"></param>
        /// <param name="duration"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator FillAmountSinusInterpolation(Slider image, float startValue, float endValue, float duration, Action onFinish)
        {
            return FillAmountInterpolation(image, startValue, endValue, duration, onFinish, Sinus);
        }
        #endregion
        #region Scale


        /// <summary>
        /// Interlolate scale
        /// </summary>
        /// <param name="objTransform"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="duration"></param>
        /// <param name="formula"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        private static IEnumerator Scale(Transform objTransform, Vector3 start, Vector3 end, float duration, Func<float, float, float> formula, Action onFinish)
        {
            float movingTime = 0;
            while (movingTime <= duration)
            {
                if (objTransform == null)
                {
                    yield break;
                }
                objTransform.localScale = Vector3.Lerp(start, end, formula(movingTime, duration));
                movingTime += Time.deltaTime;
                yield return null;
            }
            objTransform.localScale = end;

            if (onFinish != null)
            {
                onFinish();
            }
        }
        private static IEnumerator Scale(Transform objTransform, Vector3 start, Vector3 end, float duration, AnimationCurve curve, Action onFinish)
        {
            float movingTime = 0;
            while (movingTime <= duration)
            {
                if (objTransform == null)
                {
                    yield break;
                }
                objTransform.localScale = Vector3.Lerp(start, end, curve.Evaluate(movingTime / duration));
                movingTime += Time.deltaTime;
                yield return null;
            }
            objTransform.localScale = end;

            if (onFinish != null)
            {
                onFinish();
            }
        }


        /// <summary>
        /// Scale oscillation. Sinus curve. (Can be used for flip imitation)
        /// </summary>
        /// <param name="objTransform"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="duration"></param>
        /// <param name="formula"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator ScaleLoop(Transform objTransform, Vector3 start, Vector3 end, float period)
        {
            float movingTime = 0;
            while (true)
            {
                if (objTransform == null)
                {
                    yield break;
                }
                objTransform.localScale = Vector3.Lerp(start, end, Sinus(movingTime, period / 2));
                movingTime += Time.deltaTime;
                yield return null;
            }
        }

        /// <summary>
        /// Interlolate scale linear
        /// ! Corutine need start !
        /// </summary>
        /// <param name="objTransform"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="duration"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator LinearScale(Transform objTransform, Vector3 start, Vector3 end, float duration, Action onFinish)
        {
            return Scale(objTransform, start, end, duration, Linear, onFinish);
        }

        /// <summary>
        /// Interlolate scale sinus curve
        /// ! Corutine need start !
        /// </summary>
        /// <param name="objTransform"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="duration"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator SinusScale(Transform objTransform, Vector3 start, Vector3 end, float duration, Action onFinish)
        {
            return Scale(objTransform, start, end, duration, Sinus, onFinish);
        }

        #endregion
        #region Another

        /// <summary>
        /// Delay.
        /// ! Corutine need start !
        /// </summary>
        /// <param name="seconds"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator WaitFor(float seconds, Action onFinish)
        {
            yield return new WaitForSeconds(seconds);

            if (onFinish != null)
            {
                onFinish();
            }
        }

        /// <summary>
        /// Realtime delay.
        /// </summary>
        /// <param name="seconds"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator WaitForRealtime(float seconds, Action onFinish)
        {
            yield return new WaitForSecondsRealtime(seconds);

            if (onFinish != null)
            {
                onFinish();
            }
        }

        /// <summary>
        /// Interpolate camera orto-size
        /// ! Corutine need start !
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="startValue"></param>
        /// <param name="endValue"></param>
        /// <param name="duration"></param>
        /// <param name="onUpdate"></param>
        /// <param name="onFinish"></param>
        /// <param name="formula"></param>
        /// <returns></returns>
        private static IEnumerator CameraOrtoSize(Camera camera, float startValue, float endValue, float duration, Action onUpdate, Action onFinish, Func<float, float, float> formula)
        {
            float movingTime = 0;
            while (movingTime <= duration)
            {
                camera.orthographicSize = Mathf.Lerp(startValue, endValue, formula(movingTime, duration));
                movingTime += Time.deltaTime;
                onUpdate?.Invoke();
                yield return null;
            }
            camera.orthographicSize = endValue;

            onFinish?.Invoke();
        }

        /// <summary>
        /// Interpolate camera orto-size
        /// ! Corutine need start !
        /// /// </summary>
        /// <param name="camera"></param>
        /// <param name="targetZoom"></param>
        /// <param name="duration"></param>
        /// <param name="onUpdate"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator SinusCameraOrtoSize(Camera camera, float targetZoom, float duration, Action onUpdate, Action onFinish)
        {
            return CameraOrtoSize(camera, camera.orthographicSize, targetZoom, duration, onUpdate, onFinish, Sinus);
        }
        #endregion
        #region Formulas
        public static float Linear(float time, float duration)
        {
            return time / duration;
        }
        public static float Exponent(float time, float duration)
        {
            return Mathf.Pow(2, ((time / duration) - 1) * 6);
        }
        public static float CubicParabola(float time, float duration)
        {
            return Mathf.Pow((((time / duration) - 0.5f) * 1.6f), 3f) + 0.5f;
        }
        public static float Tangens(float time, float duration) //1/pi*tg(x*2); 
        {
            return (1 / Mathf.PI * (Mathf.Tan((time / duration) * 2 - 1))) + 0.5f;
        }
        public static float Sinus(float time, float duration)
        {
            return (Mathf.Cos((time / duration) * Mathf.PI - Mathf.PI) + 1f) / 2f;
        }
        public static float Asseleration(float time, float duration)
        {
            return Mathf.Pow((time / duration), 2);
        }
        public static float Braking(float time, float duration)
        {
            return -Mathf.Pow(((time / duration) - 1f), 2f) + 1f;
        }
        public static Vector3 Bezier(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            return (Mathf.Pow((1 - t), 2) * p0) + (2 * t * (1 - t) * p1) + (Mathf.Pow(t, 2) * p2);
        }
        public static Vector3 Bezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            return (Mathf.Pow((1 - t), 3) * p0) + (3 * t * Mathf.Pow((1 - t), 2) * p1) + (3 * Mathf.Pow(t, 2) * (1 - t) * p2) + (Mathf.Pow(t, 3) * p3);
        }
        #endregion
    }
}
