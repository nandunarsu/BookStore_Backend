﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Exceptions
{
    public class EmailSendingException:Exception
    {
        public EmailSendingException(string message) : base(message) { }
        public EmailSendingException(string message, Exception innerException)
          : base(message, innerException)
        {
        }

    }
}
