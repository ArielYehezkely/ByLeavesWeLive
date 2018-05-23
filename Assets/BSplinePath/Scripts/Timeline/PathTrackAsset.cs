using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Mewlist.BSplinePath
{
    [Serializable]
    [TrackClipType(typeof(PathPlayableAsset))]
    [TrackBindingType(typeof(Transform))]
    public class PathTrackAsset : TrackAsset
    {
        const string DefaultDisplayName = "PathPlayableAsset";

        [SerializeField]
        public bool ControlRotation = false;
        [SerializeField]
        public bool HorizontalRatation = true;
        [SerializeField]
        public bool RotationSmoothing = true;

        private ScriptPlayable<PathPlayableBehaviour> mixer;

        public override Playable CreateTrackMixer(
            PlayableGraph graph, GameObject go, int inputCount)
        {
            mixer = ScriptPlayable<PathPlayableBehaviour>.Create(graph);
            mixer.SetInputCount(inputCount);

            foreach (var clip in GetClips())
            {
                var asset = clip.asset as PathPlayableAsset;
                if (asset)
                {
                    asset.Start = clip.start;
                    asset.End = clip.end;
                    if (clip.displayName == DefaultDisplayName)
                    {
                        var path = asset.Path.Resolve(graph.GetResolver());
                        clip.displayName = path != null ? path.name : clip.displayName;
                    }
                }
            }

            mixer.GetBehaviour().ControlRotation = ControlRotation;
            mixer.GetBehaviour().HorizontalRatation = HorizontalRatation;
            mixer.GetBehaviour().RotationSmoothing = RotationSmoothing;

            return mixer;
        }

        void OnValidate()
        {
            if(!mixer.IsValid()) return;
            mixer.GetBehaviour().ControlRotation = ControlRotation;
            mixer.GetBehaviour().HorizontalRatation = HorizontalRatation;
            mixer.GetBehaviour().RotationSmoothing = RotationSmoothing;
        }
    }
}
