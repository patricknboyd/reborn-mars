using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.UI.Shell.Output
{
    public class MessageOutput
    {
        /// <summary>
        /// Gets the current games message log.
        /// </summary>
        public IGameConsole Messages { get; private set; }
        /// <summary>
        /// Gets or sets the number of characters that can be output per line.
        /// </summary>
        public int OutputWidth { get; set; }

        public static string MoreMessagesString = " -MORE-";

        public MessageOutput(int consoleWidth)
        {
            Messages = Game.Current.Messages;
            OutputWidth = consoleWidth - MoreMessagesString.Length;
        }

        public string GetOutputMessageLine()
        {
            if (Messages.HasNewMessages)
            {
                StringBuilder output = new StringBuilder();
                output.Append(Messages.PopNextMessage());

                while (Messages.HasNewMessages)
                {
                    if(output.Length + Messages.PeekNextMessage().Length + 1 <= OutputWidth)
                    {
                        output.AppendFormat(" {0}", Messages.PopNextMessage());
                    }
                    else
                    {
                        output.Append(MoreMessagesString);
                        break;
                    }
                }

                return output.ToString();                
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
