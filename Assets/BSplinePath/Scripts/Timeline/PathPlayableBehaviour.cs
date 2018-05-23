using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Mewlist.BSplinePath
{
    public class PathPlayableBehaviour : PlayableBehaviour
    {
        public Path Path;
        public Transform MoveTarget;
        public bool ControlRotation;
        public bool HorizontalRatation;
        public bool RotationSmoothing;
        public AnimationCurve Curve;
        public double Start;
        public double End;

        private Vector3 initialPosition;
        private Vector3 initialDirection;

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            var count = playable.GetInputCount();
            if (count > 0)
            {
                GameObject go = playerData as GameObject;
                if (MoveTarget == null)
                {
                    if (go == null)
                        MoveTarget = playerData as Transform;
                    else
                        MoveTarget = go.GetComponent<Transform>();
                    if (MoveTarget == null)
                        return;

                    initialDirection = MoveTarget.InverseTransformDirection(Vector3.forward);
                    initialPosition = MoveTarget.localPosition;
                }

                Vector3 position = Vector3.zero;
                Vector3 direction = Vector3.zero;
                var rate = 0f;
                var lastClipRate = 0f;

                bool noClip = true;
                for (int i = 0; i < count; i++)
                {
                    var p = playable.GetInput(i);
                    var behaviour = ((ScriptPlayable<PathPlayableBehaviour>)p).GetBehaviour();
                    var weight = playable.GetInputWeight(i);
                    if (weight > 0 && behaviour.Path != null)
                    {
                        noClip = false;
                        rate = behaviour.Curve.Evaluate(GetRate(p));
                        position += behaviour.Path.GetPoint(rate).Position * weight;
                        direction += behaviour.Path.GetPoint(rate).Direction * weight;
                        lastClipRate = rate;
                    }
                }

                if (noClip)
                {
                    Path lastPath = null;
                    double currentTime = CurrentTime(playable);
                    double lastClipEndTime = float.MinValue;
                    // set path to previous finished clip end point.
                    for (int i = 0; i < count; i++)
                    {
                        var p = playable.GetInput(i);
                        var behaviour = ((ScriptPlayable<PathPlayableBehaviour>)p).GetBehaviour();
                        if (currentTime >= behaviour.End && lastClipEndTime < behaviour.End)
                        {
                            lastPath = behaviour.Path;
                            lastClipEndTime = behaviour.End;
                        }
                    }
                    lastClipRate = 1f;
                    if (lastPath != null)
                    {
                        var point = lastPath.GetPoint(1f);
                        position = point.Position;
                        direction = point.Direction;
                    }
                    else
                    {
                        position = initialPosition;
                        direction = initialDirection;
                    }
                }

                if (MoveTarget != null)
                {
                    MoveTarget.localPosition = position;
                    if (ControlRotation)
                    {
                        var lookAt = HorizontalRatation ?
                            position + Vector3.Scale(direction, new Vector3(1f, 0f, 1f)) :
                            position + direction;
                        MoveTarget.LookAt(Vector3.Lerp(position + MoveTarget.InverseTransformDirection(Vector3.forward), lookAt, RotationSmoothing ? lastClipRate * 20f : 1f));
                    }
                }
            }
        }

        private float GetRate(Playable playable)
        {
            return (float)(playable.GetTime() / playable.GetDuration());
        }

        private double CurrentTime(Playable playable)
        {
            return (playable.GetGraph().GetResolver() as PlayableDirector).time;
        }
    }
}

