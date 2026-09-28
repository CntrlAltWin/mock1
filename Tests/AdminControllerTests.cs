using Xunit;
using mock1.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace mock1.Tests
{
    public class AdminControllerTests
    {
        [Fact]
        public void Index_ReturnsViewResult()
        {
            // Arrange
            var controller = new AdminController();

            // Act
            var result = controller.Index();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Index_ReturnsViewWithNoModel()
        {
            // Arrange
            var controller = new AdminController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.Model);
        }

        [Fact]
        public void Index_ReturnsViewWithCorrectViewName()
        {
            // Arrange
            var controller = new AdminController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            // If ViewName is null, it defaults to "Index"
            Assert.Null(result.ViewName);
        }
    }
}
