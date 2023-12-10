using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterProject
{
    public interface IFileUtil<T>
    {
        List<T> ReadDataFromFile();
        List<T> ReadDataFromFile(string propertyName, string propertyValue);
        void WriteDataToFile(T data);
        void ReWriteDataToFile<Z>(string propertyName_Search, string propertyValue_Search, string propertyName_Update, Z propertyValue_Update);
    }
}
