using GBReaderHanusH.Domains.Domains;
using GBReaderHanusH.Infrastructure.DTO;
using GBReaderHanusH.Infrastructure.Mapper;
using GBReaderHanusH.Infrastructure.Repository;
using GBReaderHanusH.Repository.CustomException;
using GBReaderHanusH.Repository.Repository;
using System.Globalization;

namespace GBReaderHanusH.Test.RepositoryTest;

public class RepositoryLoadTest
{
    
    [Test]
    public void GoodSituationTwoSession()
    {
        ISessionMapper mapperOne = new SessionMapperOne();
        var history = new History();
        DateTime date = DateTime.ParseExact("2022-12-21 16:34:32", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        DateTime dateT = DateTime.ParseExact("2022-12-21 15:14:12", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        history.SessionList.Add("1-551-11", new Session(date,date,"Titre 1",5));
        history.SessionList.Add("2-662-22", new Session(dateT, dateT, "Titre 2", 6));
        const string path = @"..\..\..\resources\GoodSituation";
        IRepository repo = new JsonRepository(mapperOne, history, path);
        repo.SaveHistory(history);
        repo.LoadHistory();
        Assert.That(repo.History.SessionList, Has.Count.EqualTo(history.SessionList.Count));
        Assert.That(repo.History.SessionList["1-551-11"].ToDetail(), Is.EqualTo(history.SessionList["1-551-11"].ToDetail()));
        Assert.That(repo.History.SessionList["2-662-22"].ToDetail(), Is.EqualTo(history.SessionList["2-662-22"].ToDetail()));
    }
    [Test]
   public void NotGoodFormat()
    {
        
        ISessionMapper mapperOne = new SessionMapperOne();
        var history = new History();
        const string path = @"..\..\..\resources\NotGoodFormat";
        IRepository repo = new JsonRepository(mapperOne, history, path);
        try
        {
            repo.LoadHistory();
            Assert.Fail();
        }
        catch (NotFormatJsonException)
        {
            Assert.That(repo.History.SessionList, Is.Empty);
        }
    }
    [Test]
    public void NotGoodFormatDateTime()
    {

        ISessionMapper mapperOne = new SessionMapperOne();
        var history = new History();
        const string path = @"..\..\..\resources\NotGoodFormatDateTime";
        IRepository repo = new JsonRepository(mapperOne, history, path);
        try
        {
            repo.LoadHistory();
            Assert.Fail();
        }
        catch (NotFormatJsonException)
        {
            Assert.That(repo.History.SessionList, Is.Empty);
        }
    }

    [Test]
    public void FolderEmpty()
    {
        
        ISessionMapper mapperOne = new SessionMapperOne();
        var history = new History();
        const string path = @"..\..\..\resources\Empty";
        IRepository repo = new JsonRepository(mapperOne, history, path);
        try
        {
            repo.LoadHistory();
            Assert.Fail();
        }
        catch (EmptyJsonFileException)
        {
            Assert.That(repo.History.SessionList, Is.Empty);
        }
    }
    [Test]
    public void FileEmpty()
    {
        
        ISessionMapper mapperOne = new SessionMapperOne();
        var history = new History();
        const string path = @"..\..\..\resources\FileEmpty";
        IRepository repo = new JsonRepository(mapperOne, history, path);
        try
        {
            repo.LoadHistory();
            Assert.Fail();
        }
        catch (EmptyJsonFileException)
        {
            Assert.That(repo.History.SessionList, Is.Empty);
        }
       
    }
}