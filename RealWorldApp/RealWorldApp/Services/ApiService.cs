using Newtonsoft.Json;
using RealWorldApp.Models;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Net.Http.Headers;
using System.Collections.Generic;

namespace RealWorldApp.Services
{
    public class ApiService
    {
        #region AUTHORIZATION

        // Register
        public async Task<bool> RegisterUser(string name, string email, string password)
        {
            var register = new Register()
            {
                Name = name,
                Email = email,
                Password = password
            };

            using (var client = new HttpClient())
            {
                var httpClient = new HttpClient();
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
            Preferences.Set("accessToken", result.access_token);
            Preferences.Set("userId", result.user_Id);
            Preferences.Set("userName", result.user_name);

            return true;

        }
        #endregion

        #region GET

        // GET All Categories
        public static async Task<List<Category>> GetCategories()
        {
            using (var httpClient = new  HttpClient())
            {               
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
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
                var response = await httpClient.GetStringAsync($"{AppSettings.ApiUrl}/api/Products/PopularProducts");
                var json = JsonConvert.DeserializeObject<List<PopularProduct>>(response);
                return json;
            }
        }

        #endregion

        #region POST

        // Add Item To Cart
        public async Task<bool> AddItemsInCart(AddToCart addToCart)
        {       
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
                var json = JsonConvert.SerializeObject(addToCart);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{AppSettings.ApiUrl}/api/ShoppingCartItems", content);
                if (!response.IsSuccessStatusCode) return false;
            }

            return true;
        }

        #endregion
    }
}
