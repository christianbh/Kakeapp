using System;
using System.Collections.Generic;

namespace Statkraft.TCS.Kake.Domain
{
    public class KakeFordelerService : IKakeFordelerService
    {
        private const int DaysBeforeKakeDatoToSendReminder = 2;

        private IEpostSender _epostSender;
        private IKakeFordelerRepository _kakeFordelerRepository;

        public KakeFordelerService(IEpostSender epostSender, IKakeFordelerRepository kakeFordelerRepository)
        {
            _kakeFordelerRepository = kakeFordelerRepository;
            _epostSender = epostSender;
        }

        public void Run(IKakeFordeler kakeFordeler, DateTime now)
        {
            if (NowIsTimeToSendReminder(kakeFordeler, now))
            {
                _epostSender.Send(kakeFordeler.NextKakeAnsvarlig, kakeFordeler.NextKakeDato);

                Console.WriteLine("Epost med påminnelse sendt til: " + kakeFordeler.NextKakeAnsvarlig);
            }
            else if (NowIsTimeForKake(kakeFordeler, now))
            {
                kakeFordeler.FindNextKakeAnsvarlig();
                kakeFordeler.FindNextKakeDato(now);

                _kakeFordelerRepository.SaveNextKakeAnsvarlig(kakeFordeler.NextKakeAnsvarlig);
                _kakeFordelerRepository.SaveNextKakeDato(kakeFordeler.NextKakeDato);

                Console.WriteLine("Ny kakeansvarlig: " + kakeFordeler.NextKakeAnsvarlig);
                Console.WriteLine("Ny kakedato: " + kakeFordeler.NextKakeDato.ToShortDateString());
            }
        }

        public KakeFordeler Initialize()
        {
            FerieOversikt ferieOversikt = _kakeFordelerRepository.GetFerieOversikt();
            IList<KakeAnsvarlig> kakeAnsvarlige = _kakeFordelerRepository.GetMuligeKakeAnsvarlige();

            KakeAnsvarlig nextKakeAnsvarlig = _kakeFordelerRepository.GetNextKakeAnsvarlig();
            DateTime nextKakeDato = _kakeFordelerRepository.GetNextKakeDato();

            KakeFordeler kakeFordeler = new KakeFordeler(kakeAnsvarlige, ferieOversikt, nextKakeAnsvarlig, nextKakeDato, DateTime.Now);
            _kakeFordelerRepository.SaveNextKakeAnsvarlig(kakeFordeler.NextKakeAnsvarlig);
            _kakeFordelerRepository.SaveNextKakeDato(kakeFordeler.NextKakeDato);

            Console.WriteLine("Kakeansvarlig: " + kakeFordeler.NextKakeAnsvarlig);
            Console.WriteLine("Kakedato: " + kakeFordeler.NextKakeDato.ToShortDateString());

            return kakeFordeler;
        }

        private static bool NowIsTimeForKake(IKakeFordeler kakeFordeler, DateTime now)
        {
            return kakeFordeler.NextKakeDato.Date == now.Date && now.Hour == 14 && now.Minute == 00;
        }

        private static bool NowIsTimeToSendReminder(IKakeFordeler kakeFordeler, DateTime now)
        {
            return (kakeFordeler.NextKakeDato.Date - now.Date).Days == DaysBeforeKakeDatoToSendReminder && now.Hour == 14 && now.Minute == 00;
        }
    }
}