using ErgoCalc;
using ErgoCalc.Models.LibertyMutual;

namespace ErgoCalc.UnitTest;

[TestClass]
public class LibertyMutualTest
{
    [TestMethod]
    public void RandomData()
    {
        // Arrange
        Job _job = new()
        {
            NumberTasks = 2,
            Tasks = new ModelLiberty[2]
        };

        _job.Tasks[0] = new();
        _job.Tasks[0].Data.Type = TaskType.Lifting;
        _job.Tasks[0].Data.Gender = Gender.Female;
        _job.Tasks[0].Data.HorzReach = 0.38;
        _job.Tasks[0].Data.VertRangeM = 1.03;
        _job.Tasks[0].Data.DistHorz = 0;
        _job.Tasks[0].Data.DistVert = 0.51;
        _job.Tasks[0].Data.VertHeight = 0;
        _job.Tasks[0].Data.Freq = 1;

        _job.Tasks[1] = new();
        _job.Tasks[1].Data.Type = TaskType.Pulling;
        _job.Tasks[1].Data.Gender = Gender.Male;
        _job.Tasks[1].Data.HorzReach = 0;
        _job.Tasks[1].Data.VertRangeM = 0;
        _job.Tasks[1].Data.DistHorz = 20;
        _job.Tasks[1].Data.DistVert = 0;
        _job.Tasks[1].Data.VertHeight = 1.2;
        _job.Tasks[1].Data.Freq = 1;

        // Act
        bool result = LibertyMutual.LibertyMutualMMH(_job);

        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual(0.26, _job.Tasks[0].Initial.CV, 0.001);
        Assert.AreEqual(0, _job.Tasks[0].Initial.DH);
        Assert.AreEqual(0.90739279538250583, _job.Tasks[0].Initial.DV, 0.0000000000000001);
        Assert.AreEqual(0.6767, _job.Tasks[0].Initial.F);
        Assert.AreEqual(0.76579458756180063, _job.Tasks[0].Initial.H, 0.0000000000000001);
        Assert.AreEqual(15.55552892693037, _job.Tasks[0].Initial.MAL, 0.0000000000000001);
        Assert.AreEqual(12.827597273706141, _job.Tasks[0].Initial.MAL75, 0.0000000000000001);
        Assert.AreEqual(10.372373690143064, _job.Tasks[0].Initial.MAL90, 0.0000000000000001);
        Assert.AreEqual(34.9, _job.Tasks[0].Initial.RL);
        Assert.AreEqual(0, _job.Tasks[0].Initial.V);
        Assert.AreEqual(0.94788480222954741, _job.Tasks[0].Initial.VRM, 0.0000000000000001);

        Assert.AreEqual(0.238, _job.Tasks[1].Initial.CV, 0.001);
        Assert.AreEqual(0.76003361652960055, _job.Tasks[1].Initial.DH, 0.0000000000000001);
        Assert.AreEqual(0, _job.Tasks[1].Initial.DV);
        Assert.AreEqual(0.6281, _job.Tasks[1].Initial.F);
        Assert.AreEqual(0, _job.Tasks[1].Initial.H);
        Assert.AreEqual(22.909828065410867, _job.Tasks[1].Initial.MAL, 0.0000000000000001);
        Assert.AreEqual(19.232146343698815, _job.Tasks[1].Initial.MAL75, 0.0000000000000001);
        Assert.AreEqual(15.922118071797655, _job.Tasks[1].Initial.MAL90, 0.0000000000000001);
        Assert.AreEqual(69.8, _job.Tasks[1].Initial.RL);
        Assert.AreEqual(0.68755083236546632, _job.Tasks[1].Initial.V);
        Assert.AreEqual(0, _job.Tasks[1].Initial.VRM);

        Assert.AreEqual(0.257, _job.Tasks[1].Sustained.CV, 0.001);
        Assert.AreEqual(0.68568517802594264, _job.Tasks[1].Sustained.DH, 0.0000000000000001);
        Assert.AreEqual(0, _job.Tasks[1].Sustained.DV);
        Assert.AreEqual(0.4896, _job.Tasks[1].Sustained.F);
        Assert.AreEqual(0, _job.Tasks[1].Sustained.H);
        Assert.AreEqual(13.771820471847821, _job.Tasks[1].Sustained.MAL, 0.0000000000000001);
        Assert.AreEqual(11.384559872148726, _job.Tasks[1].Sustained.MAL75, 0.0000000000000001);
        Assert.AreEqual(9.2359508637212127, _job.Tasks[1].Sustained.MAL90, 0.0000000000000001);
        Assert.AreEqual(61, _job.Tasks[1].Sustained.RL);
        Assert.AreEqual(0.67250473544362177, _job.Tasks[1].Sustained.V);
        Assert.AreEqual(0, _job.Tasks[1].Sustained.VRM);

    }

    [TestMethod]
    public void ExampleNTP()
    {

    }
}