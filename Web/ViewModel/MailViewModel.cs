﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.ViewModel
{
    public class MailViewModel
    {
        [Required, Display(Name = "İsminiz")]
        public string FromName { get; set; }

        [Required, Display(Name = "E-Posta adresi"), EmailAddress]
        public string FromEmail { get; set; }

        [Required, Display(Name = "Konu"),]
        public string Subject { get; set; }

        [Required, Display(Name = "Mesaj"),]
        public string Message { get; set; }
    }
}