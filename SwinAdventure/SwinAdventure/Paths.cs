using System;

namespace SwinAdventure
{
    public class Paths : GameObject
    {
        bool _isLocked;
        Locations _source, _destination;
        string[] _ids;

        public Paths(string[] ids, string name, string desc, Locations source, Locations destination) : base(ids, name, desc)
        {
            _isLocked = false;
            _source = source;
            _destination = destination;
            _ids = ids;

            AddIdentifier("path");
            foreach (string s in name.Split(" "))
            {
                AddIdentifier(s);
            }
        }

        public Locations Destination
        {
            get
            {
                return _destination;
            }
        }

        public override string ShortDescription
        {
            get
            {
                return "- " + _ids[0] + ": " + _destination.Name + "\n";
            }
        }

        public bool IsLocked
        {
            get
            {
                return _isLocked;
            }
            set
            {
                _isLocked = value;
            }
        }
    }
}
