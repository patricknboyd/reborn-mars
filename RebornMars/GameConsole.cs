using System;
using System.Linq;
using System.Collections.Generic;

namespace Boyd.Games.RebornMars
{
    public class GameConsole : IGameConsole
    {
        private Stack<string> _messageHistory;
        public IEnumerable<string> MessageHistory { get { return _messageHistory; } }

        private Stack<string> _newMessages;
        public IEnumerable<string> NewMessages {  get { return _newMessages; } }

        public bool HasNewMessages { get { return _newMessages.Count > 0; } }

        public bool ShowDebugMessages { get; set; }

        public GameConsole()
        {
            _messageHistory = new Stack<string>();
            _newMessages = new Stack<string>();

            ShowDebugMessages = false;
        }

        public void AddMessage(string message)
        {
            string capitalized = CapitalizeFirstLetter(message);

            _messageHistory.Push(capitalized);
            _newMessages.Push(capitalized);
        }

        public void AddMessage(string format, params object[] args)
        {
            AddMessage(string.Format(format, args));
        }

        public void AddDebugMessage(string message)
        {
            string capitalized = CapitalizeFirstLetter(message);

            System.Diagnostics.Debug.WriteLine(capitalized);

            if(ShowDebugMessages)
            {
                AddMessage(capitalized);
            }
        }

        public void AddDebugMessage(string messageFormat, params object[] args)
        {
            AddDebugMessage(string.Format(messageFormat, args));
        }

        public string PopNextMessage()
        {
            return _newMessages.Pop();
        }

        public string PeekNextMessage()
        {
            return _newMessages.Peek();
        }

        private string CapitalizeFirstLetter(string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                return string.Format("{0}{1}", message.First().ToString().ToUpper(), message.Substring(1));
            }
            else
            {
                return string.Empty;
            }
        }
    }
}