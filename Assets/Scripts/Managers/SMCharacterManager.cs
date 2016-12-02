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

        private void onCharacterDestroy( ITargetable self_ )
        {
            AICharacter character = self_ as AICharacter;
            switch( character.faction )
            {
                case FactionEnum.Ally:
                    _playerCharacters.Remove(character);
                    break;
                case FactionEnum.Enemy:
                    _enemyCharacters.Remove(character);
                    break;
            }
        }
        #endregion

        private List<AICharacter> getParty( FactionEnum faction_)
        {
            switch (faction_)
            {
                case FactionEnum.Ally:
                    return enemyParty;

                default:
                    return playerParty;
            }
        }

        public AICharacter getLowestHealthAlly(AICharacter c )
        {
            List<AICharacter> party = getParty(c.faction);

            if (party.Count == 0)
            {
                return null;
            }

            AICharacter minHealthCharacter = party[0];

            for (int i = 1; i < party.Count; ++i)
            {
                if( minHealthCharacter.hitPoint > party[i].hitPoint)
                {
                    minHealthCharacter = party[i];
                }
            }

            return minHealthCharacter;
        }

        public AICharacter getNearestEnemyFrom(AICharacter c)
        {
            List<AICharacter> party = getParty(c.faction);

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
