using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Helpers
{
    public static class MockHelper
    {
        public static void SetupMock<TMock, TInput, TOutput>(Mock<TMock> mock, Func<TMock, TInput, TOutput> setupMethod, TOutput returnValue)
            where TMock : class
        {
            mock.Setup(m => setupMethod(m, It.IsAny<TInput>())).Returns(returnValue);
        }
    }

}
