using System;
using System.Collections.Generic;
using System.Text;

namespace App172S.Helpers
{
    public class ServiceResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }
}
