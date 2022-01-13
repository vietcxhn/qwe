using UnityEngine;

namespace LF2
{
    /// <summary>
    /// Abstract base class containing some common members shared by Action (server) and ActionFX (client visual)
    /// </summary>
    public abstract class StateBase
    {
        protected CharacterTypeEnum CharacterType;



        /// <summary>
        /// Time when this Action was started (from Time.time) in seconds. Set by the ActionPlayer or ActionVisualization.
        /// </summary>
        public float TimeStarted { get; set; }

        /// <summary>
        /// How long the Action has been running (since its Start was called)--in seconds, measured via Time.time.
        /// </summary>
        public float TimeRunning => Time.time - TimeStarted;


        // / <summary>
        // / Data Description for this action.
        // / </summary>


        public bool AnimationActionTrigger;
        public StateBase()
        {
        }

    }

}
