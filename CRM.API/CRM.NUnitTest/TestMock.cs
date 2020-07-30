using CRM.Data;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.NUnitTest
{
    class TestLead : ILeadRepository<BaseTestServerFixture>
    {
            private readonly BaseTestServerFixture _fixture;

            public TestLead(BaseTestServerFixture fixture)
            {
                _fixture = fixture;
            }

        [Fact]
        public async Task Get_ShouldReturnListResult()
        {
            // Arrange
            var response = await _fixture.Client.GetAsync("/Lead/256");
            response.EnsureSuccessStatusCode();
            var models = JsonConvert.DeserializeObject<IEnumerable<ILeadRepository>>(await response.Content.ReadAsStringAsync());
            // Assert
            Assert.IsNotEmpty(models);
        }

    }
}
