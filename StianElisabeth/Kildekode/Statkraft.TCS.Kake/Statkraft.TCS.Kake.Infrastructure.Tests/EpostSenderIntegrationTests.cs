using System;
using NUnit.Framework;
using Statkraft.TCS.Kake.Domain;

namespace Statkraft.TCS.Kake.Infrastructure.Tests
{
    [TestFixture]
    public class EpostSenderIntegrationTests
    {
        [Test]
        public void ShouldSendEpost()
        {
            const string kakeAnsvarligNavn = "Stian";
            const string kakeAnsvarligEpost = "stian.gjedrem@statkraft.com";
            DateTime kakeDato = new DateTime(2010, 05, 17);

            var epostSender = new EpostSender();
            epostSender.Send(new KakeAnsvarlig(kakeAnsvarligNavn, kakeAnsvarligEpost), kakeDato);
        }
    }
}
