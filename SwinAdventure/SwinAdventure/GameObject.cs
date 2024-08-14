using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public abstract class GameObject : IdentifiableObject
    {
        private string _name;
        private string _description;
        private string[] _ids;

        public GameObject(string[] ids, string name, string desc) : base(ids)
        {
            _name = name;
            _description = desc;
            _ids = ids;
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public virtual string ShortDescription
        {
            get
            {
                return _name + " (" + _ids[0] + ")";
            }
        }

        public virtual string FullDescription
        {
            get
            {
                return _description;
            }
        }
    }
}
