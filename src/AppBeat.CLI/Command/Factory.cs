using System;
using System.Collections.Generic;
using System.Linq;

namespace AppBeat.CLI.Command
{
    public static class Factory
    {
        private static Dictionary<string, Tuple<int, Type>> _commands = new Dictionary<string, Tuple<int, Type>>() {
            //lower case command name, sort order, type of implementing class
            { "help", new Tuple<int, Type>(1000, typeof(Help)) },
            { "page-speed", new Tuple<int, Type>(2000, typeof(PageSpeed)) }
        };

        public static ICommand GetCommandByName(string name)
        {
            if (name == null) return null;

            name = name.ToLowerInvariant();
            if (!_commands.ContainsKey(name))
            {
                return null;
            }

            var type = _commands[name].Item2;
            var command = Activator.CreateInstance(type) as ICommand;
            return command;
        }

        public static string[] SupportedCommands
        {
            get
            {
                return (from command in _commands orderby command.Value.Item1 select command.Key).ToArray();
            }
        }
    }
}
