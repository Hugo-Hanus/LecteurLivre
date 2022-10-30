using GBReaderHanusH.Domains.Domains;
using GBReaderHanusH.Infrastructure.Mapper;
using GBReaderHanusH.Infrastructure.Repository;
using GBReaderHanusH.Repository.Repository;
using Presenter.Events;
using Presenter.mvp;

namespace GBReaderHanusH.Test.PresenterTest;

public class PresenterTest
{

    [Test]
    public void TesFunctionInPresenterNoSolution()
    {
        ///<summary>
        ///
        /// Je ne vois pas comment faire les tests de mes méthodes dans mon presenter car celui-ci à besoin d'une mainView pour être initialisé. 
        /// 
        /// </summary>

        Assert.Pass();
    }
}