using Client.State;
using NUnit.Framework;
using System;

namespace NUnit_tests.State
{
    [TestFixture]
    public class StateContextTests
    {
        [Test]
        public void SetState()
        {
            var state_mock = new Moq.Mock<StateContext>();
            IState state = new PreparationState(null);

            state_mock.Object.setState(state);

            Assert.AreEqual(state_mock.Object.message, "Waiting for players");
        }

        [Test]
        public void NextState()
        {
            var state_mock = new Moq.Mock<StateContext>();
            IState state = new PreparationState(null);

            state_mock.Object.setState(state);
            state_mock.Object.NextState();

            Assert.AreEqual(state_mock.Object.message, "Players playing");
        }

        [Test]
        public void GetState()
        {
            var state_mock = new Moq.Mock<StateContext>();
            var stateContext = new StateContext();
            IState state = new PreparationState(null);

            state_mock.Object.setState(state);
            stateContext.setState(state);

            Assert.AreEqual(stateContext.getState(), state_mock.Object.getState());
        }
    }
}
