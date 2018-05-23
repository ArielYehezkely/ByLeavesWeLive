using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Playables;

namespace Mewlist.BSplinePath
{
    [System.Serializable]
    public class PathPlayableAsset : PlayableAsset
    {
        public ExposedReference<Path> Path = new ExposedReference<Path>() ;
        public AnimationCurve Curve = AnimationCurve.Linear(0, 0, 1, 1);
        public double Start;
        public double End;
        public PathTrackAsset TrackAsset;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            PathPlayableBehaviour behaviour = new PathPlayableBehaviour();
            behaviour.Path = Path.Resolve(graph.GetResolver());
            behaviour.Curve = Curve;
            behaviour.Start = Start;
            behaviour.End = End;
	        return ScriptPlayable<PathPlayableBehaviour>.Create(graph, behaviour);
        }
    }
}
