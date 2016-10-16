using SoftwareModeling.Managers;

using UnityEngine.UI;

namespace SoftwareModeling.UI
{
    public class PauseResumeButton : AbstractUI
    {
        private Text _text;
        protected override void Awake()
        {
            _text = GetComponentInChildren<Text>();
        }

        protected override void Start()
        {
            SMGameManager.getInstance().onBeforeStateChange += onStateChanged;
        }

        private void onStateChanged( SMGameState prev_, SMGameState next_ )
        {
            switch (next_)
            {
                case SMGameState.IN_GAME:
                    _text.text = "Pause";
                    break;
                case SMGameState.PAUSED:
                    _text.text = "Resume";
                    break;
            }

        }

        protected override void Update()
        {
        }
    }
}
