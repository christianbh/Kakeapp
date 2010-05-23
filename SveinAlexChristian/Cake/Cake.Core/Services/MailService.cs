using System;
using System.Collections.Generic;
using System.Net.Mail;
using Cake.Core.Configuration;
using Cake.Core.Domain;

namespace Cake.Core.Services
{
    public class MailService : IMailService
    {
        private readonly IAppConfigReader _appConfigReader;
        private readonly IDepartmentServices _departmentServices;
        private DateTime _cakeDate;

        public MailService(IDepartmentServices departmentServices, IAppConfigReader appConfigReader, DateTime cakeDate)
        {
            _departmentServices = departmentServices;
            _appConfigReader = appConfigReader;
            _cakeDate = cakeDate;
        }

        #region IMailService Members

        public void SendMailToNextCakeDepartment(IList<Department> departments)
        {
            Department department = _departmentServices.GetFirstDepartmentInList(departments);

            if (department.ContactEmail == string.Empty)
            {
                return;
            }

            SendMail(department.ContactEmail);
        }

        public void SendMail(string emailAdress)
        {
            var smtpClient = new SmtpClient();
            smtpClient.Host = _appConfigReader.MailHost;
            var mailMessage = new MailMessage();


            var fromAddress = new MailAddress(_appConfigReader.FromMailAddress);
            mailMessage.From = fromAddress;
            mailMessage.To.Add(emailAdress);
            mailMessage.Subject = _appConfigReader.MailSubject;
            mailMessage.Body = string.Format(_appConfigReader.MailBody, _cakeDate.Date);
            smtpClient.Send(mailMessage);
        }

        #endregion
    }
}