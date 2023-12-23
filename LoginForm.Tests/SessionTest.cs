using LoginForm.Interfaces;
using LoginForm.Model;
using LoginForm.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginForm.Tests
{
    public class SessionTest
    {
        ISessionRepository sessionRepository;
        [SetUp]
        public void Setup()
        {
            sessionRepository = new SessionRepository();
        }
        [Test]
        public void IncorrectSessionSearch()
        {
            DateTime date = DateTime.Now;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            const int length = 10;
            string name = new string(Enumerable.Repeat(chars, length).Select(s => s[new Random().Next(s.Length)]).ToArray());

            SessionModel result = sessionRepository.SearchSession(name, date);

            Assert.IsNull(result);
        }
        [Test]
        public void SessionSearch_Null()
        {
            DateTime date = DateTime.Now;

            SessionModel result = sessionRepository.SearchSession("", date);

            Assert.IsNull(result);
        }
    }
}
