using GBReaderHanusH.Domains.Domains;
using GBReaderHanusH.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBReaderHanusH.Infrastructure.Mapper
{
    public interface IDomainMapper
    {
        GameBook DtoGamebookToGameBook(DtoGameBook dto);


        User DtoUserToUser(DtoUser dto);

        Page DtoPageToPage(DtoPage dto);

        Choice DtoChoiceToChoice(DtoChoice dto);

    }
}
