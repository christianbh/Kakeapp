using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Statkraft.TCS.Kake.Domain;

namespace Statkraft.TCS.Kake.Infrastructure.Tests
{
    [TestFixture]
    public class KakeFordelerRepositoryIntegrationTests
    {
        private KakeFordelerRepository _repository;

        [SetUp]
        public void SetUp()
        {
            _repository = new KakeFordelerRepository();
            SetUpRepositoryWithText(_repository.NesteKakeAnsvarligFilename, "Christian christian.hauknes@statkraft.com");
            SetUpRepositoryWithText(_repository.NesteKakeDatoFilename, "07.05.2010");
        }

        [Test]
        public void ShouldGetMuligeKakeAnsvarlige()
        {
            IList<KakeAnsvarlig> kakeAnsvarlige = _repository.GetMuligeKakeAnsvarlige();

            Assert.That(kakeAnsvarlige.Count, Is.EqualTo(3));
            Assert.That(kakeAnsvarlige.First().Navn, Is.EqualTo("Roy"));
            Assert.That(kakeAnsvarlige.First().Epost, Is.EqualTo("roy.eidset@statkraft.com"));
        }

        [Test]
        public void ShouldGetFerier()
        {
            FerieOversikt ferieOversikt = _repository.GetFerieOversikt();

            Assert.That(ferieOversikt.Ferier.Count, Is.EqualTo(3));
            Assert.That(ferieOversikt.Ferier[1].FromDate, Is.EqualTo(new DateTime(2010,05,15)));
            Assert.That(ferieOversikt.Ferier[1].ToDate, Is.EqualTo(new DateTime(2010,05,18)));
        }

        [Test]
        public void ShouldHandleGetFerier_WhenNoFerierExists()
        {
            SetUpRepositoryWithText(_repository.FerierFilename, "");

            FerieOversikt ferieOversikt = _repository.GetFerieOversikt();

            Assert.That(ferieOversikt.Ferier.Count, Is.EqualTo(0));
        }

        [Test]
        public void ShouldGetNextKakeAnsvarlig()
        {
            KakeAnsvarlig kakeAnsvarlig = _repository.GetNextKakeAnsvarlig();

            Assert.That(kakeAnsvarlig.Navn, Is.EqualTo("Christian"));
            Assert.That(kakeAnsvarlig.Epost, Is.EqualTo("christian.hauknes@statkraft.com"));
        }

        [Test]
        public void ShouldHandleGetNextKakeAnsvarlig_WhenNoPreviousKakeAnsvarlig()
        {
            SetUpRepositoryWithText(_repository.NesteKakeAnsvarligFilename, "");

            KakeAnsvarlig kakeAnsvarlig = _repository.GetNextKakeAnsvarlig();

            Assert.That(kakeAnsvarlig, Is.Null);
        }

        [Test]
        public void ShouldSaveNextKakeAnsvarlig()
        {
            KakeAnsvarlig kakeAnsvarlig = new KakeAnsvarlig("Christian", "christian.hauknes@statkraft.com");

            _repository.SaveNextKakeAnsvarlig(kakeAnsvarlig);

            AssertThatRepositoryHasText(_repository.NesteKakeAnsvarligFilename, "Christian christian.hauknes@statkraft.com");
        }

        [Test]
        public void ShouldGetNextKakeDato()
        {
            DateTime kakeDato = _repository.GetNextKakeDato();

            Assert.That(kakeDato.Date, Is.EqualTo(new DateTime(2010, 05, 07)));
        }

        [Test]
        public void ShouldHandleGetNextKakeDato_WhenNoPreviousKakeDato()
        {
            SetUpRepositoryWithText(_repository.NesteKakeDatoFilename, "");

            DateTime kakeDato = _repository.GetNextKakeDato();

            Assert.That(kakeDato.Date, Is.EqualTo(DateTime.MinValue));
        }

        [Test]
        public void ShouldSaveNextKakeDato()
        {
            DateTime kakeDato = new DateTime(2010, 05, 17);

            _repository.SaveNextKakeDato(kakeDato);

            AssertThatRepositoryHasText(_repository.NesteKakeDatoFilename, "17.05.2010");
        }

        private static void SetUpRepositoryWithText(string filename, string text)
        {
            File.WriteAllText(filename, text);
        }

        private static void AssertThatRepositoryHasText(string filename, string expectedText)
        {
            string actualText = File.ReadAllText(filename);
            Assert.That(actualText, Is.EqualTo(expectedText));
        }
    }
}
