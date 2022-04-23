using Newtonsoft.Json;
using RealWorldApp.Models;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Net.Http.Headers;
using System.Collections.Generic;
using UnixTimeStamp;
using System.Collections.ObjectModel;

namespace RealWorldApp.Services
{
    public static class ApiService
    {
        #region AUTHORIZATION

        // Register
        public async static Task<bool> RegisterUser(string name, string email, string password)
        {
            var register = new Register()
            {
                Name = name,
                Email = email,
                Password = password
            };

            using (var httpClient = new HttpClient())
            {             
                var json = JsonConvert.SerializeObject(register);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{AppSettings.ApiUrl}/api/Accounts/Register", content);
                if (!response.IsSuccessStatusCode) return false;
            }
                     
            return true;
        }

        // Login
        public static async Task<bool> Login(string email, string password)
        {
            var login = new Login()
            {
                Email = email,
                Password = password
            };

            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(login);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{AppSettings.ApiUrl}/api/Accounts/Login", content);

            if (!response.IsSuccessStatusCode) return false;

            // Get Response Here
            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Token>(jsonResult);

            // Store Token + Userid In Device Local Storage
            Preferences.Set(Constants.Preference.AccessToken, result.access_token);
            Preferences.Set(Constants.Preference.UserId, result.user_Id);
            Preferences.Set(Constants.Preference.UserName, result.user_name);
            Preferences.Set(Constants.Preference.TokenExpirationTime, result.expiration_Time);
            Preferences.Set(Constants.Preference.CurrentTime, UnixTime.GetCurrentTime());

            return true;

        }
        #endregion

        #region GET

        // GET All Categories
        public static async Task<List<Category>> GetCategories()
        {
            using (var httpClient = new  HttpClient())
            {
                // Check Token Expiration
                await TokenValidator.CheckTokenValidity();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
                var response = await httpClient.GetStringAsync($"{AppSettings.ApiUrl}/api/Categories");
                var json = JsonConvert.DeserializeObject<List<Category>>(response);
                return json;
            }
        }

        // GET Product By Id 
        public static async Task<Product> GetProductById(int productId)
        {
            using (var httpClient = new HttpClient())
            {
                // Check Token Expiration
                await TokenValidator.CheckTokenValidity();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
                var response = await httpClient.GetStringAsync($"{AppSettings.ApiUrl}/api/Products/{productId}");
                var json = JsonConvert.DeserializeObject<Product>(response);
                return json;
            }
        }

        // GET Products By Category
        public static async Task<List<ProductByCategory>> GetProductByCategory(int categoryId)
        {
            using (var httpClient = new HttpClient())
            {
                // Check Token Expiration
                await TokenValidator.CheckTokenValidity();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
                var response = await httpClient.GetStringAsync($"{AppSettings.ApiUrl}/api/Products/ProductsByCategory/{categoryId}");
                var json = JsonConvert.DeserializeObject<List<ProductByCategory>>(response);
                return json;
            }
        }

        // GET Popular Products
        public static async Task<List<PopularProduct>> GetPopularProducts()
        {
            using (var httpClient = new HttpClient())
            {
                // Check Token Expiration
                await TokenValidator.CheckTokenValidity();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get(Constants.Preference.AccessToken, string.Empty));
                var response = await httpClient.GetStringAsync($"{AppSettings.ApiUrl}/api/Products/PopularProducts");
                var json = JsonConvert.DeserializeObject<List<PopularProduct>>(response);
                return json;
            }
        }

        // GET Cart SubToatal 
        public static async Task<CartSubTotal> GetCartSubTotal(int userId)
        {
            using (var httpClient = new HttpClient())
            {
                // Check Token Expiration
                await TokenValidator.CheckTokenValidity();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
                var response = await httpClient.GetStringAsync($"{AppSettings.ApiUrl}/api/ShoppingCartItems/SubTotal/{userId}");
                var json = JsonConvert.DeserializeObject<CartSubTotal>(response);
                return json;
            }
        }

        // GET Shopping Cart Item
        public static async Task<List<ShoppingCartItem>> GetShoppingCartItems(int userId)
        {
            using (var httpClient = new HttpClient())
            {
                // Check Token Expiration
                await TokenValidator.CheckTokenValidity();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
                var response = await httpClient.GetStringAsync($"{AppSettings.ApiUrl}/api/ShoppingCartItems/{userId}");
                var json = JsonConvert.DeserializeObject<List<ShoppingCartItem>>(response);
                return json;
            }
        }

        // GET Total Cart Items 
        public static async Task<TotalCartItem> GetTotalCartItems(int userId)
        {
            using (var httpClient = new HttpClient())
            {
                // Check Token Expiration
                await TokenValidator.CheckTokenValidity();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
                var response = await httpClient.GetStringAsync($"{AppSettings.ApiUrl}/api/ShoppingCartItems/TotalItems/{userId}");
                var json = JsonConvert.DeserializeObject<TotalCartItem>(response);
                return json;
            }
        }

        // GET Orders By User 
        public static async Task<List<OrderByUser>> GetOrdersByUser(int userId)
        {
            using (var httpClient = new HttpClient())
            {
                // Check Token Expiration
                await TokenValidator.CheckTokenValidity();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
                var response = await httpClient.GetStringAsync($"{AppSettings.ApiUrl}/api/Orders/OrdersByUser/{userId}");
                var json = JsonConvert.DeserializeObject<List<OrderByUser>>(response);
                return json;
            }
        }

        // GET Orders Details
        public static async Task<List<Order>> GetOrderDetails(int orderId)
        {
            using (var httpClient = new HttpClient())
            {
                // Check Token Expiration
                await TokenValidator.CheckTokenValidity();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
                var response = await httpClient.GetStringAsync($"{AppSettings.ApiUrl}/api/Orders/OrderDetails/{orderId}");
                var json = JsonConvert.DeserializeObject<List<Order>>(response);
                return json;
            }
        }

        // Get Main Selection Dishes
        public static async Task<List<MainDishes>> GetMainDishSelection()
        {
            using (var httpClient = new HttpClient())
            {
                // Check Token Expiration
                await TokenValidator.CheckTokenValidity();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get(Constants.Preference.AccessToken, string.Empty));
                var response = await httpClient.GetStringAsync($"{AppSettings.ApiUrl}/api/SideDishes");
                var json = JsonConvert.DeserializeObject<List<MainDishes>>(response);
                return json;
            }
        }

        // Get Side Dishes Selection 
        public static async Task<List<SideDish>> GetSideDishSelection(int mainDishId)
        {
            using (var httpClient = new HttpClient())
            {
                // Check Token Expiration
                await TokenValidator.CheckTokenValidity();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get(Constants.Preference.AccessToken, string.Empty));
                var response = await httpClient.GetStringAsync($"{AppSettings.ApiUrl}/api/SideDishes/SideDishesByMainDishId/{mainDishId}");
                var json = JsonConvert.DeserializeObject<List<SideDish>>(response);
                return json;
            }
        }

        #endregion

        #region POST

        // Add Item To Cart
        public static async Task<bool> AddItemsInCart(AddToCart addToCart)
        {       
            using (var httpClient = new HttpClient())
            {
                // Check Token Expiration
                await TokenValidator.CheckTokenValidity();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
                var json = JsonConvert.SerializeObject(addToCart);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{AppSettings.ApiUrl}/api/ShoppingCartItems", content);
                if (!response.IsSuccessStatusCode) return false;
            }

            return true;
        }

        // Place Order
        public static async Task<OrderResponse> PlaceOrder(Order order)
        {
            using (var httpClient = new HttpClient())
            {
                // Check Token Expiration
                await TokenValidator.CheckTokenValidity();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
                var json = JsonConvert.SerializeObject(order);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{AppSettings.ApiUrl}/api/Orders", content);

                // Get Response Here
                var jsonResult = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<OrderResponse>(jsonResult);
                return result;
            }

            
        }


        #endregion

        #region DELETE

        // Delete Shopping Cart
        public static async Task<bool> ClearShoppingCart(int userId)
        {
            using (var httpClient = new HttpClient())
            {
                // Check Token Expiration
                await TokenValidator.CheckTokenValidity();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
                var response = await httpClient.DeleteAsync($"{AppSettings.ApiUrl}/api/ShoppingCartItems/{userId}");
                if (!response.IsSuccessStatusCode) return false;
                return true;
            }
        }
        #endregion
    }

    public static class TokenValidator
    {
        // Check If Token Expire
        public async static Task CheckTokenValidity() 
        {
            var expirationTime = Preferences.Get("tokenExpirationTime", 0);
            Preferences.Set("currentTime", UnixTime.GetCurrentTime());
            var currentTime = Preferences.Get("currentTime", 0);

            // If We Need To Get New Token
            if (expirationTime < currentTime)
            {
                var email = Preferences.Get("email", string.Empty);
                var password = Preferences.Get("password", string.Empty);
                await ApiService.Login(email, password);
            }
        }
    }
}
