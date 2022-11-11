using System.Data;
using GBReaderHanusH.Repository.CustomException;
using GBReaderHanusH.Domains.Domains;

namespace GBReaderHanusH.Repository.Storage
{
    public class LibraryStorage : IDisposable, ILibraryStorage
    {
        private readonly IDbConnection _con;

        public LibraryStorage(IDbConnection con)
        {
            this._con = con;
        }
        public void Dispose() => _con.Dispose();

        public IList<string> SelectResumeGameBookPublish()
        {
            IList<string> list = new List<string>();
            string selectQuery = @"Select g.isbn,g.title,Concat(u.firstname,'.', u.lastname) as creator  FROM gamebook g JOIN utilisateur u on u.id_user = g.id_user WHERE g.is_publish=1";
            try
            {
                using var selectCommand = _con.CreateCommand();
                selectCommand.CommandText = selectQuery;
                IDataReader reader = selectCommand.ExecuteReader();
                while (reader.Read())
                {
                    var isbn = (string)reader["isbn"];
                    var title = (string)reader["title"];
                    var creator = (string)reader["creator"];
                    list.Add($"{isbn} | {title} | {creator}");
                }
                return list;
            } catch (InvalidOperationException e)
            {
                throw new ConnectionFailedException(e, "Erreur lors de la connection à la base de donnée");
            }
        }

        public string SelectResumeGameBookSelected(string isbnSeleted)
        {
            string selectQuery = @"Select g.title,g.resume,Concat(u.firstname,'.', u.lastname) as creator  FROM gamebook g JOIN utilisateur u on u.id_user = g.id_user WHERE g.is_publish=1 AND g.isbn=@isbn";
            try
            {
                using var selectCommand = _con.CreateCommand();
                selectCommand.CommandText = selectQuery;
                selectCommand.Parameters.Add(CreateParameter(selectCommand.CreateParameter(), "@isbn", isbnSeleted));
                IDataReader reader = selectCommand.ExecuteReader();
                if (reader.Read())
                {
                    return ($"ISBN:\n  {isbnSeleted}\nTitre:\n  {(string)reader["title"]}\nAuteur:\n  {(string)reader["creator"]}\nRésumé :\n  {(string)reader["resume"]}");
                }
                return "";
            }
            catch (InvalidOperationException e)
            {
                throw new ConnectionFailedException(e, "Erreur lors de la connection à la base de donnée");
            }
        }
        public IList<string> SelectResumeGameBookSearch(string search)
        {
            string selectQuery = @"Select g.isbn,g.title,g.resume,Concat(u.firstname,'.', u.lastname) as creator  FROM gamebook g JOIN utilisateur u on u.id_user = g.id_user WHERE g.is_publish=1 AND (g.title LIKE @search OR u.lastname LIKE @search  OR u.firstname LIKE @search)";
            try
            {
                IList<string> list = new List<string>();
                using var selectCommand = _con.CreateCommand();
                selectCommand.CommandText = selectQuery;
                selectCommand.Parameters.Add(CreateParameter(selectCommand.CreateParameter(), "@search", $"%{search}%"));
                IDataReader? reader = selectCommand.ExecuteReader();
                while (reader.Read())
                {
                    list.Add($"{(string)reader["isbn"]} | {(string)reader["title"]} | {(string)reader["creator"]}");
                }
                return list;
            }
            catch (InvalidOperationException e)
            {
                throw new ConnectionFailedException(e, "Erreur lors de la connection à la base de donnée");
            }
        }

        public GameBook LoadGameBook(string isbn)
        {
            string selectQuery = @"Select g.title,g.isbn,g.resume,u.firstname,u.lastname FROM gamebook g JOIN utilisateur u on u.id_user = g.id_user WHERE g.isbn=@isbn AND g.is_publish=1";
            try
            {
                using var selectCommand = _con.CreateCommand();
                selectCommand.CommandText = selectQuery;
                selectCommand.Parameters.Add(CreateParameter(selectCommand.CreateParameter(), "@isbn", $"{isbn}"));
                IDataReader reader = selectCommand.ExecuteReader();
                if (reader.Read())
                {
                    return new GameBook((string)reader["title"], (string)reader["resume"], (string)reader["isbn"], new User((string)reader["firstname"], (string)reader["lastname"]), new Dictionary<int, Page>());
                }
                else
                {
                    throw new CantSelectException("La Reqête non exécutée");
                }
            }
            catch (InvalidOperationException e)
            {
                throw new ConnectionFailedException(e, "Erreur lors de la connection à la base de donnée");
            }
        }

        public IList<int> LoadIdPageOfGameBook(string isbn)
        {
            IList<int> pages = new List<int>();
            string selectQuery = @"Select p.id_page from page p where id_gamebook=(Select g.id_gamebook from gamebook g where g.isbn=@isbn) ORDER BY p.numero ASC";
            try
            {
                using var selectCommand = _con.CreateCommand();
                selectCommand.CommandText = selectQuery;
                selectCommand.Parameters.Add(CreateParameter(selectCommand.CreateParameter(), "@isbn", $"{isbn}"));
                IDataReader reader = selectCommand.ExecuteReader();
                while (reader.Read())
                {
                    pages.Add((int)reader["id_page"]);
                }
                reader.Close();
                return pages;
            }
            catch (InvalidOperationException e)
            {
                throw new ConnectionFailedException(e, "Erreur lors de la connection à la base de donnée");
            }
        }

        public Page LoadPage(int idPage)
        {
           
            string selectQuery = @"Select p.text,p.numero from page p  where p.id_page=@idPage";
            try
            {
                using var selectCommand = _con.CreateCommand();
                selectCommand.CommandText = selectQuery;
                var param = selectCommand.CreateParameter();
                param.ParameterName = "@idPage";
                param.Value = idPage;
                param.DbType = DbType.Int32;
                selectCommand.Parameters.Add(param);
                IDataReader reader = selectCommand.ExecuteReader();
                if (reader.Read())
                {
                    var page = new Page((string)reader["text"], new List<Choice>());
                    reader.Close();
                    return page;
                }
                else
                {
                   
                    throw new  CantSelectException("La Reqête non exécutée");
                }
            }
            catch (InvalidOperationException e)
            {
                throw new ConnectionFailedException(e, "Erreur lors de la connection à la base de donnée");
            }
        }
        public int LoadNumberPage(int idPage)
        {

            string selectQuery = @"Select p.numero from page p  where p.id_page=@idPage";
            try
            {
                using var selectCommand = _con.CreateCommand();
                selectCommand.CommandText = selectQuery;
                selectCommand.Parameters.Add(CreateParameter(selectCommand.CreateParameter(), "@idPage", idPage, DbType.Int32));
                IDataReader reader = selectCommand.ExecuteReader();
                if (reader.Read())
                {
                    var numero = (int)reader["numero"];
                    reader.Close();
                    return numero;
                }
                else
                {
                    
                    throw new CantSelectException("La Reqête non exécutée"); ;
                }
            }
            catch (InvalidOperationException e)
            {
                throw new ConnectionFailedException(e, "Erreur lors de la connection à la base de donnée");
            }
        }

        public IList<Choice> LoadChoicesOfAPage(int idPage)
        {
            IList<Choice> list = new List<Choice>();
            string selectQuery = @"Select (SELECT pg.numero from page pg where pg.id_page=c.id_page_1) as pageDirection,c.texteChoix from choix c JOIN page p on p.id_page = c.id_page  where p.id_page=@idPage";
            try
            {
                using var selectCommand = _con.CreateCommand();
                selectCommand.CommandText = selectQuery;
                selectCommand.Parameters.Add(CreateParameter(selectCommand.CreateParameter(), "@idPage", idPage, DbType.Int32));
                IDataReader reader = selectCommand.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Choice((string)reader["texteChoix"], (int)reader["pageDirection"]));
                }
                reader.Close();
                return list;
            }
            catch (InvalidOperationException e)
            {
                throw new ConnectionFailedException(e, "Erreur lors de la connection à la base de donnée");
            }
        }
        private IDbDataParameter CreateParameter(IDbDataParameter createParameter,
           string name, object value, DbType dbType = DbType.String)
        {
            createParameter.ParameterName = name;
            createParameter.Value = value;
            createParameter.DbType = dbType;
            return createParameter;
        }

    }
}
