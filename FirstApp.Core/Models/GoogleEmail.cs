using System;
using System.Collections.Generic;
using System.Text;

namespace FirstApp.Core.Models
{
    public class GoogleEmail
    {
        public GoogleEmailData Data { get; set; }
    }

    public class GoogleEmailData
    {
        public string Email { get; set; }
    }
}
