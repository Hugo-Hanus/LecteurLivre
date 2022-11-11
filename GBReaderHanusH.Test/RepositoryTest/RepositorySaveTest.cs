using GBReaderHanusH.Domains.Domains;
using GBReaderHanusH.Infrastructure.DTO;
using GBReaderHanusH.Infrastructure.Mapper;
using GBReaderHanusH.Infrastructure.Repository;
using GBReaderHanusH.Repository.CustomException;
using GBReaderHanusH.Repository.Repository;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using System;
using System.Globalization;

namespace GBReaderHanusH.Test.RepositoryTest
{
    public class RepositorySaveTest
    {

        [Test]
        public void GoodSituationCreateUe36AndJsonFile()
        {
            const string path = @"..\..\..\resources\CreateDirectory";
            const string pathComplete = path + @"\ue36\190533-session.json";
            if (File.Exists(pathComplete))
            {
                File.Delete(pathComplete);
                if (Directory.Exists(pathComplete))
                {
                    Directory.Delete(path + @"ue36");
                }

            }
            IMapper mapperOne = new MapperOne();
            var history = new History();
            string json = "{\"1-551-11\":{\"Begin\":\"2022-12-21T16:34:32\",\"LastUpdate\":\"2022-12-21T16:34:32\",\"Title\":\"Titre 1\",\"Page\":5},\"2-662-22\":{\"Begin\":\"2022-12-21T16:34:32\",\"LastUpdate\":\"2022-12-21T16:34:32\",\"Title\":\"Titre 2\",\"Page\":6}}\r\n";
            DateTime date = DateTime.ParseExact("2022-12-21 16:34:32", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            history.SessionList.Add("1-551-11", new Session(date, date, "Titre 1", 5));
            history.SessionList.Add("2-662-22", new Session(date, date, "Titre 2", 6));
           
            IRepository repo = new JsonRepository(mapperOne, history, path);
            repo.SaveHistory(history);
              using StreamReader reader = new(pathComplete);
                var jsonString = reader.ReadToEnd();
                reader.Close();
            
            Assert.That(jsonString, Is.EqualTo(json));
        }
        [Test]
        public void GoodSituationCreateOnlyJsonFile()
        {
            const string path = @"..\..\..\resources\FileUe36";
            const string pathComplete = path + @"\ue36\190533-session.json";
            if (File.Exists(pathComplete))
            {
                File.Delete(pathComplete);
            }
            IMapper mapperOne = new MapperOne();
            var history = new History();
            string json = "{\"1-551-11\":{\"Begin\":\"2022-12-21T16:34:32\",\"LastUpdate\":\"2022-12-21T16:34:32\",\"Title\":\"Titre 1\",\"Page\":5},\"2-662-22\":{\"Begin\":\"2022-12-21T16:34:32\",\"LastUpdate\":\"2022-12-21T16:34:32\",\"Title\":\"Titre 2\",\"Page\":6}}\r\n";
            DateTime date = DateTime.ParseExact("2022-12-21 16:34:32", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            history.SessionList.Add("1-551-11", new Session(date, date, "Titre 1", 5));
            history.SessionList.Add("2-662-22", new Session(date, date, "Titre 2", 6));

            IRepository repo = new JsonRepository(mapperOne, history, path);
            repo.SaveHistory(history);
                using StreamReader reader = new(pathComplete);
                var jsonString = reader.ReadToEnd();
                reader.Close();
            Assert.That(jsonString, Is.EqualTo(json));
        }

    }
}
