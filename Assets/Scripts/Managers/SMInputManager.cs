using UnityEngine;

using SoftwareModeling.SMInput;

namespace SoftwareModeling.SMInput
{
    public enum SMInputTypes { UP, DOWN, LEFT, RIGHT, ZOOM_IN, ZOOM_OUT, PAUSE_BTN };
    public delegate void onInputDelegate(SMInputTypes type_);
}

namespace SoftwareModeling.Managers
{
    class SMInputManager : AbstractGameObject
    {
        static private SMInputManager _instance = null;
        private event onInputDelegate _onInput;

        #region initialize
        protected override void Awake()
        {
            _instance = this;
        }

        protected override void Start()
        {
        }
        #endregion

        private void triggerInput( SMInputTypes type_)
        {
            if( _onInput != null )
            {
                _onInput(type_);
            }
        }

        protected override void Update()
        {
            switch( SMGameManager.getInstance().gameState )
            {
                case SMGameState.PAUSED:
                case SMGameState.IN_GAME:
                    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                    {
                        triggerInput(SMInputTypes.LEFT);
                    }
                    if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                    {
                        triggerInput(SMInputTypes.RIGHT);
                    }
                    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                    {
                        triggerInput(SMInputTypes.UP);
                    }
                    if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                    {
                        triggerInput(SMInputTypes.DOWN);
                    }

                    if(Input.GetAxis("Mouse ScrollWheel") > 0 )
                    {
                        triggerInput(SMInputTypes.ZOOM_IN);
                    }
                    else if(Input.GetAxis("Mouse ScrollWheel") < 0)
                    {
                        triggerInput(SMInputTypes.ZOOM_OUT);
                    }
                    break;
            }
        }

        private void onPause()
        {
            triggerInput(SMInputTypes.PAUSE_BTN);
        }

        #region peripherals

        static public SMInputManager getInstance()
        {
            if( _instance == null )
            {
                Debug.LogError("SMInputManager is null");
                Debug.Break();
            }
            return _instance;
        }

        public event onInputDelegate onInput
        {
            add
            {
                _onInput += value;
            }

            remove
            {
                _onInput -= value;
            }
        }

        #endregion
    }
}
