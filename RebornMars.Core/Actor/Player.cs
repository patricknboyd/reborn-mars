using Boyd.Games.RebornMars.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.Actor
{
    public class Player : Monster
    {
        public string PlayerName { get; set; }
        public string Title { get; set; }

        public Player(int startX, int startY) 
        {
            Symbol = Assets.MonsterSymbols.Humanoids[0];
            Name = "you";
            DefinitiveForm = "you";
            AttackVerb = "hit";
            Position = new Position(startX, startY);
            Movement = MonsterMovementType.Walk;

            Strength = 10;
            Constitution = 10;
            Dexterity = 10;
            Intelligence = 10;
            Luck = 10;

            Armour = 100;
            Evasion = 100;

            MaxHP = 100;
            CurrentHP = 100;

            MaxMP = 50;
            CurrentMP = 50;

            PlayerName = "Patrick";
            Title = "Developer";

        }
    }
}
