using UnityEngine;

namespace enjoythevibes
{
    public static class EngineSettings
    {
        #region Platforms
        public static class Platforms
        {
            public const string PlatformsPoolTagName = "Platforms";
            public const float MaximumHeight = 5.25f;
            public const float MinimumHeight = -5.25f;
            public const float SpawnEachTime = 2.5f;
            public const float MinMaxXAxisFrame = 2.5f;
        }
        #endregion

        #region Game
        public static class Game
        {
            public const float CollisionHeight = 3.65f;
            public const float CollisionBelow = -5f;
            public const float MaxTimeScale = 3.0f;
            public const float TimeScaleMultiplier = 0.1f;
        }
        #endregion

        #region Player
        public static class Player
        {
            public static readonly Vector3 DefaultPlayerPosition = new Vector3(0f, 1.59f, 0f);
        }
        #endregion
    }
}