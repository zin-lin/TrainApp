using TrainApp.BusinessLogic;
using TrainApp.Persistence;

namespace TestTrainApp
{
    [TestClass]
    public class TrainTest
    {
        /// <summary>
        /// Test If the Constructors works perfectly
        /// will succeed
        /// </summary>
        [TestMethod]
        public void TestTrainCreationBehaviour()
        {
            Train train = new Train("110", "GNER", 400, 100, "Edinburgh Waverley", "King's Cross", "19:30", "12/12/22", new string[] {"Newcastle", "York"});
            string expectedName = "GNER";
            string expectedId = "110";
            int expectedPreCap = 400; //wrong

            Assert.AreEqual(expectedName, train.Name);
            Assert.AreEqual(expectedId, train.Id);
            Assert.AreNotEqual(expectedPreCap, train.PreCap);
        }
        
        /// <summary>
        /// Test if Getters and Setters works perfectly
        /// will fail
        /// </summary>
        [TestMethod]
        public void TestTrainUpdateBehaviour()
        {
            Train train = new Train("110", "GNER", 400, 100, "Edinburgh Waverley", "King's Cross", "19:30", "12/12/22", new string[] { "Newcastle", "York" });
            train.Destination = "Edinburgh Waverley";
            Assert.AreEqual(train.Destination, "Edinburgh Waverley");
            Assert.AreEqual(train.Destination, "King's Cross", "It's updated, Train's not moving");
        }

    }
}