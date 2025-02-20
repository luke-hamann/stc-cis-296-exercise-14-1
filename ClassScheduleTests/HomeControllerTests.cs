using Microsoft.AspNetCore.Http;

namespace ClassScheduleTests
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexActionMethod_ReturnsAViewResult()
        {
            // arrange
            var rep = new Mock<IClassScheduleUnitOfWork>();

            var days = new Mock<IRepository<Day>>();
            days.Setup(m => m.List(It.IsAny<QueryOptions<Day>>()))
                .Returns(new List<Day>());

            var classes = new Mock<IRepository<Class>>();
            classes.Setup(m => m.List(It.IsAny<QueryOptions<Class>>()))
                .Returns(new List<Class>());

            rep.Setup(m => m.Days).Returns(days.Object);
            rep.Setup(m => m.Classes).Returns(classes.Object);

            var controller = new HomeController(rep.Object);

            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Session = new Mock<ISession>().Object;

            // act
            var result = controller.Index(1);

            // assert
            Assert.IsType<ViewResult>(result);
        }
    }
}
