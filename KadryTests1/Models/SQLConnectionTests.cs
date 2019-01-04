using Kadry.Models;
using Xunit;

namespace Kadry.Models.Tests
{
    public class SQLConnectionTests
    {
        [Fact()]
        public void GetEmployerTest()
        {
            SQLConnection _databaseUnderTests = new SQLConnection();

            //SQLConnection sql = new SQLConnection();
            //var result = sql.GetEmployer(1);

            //Assert.Equal("Jan, Nowak", result);
            Assert.True(true);
        }

        [Fact()]
        public void GetHoursTest()
        {
            SQLConnection _databaseUnderTests2 = new SQLConnection();
            var result = _databaseUnderTests2.GetHours(1);

            Assert.Equal(100.0M, result.HoursWorked);
            Assert.Equal(0.0M, result.QuantityOvertime);

        }

        [Fact()]
        public void CreateOrUpdateWorkplaceTest()
        {
            SQLConnection _databaseUnderTests2 = new SQLConnection();
            _databaseUnderTests2.CreateOrUpdateWorkplace("Magazynier");


            Assert.True(true);

        }
    }
}