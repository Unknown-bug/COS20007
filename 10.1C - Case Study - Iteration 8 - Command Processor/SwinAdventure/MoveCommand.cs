using System;

namespace SwinAdventure
{
    public class MoveCommand : Command
    {
        public MoveCommand() : base(new string[] {"move"}) 
        { 
        }

        public override string Execute(Player p, string[] text)
        {
            string error = "I don't know how to move that.";
            string _direction;
            switch (text.Length)
            {
                case 1:
                    return "Move where?";
                case 2:
                    _direction = text[1].ToLower();
                    break;
                case 3:
                    _direction = text[2].ToLower();
                    break;
                default:
                    return error;
            }

            GameObject _path = p.Location.Locate(_direction);
            if (_path == null)
            {
                return error;
            }
            if (_path is Paths)
            {
                Paths _p = (Paths)_path;
                if (_p.IsLocked)
                {
                    return "The path is blocked.";
                }
                p.Location = _p.Destination;
                return "You moved to " + p.Location.Name + ".\n";
            }
            return error;
        }


    }
}
