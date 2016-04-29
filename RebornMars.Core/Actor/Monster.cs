using Boyd.Games.RebornMars.Assets;
using Boyd.Games.RebornMars.Inventory;
using Boyd.Games.RebornMars.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.Actor
{
    public class Monster : IMonster
    {
        protected static double BaseToHitChance = 0.5;
        protected static int BaseDamage = 5;

        public int Armour { get; set; }
        public int Evasion { get; set; }

        public int CurrentHP { get; set; }
        public int MaxHP { get; set; }
        public int CurrentMP { get; set; }
        public int MaxMP { get; set; }

        public int Strength { get; set; }
        public int Constitution { get; set; }
        public int Dexterity { get; set; }
        public int Intelligence { get; set; }
        public int Luck { get; set; }

        public MonsterMovementType Movement { get; set; }
        public Position Position { get; set; }
        public string Name { get; protected set; }
        public string DefinitiveForm { get; protected set; }
        public string AttackVerb { get; protected set; }
        public char Symbol { get; protected set; }

        private List<IItem> _inventory;
        public IEnumerable<IItem> Inventory {  get { return _inventory; } }

        public Monster()
        {
            Name = "Undefined";
            Symbol = MonsterSymbols.UndefinedMonsters[0];

            _inventory = new List<IItem>();
        }

        internal Monster(MonsterDefinition template, Position position) 
            : this()
        {
            Name = template.Name;
            DefinitiveForm = template.DefinitiveForm;
            AttackVerb = template.AttackVerb;
            Symbol = template.Symbol;

            MaxHP = template.MaxHP;
            CurrentHP = template.MaxHP;
            MaxMP = template.MaxMP;
            CurrentMP = template.MaxMP;
            Strength = template.Strength;
            Constitution = template.Constitution;
            Dexterity = template.Dexterity;
            Intelligence = template.Intelligence;
            Luck = template.Luck;

            Armour = template.Armour;
            Evasion = template.Evasion;

            Position = position;
        }


        public virtual void Move(MoveDirection dir)
        {
            MapState state = Game.Current.Dungeon.CurrentMapState;
            IMap map = state.Map;

            MoveTestResult moveTest = state.MoveTest(Position, dir);

            if (moveTest.IsMoveValid && CanMoveInDirection(moveTest))
            {
                if (moveTest.Monster != null)
                {
                    var attackResult = Attack(moveTest.Monster);
                    OutputAttackResult(attackResult, moveTest.Monster);
                }
                else
                {
                    Position = moveTest.NewPosition;
                }
            }

        }


        public virtual AttackResult Attack(IMonster target)
        {
            bool isHit = false;
            int damage = 0;

            // Let's do some placeholder logic for calculating damage.
            double dexDiff = (this.Dexterity - target.Dexterity) / 10.0;

            isHit = Game.Current.RNG.NextDouble() < (BaseToHitChance + dexDiff);

            if(isHit)
            {
                damage = BaseDamage + (this.Strength - target.Constitution);

                damage += Game.Current.RNG.Next(-2, 2);

                AttackResult result = new AttackResult(isHit, damage);

                target.TakeDamage(result);

                return result;
            }
            else
            {
                return AttackResult.Miss;
            }
        }

        public virtual void TakeDamage(AttackResult attack)
        {
            if(attack.IsHit)
            {
                this.CurrentHP -= attack.Damage;

                if(CurrentHP <= 0)
                {
                    this.Kill();
                }
            }
        }

        public virtual void Kill()
        {
            Game.Current.Messages.AddMessage("{0} has been killed!", DefinitiveForm);
            Game.Current.Dungeon.CurrentMapState.RemoveMonster(this);
        }

        public virtual void Update()
        {
            IGameCore game = Game.Current;
            MapState state = game.Dungeon.CurrentMapState;

            if(state.ArePositionsAdjacent(Position, game.Player.Position))
            {
                AttackTargetAndOutput(game.Player);
            }

            
        }

        protected AttackResult AttackTargetAndOutput(IMonster target)
        {
            AttackResult result = Attack(target);

            OutputAttackResult(result, target);

            return result;
        }


        protected virtual bool CanMoveInDirection(MoveTestResult testResult)
        {
            switch (Movement)
            {
                case MonsterMovementType.Walk:
                    return testResult.Tile.IsWalkable;

                case MonsterMovementType.Fly:
                    return testResult.Tile.IsFlyable;

                case MonsterMovementType.Swim:
                    return testResult.Tile.IsSwimmable;

                default:
                    throw new ArgumentException(string.Format(ErrorMessages.UnrecognizedEnumValue, Movement.ToString()));
            }

        }

        protected void OutputAttackResult(AttackResult result, IMonster target)
        {
            if (result.IsHit)
            {
                Game.Current.Messages.AddMessage("{0} {1} {2}.", DefinitiveForm, AttackVerb, target.DefinitiveForm);
                Game.Current.Messages.AddDebugMessage("{0} hits {1} for {2} damage ({3}/{4}).", 
                    DefinitiveForm, target.DefinitiveForm, result.Damage, target.CurrentHP, target.MaxHP);
            }
            else
            {
                Game.Current.Messages.AddMessage("{0} missed {1}.", DefinitiveForm, target.DefinitiveForm);
            }
        }

    }
}
