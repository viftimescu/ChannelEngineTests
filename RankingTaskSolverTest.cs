using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ChannelEngineConsoleApp.Controllers;
using ChannelEngineConsoleApp.Data;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace ChannelEngineTests {
    [TestClass]
    public class RankingTaskSolverTest {
        static string API_PATH = "https://api-dev.channelengine.net/api/";
        static string API_KEY = "541b989ef78ccb1bad630ea5b85c6ebff9ca3322";

        static Mock<DataController> dataMock;
        static RankingTaskSolver solver;
        static List<RankingProduct> list1;
        static List<RankingProduct> result1;

        [TestInitialize]
        public void TestInitialize() {
            dataMock = new Mock<DataController>(API_PATH, API_KEY);

            RankingProduct rp1 = new RankingProduct("rp1", "1111", 2);
            RankingProduct rp2 = new RankingProduct("rp2", "2222", 2);
            RankingProduct rp3 = new RankingProduct("rp3", "3333", 1);
            RankingProduct rp4 = new RankingProduct("rp2", "2222", 2);
            RankingProduct rp5 = new RankingProduct("rp4", "4444", 2);
            RankingProduct rp6 = new RankingProduct("rp3", "3333", 1);
            RankingProduct rp7 = new RankingProduct("rp6", "6666", 3);
            list1 = new List<RankingProduct> { rp1, rp2, rp3, rp4, rp5, rp6, rp7 };

            RankingProduct p2 = new RankingProduct("rp2", "2222", 4);
            RankingProduct p7 = new RankingProduct("rp6", "6666", 3);
            RankingProduct p1 = new RankingProduct("rp1", "1111", 2);
            RankingProduct p3 = new RankingProduct("rp3", "3333", 2);
            RankingProduct p5 = new RankingProduct("rp4", "4444", 2);
            result1 = new List<RankingProduct> { p2, p7, p1, p3, p5 };
        }

        [TestMethod]
        public void BasicTest() {
            dataMock.Setup(d => d.GetRankingProducts().Result).Returns(list1);
            solver = new RankingTaskSolver(dataMock.Object);
            solver.SolveTask();

            foreach (var p in solver.Top5Products)
                Console.WriteLine(p.Name);
            Console.WriteLine();
            foreach (var p in result1)
                Console.WriteLine(p.Name);

            Assert.IsTrue(result1.SequenceEqual(solver.Top5Products));
        }
    }
}
