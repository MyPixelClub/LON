using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RankPage
{
    public class RankPage : MonoBehaviour
    {
        [SerializeField] private Sprite[] _variationsAvatars;
        [SerializeField] private List<Enemy> _enemies;

        private void Start()
        {
            int lastEnemyRankPoint = 75;
            _enemies.Reverse();

            for (int i = 0; i < _enemies.Count; i++)
            {
                _enemies[i].Render(_variationsAvatars[Random.Range(0, _variationsAvatars.Length)], GenerateName(Random.Range(0, 8)), lastEnemyRankPoint += Random.Range(0, 15), _enemies.Count - i);
            }
        }

        public string GenerateName(int len)
        {
            System.Random r = new System.Random();
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string Name = "";
            Name += consonants[r.Next(consonants.Length)].ToUpper();
            Name += vowels[r.Next(vowels.Length)];
            int b = 2;
            while (b < len)
            {
                Name += consonants[r.Next(consonants.Length)];
                b++;
                Name += vowels[r.Next(vowels.Length)];
                b++;
            }

            return Name;
        }
    }
}