using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Cake.Core.DataInterfaces;

namespace Cake.DAL
{
    public class RepositoryReader : IRepositoryReader
    {
        #region IRepositoryReader Members

        public object Read(string fileName)
        {
            var fileStream = new FileStream(fileName, FileMode.Open);
            var binaryFormatter = new BinaryFormatter();
            object data = binaryFormatter.Deserialize(fileStream);
            fileStream.Close();

            return data;
        }

        #endregion
    }
}