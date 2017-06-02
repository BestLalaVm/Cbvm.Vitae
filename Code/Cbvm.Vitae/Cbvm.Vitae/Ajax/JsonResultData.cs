using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cbvm.Vitae.Ajax
{
    public class JsonResultData
    {
        public JsonResultData()
        {
            this.Success = true;
        }

        public bool Success { get; set; }

        public string RedirectUrl { get; set; }

        public string Message { get; set; }

        public object Model { get; set; }
    }
}