using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars
{
    public interface IGameConsole
    {
        /// <summary>
        /// If true, Debug messages will be included in game output.
        /// </summary>
        bool ShowDebugMessages { get; set; }

        /// <summary>
        /// Adds a message to the console.
        /// </summary>
        /// <param name="message">The message to add.</param>
        void AddMessage(string message);
        /// <summary>
        /// Adds a message to the console.
        /// </summary>
        /// <param name="messageFormat">The message format.</param>
        /// <param name="args">A collection of zero or more objects to insert into the message string.</param>
        void AddMessage(string messageFormat, params object[] args);

        /// <summary>
        /// Adds a message to the debug console.
        /// </summary>
        /// <param name="message">The message to add.</param>
        void AddDebugMessage(string message);

        /// <summary>
        /// Adds a message to the debug console.
        /// </summary>
        /// <param name="messageFormat">The message format.</param>
        /// <param name="args">A collection of zero or more objects to insert into the message string.</param>
        void AddDebugMessage(string messageFormat, params object[] args);

        /// <summary>
        /// Gets a history of all messages.
        /// </summary>
        IEnumerable<string> MessageHistory { get; }
        /// <summary>
        /// Gets the newly added messages.
        /// </summary>
        IEnumerable<string> NewMessages { get; }

        /// <summary>
        /// If true, new messages are waiting to be displayed.
        /// </summary>
        bool HasNewMessages { get; }

        /// <summary>
        /// Gets the next new message, and removes the message from the new message collection.
        /// </summary>
        /// <returns></returns>
        string PopNextMessage();

        /// <summary>
        /// Gets the next new message, but does not remove it from the new message collection.
        /// </summary>
        /// <returns></returns>
        string PeekNextMessage();
    }
}
