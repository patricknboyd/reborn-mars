using Boyd.Games.RebornMars.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.UI.Shell.Output
{
    public class GameStateOutput
    {
        public string OutputGameState(IGameCore game)
        {
            StringBuilder output = new StringBuilder();
            Player player = game.Player;

            string nameString = string.Format("{0} the {1}", player.PlayerName, player.Title);

            output.AppendFormat("{0,-30}", nameString);

            output.AppendFormat("St:{0,-3}", player.Strength);
            output.AppendFormat("Dx:{0,-3}", player.Dexterity);
            output.AppendFormat("Co:{0,-3}", player.Constitution);
            output.AppendFormat("In:{0,-3}", player.Intelligence);
            output.AppendFormat("Lu:{0,-3}", player.Luck);

            output.AppendLine();

            output.AppendFormat("Dlvl:{0,-4} ", game.Dungeon.CurrentDungeonLevel);
            output.AppendFormat("$:{0} ", "N/A");
            output.AppendFormat("HP:{0}({1}) ", player.CurrentHP, player.MaxHP);
            output.AppendFormat("MP:{0}({1}) ", player.CurrentMP, player.MaxMP);
            output.AppendFormat("AC:{0} ", player.Armour);
            output.AppendFormat("XP:{0} ", "N/A");
            output.AppendFormat("T:{0} ", "N/A");

            output.AppendLine();

            return output.ToString();

        }
    }
}
