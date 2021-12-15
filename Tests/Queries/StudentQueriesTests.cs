using Domain.Entities;
using Domain.Enums;
using Domain.Queries;
using Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Queries
{
    [TestClass]
    public class StudentQueriesTests
    {
        private IList<Student> _students;
        public StudentQueriesTests()
        {
            for (int i = 0; i < 10; i++)
            {
                _students.Add(new Student(
                    new Name("Aluno", i.ToString()),
                    new Document("1111111111" + i.ToString(), EDocumentType.CPF),
                    new Email(i.ToString() + "@plataforma.io")));
            }
        }

        [TestMethod]
        public void ShouldReturnNullWhenDocumentNotExists()
        {
            var exp = StudentQueries.GetStudentInfo("12345678911");
            var student = _students.AsQueryable().Where(exp).FirstOrDefault();

            Assert.AreEqual(null, student);
        }

        [TestMethod]
        public void ShouldReturnNullWhenDocumentExists()
        {
            var exp = StudentQueries.GetStudentInfo("11111111111");
            var student = _students.AsQueryable().Where(exp).FirstOrDefault();

            Assert.AreNotEqual(null, student);
        }
    }
}
