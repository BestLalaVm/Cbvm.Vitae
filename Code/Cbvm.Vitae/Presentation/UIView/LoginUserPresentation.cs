using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Enum;

namespace Presentation.UIView
{
    [Serializable]
    public class LoginUserPresentation
    {
        public int Id
        {
            get; set;
        }

        public string Identity
        {
            get; set;
        }

        public string UserName
        {
            get; set;
        }

        public string Password
        {
            get; set;
        }

        public UserType UserType
        {
            get; set;
        }

        public DateTime LogTime
        {
            get; set;
        }

        public string UserLabel
        {
            get; set;
        }

        private IDictionary<string, string> _AddtionalUser;
        public IDictionary<string, string> AddtionalUser
        {
            get
            {
                if (_AddtionalUser == null)
                {
                    _AddtionalUser = new Dictionary<string, string>();
                }
                return _AddtionalUser;
            }
            set
            {
                _AddtionalUser = value;
            }
        }

        public string Name
        {
            get
            {
                return String.IsNullOrEmpty(UserLabel) ? UserName : UserLabel;
            }
        }

        public string FullName { get; set; }

        public string Image { get; set; }
    }
}
