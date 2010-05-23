using System;
using System.Net.Mail;
using Statkraft.TCS.Kake.Domain;

namespace Statkraft.TCS.Kake.Infrastructure
{
    public class EpostSender : IEpostSender
    {
        private const string MailServer = "SMTPGW.nordic.energycorp.com";
        private const string EpostTekst = "<h2>Påminnelse!</h2><p>Det er din tur til å sørge for kake førstkommende fredag {0}. (Husk å ta med 2 kaker)</p><p>Vennlig hilsen KakeAppen :-)</p><p><img src='http://internt/kake/kake.jpg' /></p>";
        private const string EpostTittel = "Påminnelse: Det er din tur til å sørge for kake førstkommende fredag.";
        private const string KakeAppNavn = "TCS KakeApp";
        private const string KakeAppEpost = "tcs-kakeapp@statkraft.com";

        public void Send(KakeAnsvarlig kakeAnsvarlig, DateTime kakeDato)
        {
            MailMessage mail = new MailMessage(
                new MailAddress(KakeAppEpost, KakeAppNavn), 
                new MailAddress(kakeAnsvarlig.Epost, kakeAnsvarlig.Navn))
                                   {
                                       Subject = EpostTittel,
                                       IsBodyHtml = true,
                                       Body = String.Format(EpostTekst, kakeDato.ToShortDateString())
                                   };

            SmtpClient client = new SmtpClient {Host = MailServer};

            client.Send(mail); 
        }
    }
}
