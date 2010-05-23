using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Cake.Core.DataInterfaces;

namespace Cake.DAL
{
    public class RepositoryWriter : IRepositoryWriter
    {
        #region IRepositoryWriter Members

        public void Write(string fileName, object data)
        {
            var fileStream = new FileStream(fileName, FileMode.Create);
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(fileStream, data);
            fileStream.Close();
        }

        #endregion
    }
}