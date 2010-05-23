using System.IO;
using Cake.Core.DataInterfaces;
using Cake.Core.Domain;

namespace Cake.DAL
{
    public class CakeScheduleDao : ICakeScheduleDao
    {
        private readonly IRepositoryReader _repositoryReader;
        private readonly IRepositoryWriter _repositoryWriter;
        private string _repositoryPath;

        public CakeScheduleDao(string repositoryPath)
        {
            _repositoryWriter = new RepositoryWriter();
            _repositoryReader = new RepositoryReader();
            _repositoryPath = repositoryPath;
        }

        public CakeScheduleDao(IRepositoryWriter repositoryWriter)
        {
            _repositoryWriter = repositoryWriter;
            _repositoryReader = new RepositoryReader();
        }

        public CakeScheduleDao(IRepositoryReader repositoryReader)
        {
            _repositoryReader = repositoryReader;
            _repositoryWriter = new RepositoryWriter();
        }

        public CakeScheduleDao(IRepositoryReader reposetoryReader, IRepositoryWriter reposetoryWriter)
        {
            _repositoryReader = reposetoryReader;
            _repositoryWriter = reposetoryWriter;
        }

        public string CakeScheduleFileName
        {
            get
            {
                if (string.IsNullOrEmpty(_repositoryPath))
                    _repositoryPath = Path.GetTempPath();
                return _repositoryPath + "\\CakeSchedule.cake";
            }
        }

        #region ICakeScheduleDao Members

        public CakeSchedule Get()
        {
            CakeSchedule cakeSchedule;

            try
            {
                cakeSchedule = (CakeSchedule) _repositoryReader.Read(CakeScheduleFileName);
            }
            catch
            {
                cakeSchedule = new CakeSchedule();
            }

            return cakeSchedule;
        }

        public void Save(CakeSchedule cakeSchedule)
        {
            if (cakeSchedule == null)
                cakeSchedule = new CakeSchedule();

            _repositoryWriter.Write(CakeScheduleFileName, cakeSchedule);
        }

        #endregion
    }
}