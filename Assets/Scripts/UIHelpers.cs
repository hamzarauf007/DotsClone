using System;
using UnityEngine;
using System.Collections;

namespace UI
{
    public static class UIHelpers
    {
        public static IEnumerator TweenPosition(Transform transform, Vector3 targetPosition, float duration, Action onComplete = null)
        {
            float time = 0;
            Vector3 startPosition = transform.position;
            while (time < duration)
            {
                transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            transform.position = targetPosition; // Ensure it ends at the target position
            onComplete?.Invoke();
        }
    }
}
