using SoftwareModeling.GameCharacter;
using System.Collections.Generic;

using UnityEngine;

namespace SoftwareModeling.Managers
{
    public class SMCharacterManager
    {
        static private SMCharacterManager _instance = null;

        private List<AICharacter> _playerCharacters;
        private List<AICharacter> _enemyCharacters;

        static public SMCharacterManager getInstance()
        {
            if( _instance == null )
            {
                _instance = new SMCharacterManager();
            }

            return _instance;
        }

        #region initialize
        private SMCharacterManager()
        {
            _playerCharacters = new List<AICharacter>();
            _enemyCharacters = new List<AICharacter>();
        }

        public void init()
        {
            GameObject[] party;
            party = GameObject.FindGameObjectsWithTag("PlayerParty");
            AICharacter character;
            foreach( GameObject obj in party )
            {
                character = obj.GetComponent<AICharacter>();
                _playerCharacters.Add(character);
                character.onDestroy += onCharacterDestroy;
            }
            party = GameObject.FindGameObjectsWithTag("EnemyParty");
            foreach (GameObject obj in party)
            {
                character = obj.GetComponent<AICharacter>();
                _enemyCharacters.Add(character);
                character.onDestroy += onCharacterDestroy;
            }
        }

        #endregion

        #region peripherals
        public List<AICharacter> playerParty
        {
            get
            {
                return _playerCharacters;
            }
        }

        public List<AICharacter> enemyParty
        {
            get
            {
                return _enemyCharacters;
            }
        }

        private void onCharacterDestroy( AICharacter self_ )
        {
            switch( self_.faction )
            {
                case FactionEnum.Ally:
                    _playerCharacters.Remove(self_);
                    break;
                case FactionEnum.Enemy:
                    _enemyCharacters.Remove(self_);
                    break;
            }
        }
        #endregion

        public AICharacter getNearestEnemyFrom(AICharacter c)
        {
            List<AICharacter> party;

            switch (c.faction)
            {
                case FactionEnum.Ally:
                    party = enemyParty;
                    break;

                default:
                    party = playerParty;
                    break;
            }

            if( party.Count == 0 )
            {
                return null;
            }

            AICharacter minDistEnemy = party[0];
            float minDist = Vector2.Distance(c.position, minDistEnemy.position);
            float dist;

            for( int i = 1; i < party.Count; ++ i)
            {
                dist = Vector2.Distance(party[i].position, c.position);
                if (minDist > dist )
                {
                    minDist = dist;
                    minDistEnemy = party[i];
                }
            }

            return minDistEnemy;
        }
    }
}
