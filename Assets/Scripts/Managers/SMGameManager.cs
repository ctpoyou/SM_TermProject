using UnityEngine;
using SoftwareModeling.SMInput;
using SoftwareModeling.Constants;

namespace SoftwareModeling.Managers
{
    public enum SMGameState { ENTRY, INIT, IN_GAME, PAUSED };
    public delegate void onBeforeStateChange(SMGameState prevState_, SMGameState nextState_);

    public class SMGameManager : AbstractGameObject
    {
        private static SMGameManager _instance = null;

        private SMGameState _gameState;
        private event onBeforeStateChange _onBeforeStateChange;

        #region initialize
        protected override void Awake()
        {
            _instance = this;
            _gameState = SMGameState.ENTRY;
        }

        protected override void Start()
        {
        }

        private void initFunction()
        {
            SMCharacterManager.getInstance().init();
            changeState(SMGameState.IN_GAME);
            _onBeforeStateChange += onStateChanged;

            SMResourceManager.getInstance().preload(ResourcePathConst.NumberSpritePath);
            for( int i = 0; i <= 9; ++ i )
            {
                SMResourceManager.getInstance().preload(ResourcePathConst.getNumberPath( i ) );
            }

            SMInputManager.getInstance().onInput += (SMInputTypes t) => {
                switch( t )
                {
                    case SMInputTypes.PAUSE_BTN:
                        switch (gameState)
                        {
                            case SMGameState.IN_GAME:
                                changeState(SMGameState.PAUSED);
                                break;
                            case SMGameState.PAUSED:
                                changeState(SMGameState.IN_GAME);
                                break;
                        }
                        break;
                }
            };
        }
        #endregion

        private void onStateChanged( SMGameState prev_, SMGameState next_ )
        {
            switch (next_)
            {
                case SMGameState.PAUSED:
                    Time.timeScale = 0;
                    break;
                case SMGameState.IN_GAME:
                    Time.timeScale = 1;
                    break;
            }

        }

        protected override void Update()
        {
            switch (_gameState)
            {
                case SMGameState.ENTRY:
                    changeState(SMGameState.INIT);
                    break;
                case SMGameState.INIT:
                    initFunction();
                    break;
                case SMGameState.IN_GAME:
                    break;
                case SMGameState.PAUSED:
                    break;
            }
        }

        #region peripherals

        static public SMGameManager getInstance()
        {
            if (_instance == null)
            {
                Debug.LogError("SMGameManager is null");
                Debug.Break();
            }
            return _instance;
        }

        public SMGameState gameState
        {
            get
            {
                return _gameState;
            }
        }

        public event onBeforeStateChange onBeforeStateChange
        {
            add
            {
                _onBeforeStateChange += value;
            }

            remove
            {
                _onBeforeStateChange -= value;
            }
        }

        public void changeState(SMGameState state_)
        {
            Debug.Log(gameState + " -> " + state_);
            if( _onBeforeStateChange != null )
            {
                _onBeforeStateChange(_gameState, state_);
            }
            _gameState = state_;
        }
        #endregion
    }
}
