using Checkout.Models.ViewModels;
using Newtonsoft.Json;
using Shouldly;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.IntegrationTests.Api
{
    public class BasketApiTests : IntegrationTestServerProvider
    {
        [Fact]
        public async Task Create_ShouldReturn200WithBasketData()
        {
            using (var client = Server.CreateClient())
            {
                // Given
                var endpoint = "api/basket";

                // When
                var result = await client.PostAsync(endpoint, null);

                // Then
                result.StatusCode.ShouldBe(HttpStatusCode.OK);
                var json = await result.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<BasketViewModel>(json);
                model.ShouldNotBeNull();
            }
        }

        [Fact]
        public async Task Get_ShouldReturn200WithBasketData_WhenBasketExists()
        {
            
            using (var client = Server.CreateClient())
            {
                #region Have to create one first due to in memory storage
                var endpoint = "api/basket";
                var result = await client.PostAsync(endpoint, null);
                var json = await result.Content.ReadAsStringAsync();
                var basketId = JsonConvert.DeserializeObject<BasketViewModel>(json).BasketId;
                #endregion

                // Given
                endpoint = $"api/basket/{basketId}";

                // When
                result = await client.GetAsync(endpoint);

                // Then
                result.StatusCode.ShouldBe(HttpStatusCode.OK);
                json = await result.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<BasketViewModel>(json);
                model.ShouldNotBeNull();
            }
        }

        [Fact]
        public async Task Get_ShouldReturn404_WhenNoBasket()
        {
            using (var client = Server.CreateClient())
            {
                // Given
                var endpoint = $"api/basket/{Guid.NewGuid()}";

                // When
                var result = await client.GetAsync(endpoint);

                // Then
                result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
            }
        }

        [Fact]
        public async Task Clear_ShouldRemoveAllItemsFromBasket()
        {
            using (var client = Server.CreateClient())
            {
                #region Have to create one first due to in memory storage
                var endpoint = "api/basket";
                var result = await client.PostAsync(endpoint, null);
                var json = await result.Content.ReadAsStringAsync();
                var basketId = JsonConvert.DeserializeObject<BasketViewModel>(json).BasketId;
                endpoint = $"api/basket/{basketId}/add/{Guid.NewGuid()}";
                await client.PutAsync(endpoint, null);
                endpoint = $"api/basket/{basketId}/add/{Guid.NewGuid()}";
                await client.PutAsync(endpoint, null);
                #endregion

                // Given
                endpoint = $"api/basket/{basketId}/clear";

                // When
                result = await client.PutAsync(endpoint, null);

                // Then
                result.StatusCode.ShouldBe(HttpStatusCode.OK);
                json = await result.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<BasketViewModel>(json);
                model.Items.Any().ShouldBeFalse();
            }
        }

        [Fact]
        public async Task Clear_ShouldReturn404_WhenNoBasket()
        {
            using (var client = Server.CreateClient())
            {
                // Given
                var endpoint = $"api/basket/{Guid.NewGuid()}/Clear";

                // When
                var result = await client.GetAsync(endpoint);

                // Then
                result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
            }
        }

        [Fact]
        public async Task Add_ShouldAddItem_ToBasket()
        {
            using (var client = Server.CreateClient())
            {
                #region Have to create one first due to in memory storage
                var endpoint = "api/basket";
                var result = await client.PostAsync(endpoint, null);
                var json = await result.Content.ReadAsStringAsync();
                var basketId = JsonConvert.DeserializeObject<BasketViewModel>(json).BasketId;
                #endregion

                // Given
                var itemId = Guid.NewGuid();
                endpoint = $"api/basket/{basketId}/add/{itemId}";

                // When
                result = await client.PutAsync(endpoint, null);

                // Then
                result.StatusCode.ShouldBe(HttpStatusCode.OK);
                json = await result.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<BasketViewModel>(json);
                model.Items.Any(x => x.ItemId == itemId).ShouldBeTrue();
            }
        }

        [Fact]
        public async Task Add_ShouldReturn404_WhenNoBasket()
        {
            using (var client = Server.CreateClient())
            {
                // Given
                var endpoint = $"api/basket/{Guid.NewGuid()}/add/{Guid.NewGuid()}";

                // When
                var result = await client.PutAsync(endpoint, null);

                // Then
                result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
            }
        }
    }
}
