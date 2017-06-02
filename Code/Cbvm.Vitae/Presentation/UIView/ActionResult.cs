using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.UIView
{
    public class ActionResult
    {
        public bool IsSucess
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public static ActionResult DefaultResult
        {
            get
            {
                return new ActionResult()
                {
                    IsSucess = true
                };
            }
        }

        public static ActionResult NotFoundResult
        {
            get
            {
                return CreateErrorActionResult("找不到数据!");
            }
        }

        public static ActionResult CreateErrorActionResult(string error)
        {
            return new ActionResult()
            {
                IsSucess = false,
                Message = error
            };
        }

    }
}
