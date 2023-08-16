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
            string isbnOne = "2-190533-04-X";
            string isbnTwo = "2-190533-08-2";
            string isbnThree = "2-190533-06-6";
            DateTime dateOne = new DateTime(2000, 9, 2, 12, 25,23);
            DateTime dateTwo = new DateTime(2001, 9, 4, 11, 15,32);
            Session session = new Session(dateOne, dateOne, "title", 1);
            Session sessionTwo = new Session(dateOne, dateTwo, "title", 1);
            Stack<int> previous = new Stack<int>();
            previous.Push(1);
            previous.Push(2);
            previous.Push(4);
            History history = new();
            IDictionary<string, Session> dico = new Dictionary<string, Session>
            {
                { isbnOne, session },
                {isbnTwo,sessionTwo}
            };
            history.SessionList = dico;
            history.PreviousPage = previous;
            Assert.Multiple(() =>
            {
                Assert.That(history.SessionList, Is.EqualTo(dico));
                Assert.That(history.ContainsIsbn(isbnOne), Is.EqualTo(true));
                Assert.That(history.ContainsIsbn(isbnThree), Is.EqualTo(false));
                Assert.That(history.GetPageOfIsbn(isbnOne), Is.EqualTo(1));
            });
            history.AddSession(isbnThree, sessionTwo);
            Assert.That(history.SessionList.Count(), Is.EqualTo(3));
            history.DeleteReadingSession(isbnOne);
            Assert.That(history.SessionList.Count(), Is.EqualTo(2));
            history.UpdateReadingSession(isbnTwo, 2,"title", dateOne);
            Assert.Multiple(() =>
            {
                Assert.That(history.SessionList[isbnTwo].Page, Is.EqualTo(2));
                Assert.That(history.SessionList[isbnTwo].LastUpdate, Is.EqualTo(dateOne));
            });
            
            history.ResetReadingSession(isbnTwo,dateOne);
            Assert.Multiple((() =>
            {
                Assert.That(history.SessionList[isbnTwo].LastUpdate,Is.EqualTo(dateOne));
                Assert.That(history.SessionList[isbnTwo].Begin,Is.EqualTo(dateOne));
            }));
            history.ClearPreviousPage();
            Assert.That(history.PreviousPage.Count,Is.EqualTo(0));
        }
    }
}
