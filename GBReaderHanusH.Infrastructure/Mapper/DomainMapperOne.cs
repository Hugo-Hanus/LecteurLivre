using GBReaderHanusH.Domains.Domains;
using GBReaderHanusH.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBReaderHanusH.Infrastructure.Mapper
{
    public class DomainMapperOne : IDomainMapper
    {
        public Choice DtoChoiceToChoice(DtoChoice dto) => new Choice(dto.Texte, dto.GoTo);
        public GameBook DtoGamebookToGameBook(DtoGameBook dto)
        {
            IDictionary<int,Page> pages=new Dictionary<int,Page>();
            foreach(var page in dto.Pages)
            {
                pages.Add(page.Key, DtoPageToPage(page.Value));
            }

            return new GameBook(dto.Title, dto.Resume, dto.Isbn, DtoUserToUser(dto.UserDto), pages);
        }
        public Page DtoPageToPage(DtoPage dto)
        {
            IList<Choice> choices=new List<Choice>();
            foreach(DtoChoice choice in dto.Choices)
            {
                choices.Add(DtoChoiceToChoice(choice));
            }
            return new Page(dto.Texte, choices);
        }
        public User DtoUserToUser(DtoUser dto) => new User(dto.Firstname, dto.Name);
    }
}
