using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections;
using System.Configuration;

namespace TwitterProject
{
    public class FileUtil<T> : IFileUtil<T>
        where T : ISerializerData
    {
        public string FilePath { get; private set; }
        public IDesign design { get;private set; }

        public FileUtil(string fileName)
        {
            // Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()) + "\\Data"
            FilePath = ConfigurationManager.AppSettings["path"] + $@"\{fileName}";
            design = new Design();
        }
        public List<T> ReadDataFromFile()
        {
            var content = string.Empty;
            var fileMembers = new List<T>();
            if (!File.Exists(FilePath))
            {
                design.Error($"{FilePath} Is Not Exist");
                Console.WriteLine();
            }
            else
            {
                using (var reader = new StreamReader(FilePath))
                {
                    content = reader.ReadToEnd();
                }
                if (!string.IsNullOrWhiteSpace(content))
                {
                    var setting = new JsonSerializerSettings();
                    setting.NullValueHandling = NullValueHandling.Ignore;
                    fileMembers = JsonConvert.DeserializeObject<List<T>>(content, setting);
                }
            }
            return fileMembers;
        }
        public List<T> ReadDataFromFile(string propertyName, string propertyValue)
        {
            var fileMembers = ReadDataFromFile();
            var requiredMember = new List<T>();
            if (!fileMembers.Any() || fileMembers == null)
            {
                design.Error($"file is empty");
            }
            else
            {
                requiredMember = fileMembers.FindAll(i => i.GetType().GetProperty(propertyName).GetValue(i).ToString().Equals(propertyValue));
            }
            return requiredMember;
        }
        public void WriteDataToFile(T data)
        {
            long id = 0;
            var fileMembers = ReadDataFromFile();
            if (fileMembers.Count == 0)
            {
                id++;
            }
            else
            {
                var maxId = fileMembers.Max(i => Convert.ToInt64(i.Id));
                id = maxId + 1;
            }
            data.GetType().GetProperty("Id").SetValue(data, id);
            fileMembers.Add(data);
            var json = JsonConvert.SerializeObject(fileMembers);
            File.WriteAllText(FilePath, json);
        }
        public void ReWriteDataToFile<Z>(string propertyName_Search, string propertyValue_Search, string propertyName_Update,Z  propertyValue_Update)
        {
            var fileMembers = ReadDataFromFile();
            if (!fileMembers.Any() || fileMembers == null)
            {
                design.Error("file is empty");
            }
            else
            {
                var index = fileMembers.FindIndex(i => i.GetType().GetProperty(propertyName_Search).GetValue(i).ToString().Equals(propertyValue_Search));
                if (index!= -1)
                {
                    var flieMember = fileMembers[index];
                    flieMember.GetType().GetProperty(propertyName_Update).SetValue(flieMember, propertyValue_Update);
                    fileMembers.RemoveAt(index);
                    fileMembers.Insert(index, flieMember);
                    var json = JsonConvert.SerializeObject(fileMembers);
                    File.WriteAllText(FilePath, json);
                }
                else
                {
                    design.Error("memmber not found");
                }
            }
        }
    }
}
