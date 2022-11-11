using GBReaderHanusH.Domains.Domains;

namespace GBReaderHanusH.Test.DomainsTest
{
    internal class DomainTest
    {

        [Test]
        public void UserTest()
        {
            var unitOne = "Tony";
            var unitTwo = "Alves";
            User user = new (unitOne, unitTwo);
            Assert.Multiple(() =>
            {
                Assert.That(user.Firstname, Is.EqualTo(unitOne));
                Assert.That(user.Name, Is.EqualTo(unitTwo));
            });
        }

        [Test]
        public void PageTest()
        {
            var text = "Text from book";
            var textC = "TextfromChoice";
            Choice choice = new(textC, 1);
            IList<Choice> choices = new List<Choice>
            {
                choice
            };
            choice = new(textC, 2);
            choices.Add(choice);

            Page page = new (text, choices);
            Assert.Multiple(() =>
            {
                Assert.That(page.Text, Is.EqualTo(text));
                Assert.That(page.Choices, Is.EqualTo(choices));
            });
        }

        [Test]
        public void ChoiceTest()
        {
            var text = "Go to page 2";
            var goTo = 2;

            Choice choice = new (text, goTo);
            Assert.Multiple(() =>
            {
                Assert.That(text, Is.EqualTo(choice.Text));
                Assert.That(goTo, Is.EqualTo(choice.GoTo));
            });
        }

        [Test]
        public void GamebookTest()
        {
            var text = "Title";
            var resume = "Resume";
            var isbn = "1-151-151";
            var unitOne = "Tony";
            var unitTwo = "Alves";
            User user = new (unitOne, unitTwo);
            var textE = "Text from book";
            var textC = "TextfromChoice";
            Choice choice = new(textC, 1);
            IList<Choice> choices = new List<Choice>
            {
                choice
            };
            choice = new(textC, 2);
            choices.Add(choice);

            Page page = new(textE, choices);
            IDictionary<int,Page> listP=new Dictionary<int,Page>();
            listP.Add(1, page);
            listP.Add(2, page);

            GameBook gb = new GameBook(text, resume, isbn, user, listP);

            Assert.Multiple(() =>
            {
                Assert.That(gb.Title, Is.EqualTo(text));
                Assert.That(gb.Resume, Is.EqualTo(resume));
                Assert.That(gb.Isbn, Is.EqualTo(isbn));
                Assert.That(gb.User, Is.EqualTo(user));
                Assert.That(gb.Pages, Is.EqualTo(listP));
                Assert.That(gb.GetFirstPage(), Is.EqualTo(listP.First().Key));
                Assert.That(gb.ToString(), Is.EqualTo(isbn + "|" + text + "|" + user.ToString() + "|" + resume));
                Assert.That(gb.ToDetail(), Is.EqualTo("Auteur: " + user.ToString() + "\n\nISBN: " + isbn + "\n\nTitre: " + text + "\n\nRésumé: " + resume));

            });
        }
        [Test]
        public void SessionTest()
        {
            DateTime date = DateTime.Now;
            Session session = new Session(date, date, "title", 1);
            Assert.Multiple(() =>
            {
                Assert.That(session.Begin, Is.EqualTo(date));
                Assert.That(session.LastUpdate, Is.EqualTo(date));
                Assert.That(session.Title, Is.EqualTo("title"));
                Assert.That(session.Page, Is.EqualTo(1));
                Assert.That(session.ToDetail, Is.EqualTo($"title | {date} | {date}"));

            });
        }

        [Test]
        public void HistoryTest()
        {
            DateTime date = DateTime.Now;
            Session session = new Session(date, date, "title", 1);
            History history = new();
            IDictionary<string, Session> dico = new Dictionary<string, Session>
            {
                { "11-55-66", session },
                { "121-511-65646", session }
            };
            history.SessionList = dico;

            Assert.Multiple(() =>
            {
                Assert.That(history.SessionList, Is.EqualTo(dico));
                Assert.That(history.ContainsIsbn("11-55-66"), Is.EqualTo(true));
                Assert.That(history.ContainsIsbn("11-55-6"), Is.EqualTo(false));
                Assert.That(history.GetPageOfIsbn("11-55-66"), Is.EqualTo(1));
            });
            history.AddSession("11-55-99", session);
            Assert.That(history.SessionList.Count(), Is.EqualTo(3));
            history.DeleteReadingSession("11-55-99");
            Assert.That(history.SessionList.Count(), Is.EqualTo(2));
            date = DateTime.Now;
            history.UpdateReadingSession("11-55-66", 2, date);
            Assert.Multiple(() =>
            {
                Assert.That(history.SessionList["11-55-66"].Page, Is.EqualTo(2));
                Assert.That(history.SessionList["11-55-66"].LastUpdate, Is.EqualTo(date));
            });
        }
    }
}
