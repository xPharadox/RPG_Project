using System;

namespace RPG
{
    public class Creature
    {
        public string name;
        public int attack;
        public int health;
        public int magic;
        public bool res;

        public Creature(string _name, int _attack, int _health, int _magic, bool _res)
        {
            name = _name;
            attack = _attack;
            health = _health;
            magic = _magic;
            res = _res;
        }
    }
}
