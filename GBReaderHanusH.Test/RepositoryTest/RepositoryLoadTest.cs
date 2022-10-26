using GBReaderHanusH.Domains.Domains;
using GBReaderHanusH.Infrastructure.Mapper;
using GBReaderHanusH.Infrastructure.Repository;
using GBReaderHanusH.Repository.Repository;

namespace GBReaderHanusH.Test.RepositoryTest;

public class RepositoryLoadTest
{
    
    [Test]
    public void GoodSituationTwoBook()
    {
        
        IMapper mapperOne = new MapperOne();
        var library = new Library();
        const string path = @"..\..\..\resources\GoodSituation\190533.json";
        IRepository repo = new JsonRepository(mapperOne,library,path);
        Assert.That(repo.LoadBook(),Is.EqualTo(true));
        Assert.That(repo.Library.SizeOfLibrary(), Is.EqualTo(2));
    }
    [Test]
    public void NotGoodFormat()
    {
        
        IMapper mapperOne = new MapperOne();
        var library = new Library();
        const string path = @"..\..\..\resources\NotGoodFormat\190533.json";
        IRepository repo = new JsonRepository(mapperOne,library,path);
        Assert.That(repo.LoadBook(),Is.EqualTo(true));
        Assert.That(repo.Library.SizeOfLibrary(), Is.EqualTo(0));
    }
    [Test]
    public void FolderEmpty()
    {
        
        IMapper mapperOne = new MapperOne();
        var library = new Library();
        const string path = @"..\..\..\resources\Empty\190533.json";
        IRepository repo = new JsonRepository(mapperOne,library,path);
        Assert.That(repo.LoadBook(),Is.EqualTo(false));
        Assert.That(repo.Library.SizeOfLibrary(), Is.EqualTo(0));
    }
    [Test]
    public void FileEmpty()
    {
        
        IMapper mapperOne = new MapperOne();
        var library = new Library();
        const string path = @"..\..\..\resources\FileEmpty\190533.json";
        IRepository repo = new JsonRepository(mapperOne,library,path);
        Assert.That(repo.LoadBook(),Is.EqualTo(true));
        Assert.That(repo.Library.SizeOfLibrary(), Is.EqualTo(0));
    }
}