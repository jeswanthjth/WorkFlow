using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WorkFlow.Controllers;
using WorkFlow.Model;

namespace WorkFlow.Tests
{
    [TestFixture]
    public class WorkControllerTests
    {
        private workController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new workController();
        }

        [Test]
        public void Create_ShouldAddWorkSuccessfully()
        {
            var newWork = new WorkInfo
            {
                Employee = "Jeswanth",
                Role = "Intern",
                Reporting = "Gowtham"
            };

            var result = _controller.Create(newWork);
            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);

            var createdWork = (result.Result as CreatedAtActionResult).Value as WorkInfo;
            Assert.AreEqual("Jeswanth", createdWork.Employee);
        }

        [Test]
        public void GetById_ShouldReturnNotFound_WhenWorkDoesNotExist()
        {
            var result = _controller.GetById(999);
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public void Update_ShouldModifyWorkSuccessfully()
        {
            var created = _controller.Create(new WorkInfo
            {
                Employee = "Old Name",
                Role = "Intern",
                Reporting = "Gowtham"
            });

            var work = (created.Result as CreatedAtActionResult).Value as WorkInfo;

            var updatedWork = new WorkInfo
            {
                Employee = "New Name",
                Role = "Software Engineer",
                Reporting = "Karthic"
            };

            var result = _controller.Update(work.Id, updatedWork);
            Assert.IsInstanceOf<NoContentResult>(result);

            var updated = _controller.GetById(work.Id);
            var updatedValue = (updated.Result as OkObjectResult).Value as WorkInfo;
            Assert.AreEqual("New Name", updatedValue.Employee);
        }

        [Test]
        public void Delete_ShouldRemoveWorkSuccessfully()
        {
            var created = _controller.Create(new WorkInfo
            {
                Employee = "ToDelete",
                Role = "Intern",
                Reporting = "Gowtham"
            });

            var work = (created.Result as CreatedAtActionResult).Value as WorkInfo;

            var result = _controller.Delete(work.Id);
            Assert.IsInstanceOf<NoContentResult>(result);

            var deleted = _controller.GetById(work.Id);
            Assert.IsInstanceOf<NotFoundResult>(deleted.Result);
        }
    }
}
