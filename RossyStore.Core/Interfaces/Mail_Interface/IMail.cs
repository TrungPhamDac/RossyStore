using RossyStore.Core.Models.Mail_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RossyStore.Core.Interfaces.Mail_Interface
{
    public interface IMail
    {
        Task SendEmailAsync(MailRequest mailRequest);
        Task SendEmailAsync(_MailRequest mailRequest);
    }
}
