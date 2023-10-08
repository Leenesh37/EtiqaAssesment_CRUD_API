using Freelancer_API.Models;
using Newtonsoft.Json;
using System.Net;
using System.Security.Policy;
using System.Text;

namespace Freelancer_API
{
    public class APIGateway
    {
        private string url = "https://localhost:7269/api/Freelance";

        private HttpClient httpClient = new HttpClient();

        public List<Users> ListUsers()
        { 
             List<Users> users = new List<Users>();
             if(url.Trim().Substring(0, 5).ToLower() == "https")
                  ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                if(response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var datacol = JsonConvert.DeserializeObject<List<Users>>(result);
                    if (datacol != null)
                        users = datacol;
                }
                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error occured at the API Endpoint, Error Info. " + result);
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error occured at the API Endpoint, Error Info. " + ex.Message);
            }
            finally { }
            return users;
         }

        public Users CreateUser(Users users)
        {
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string json = JsonConvert.SerializeObject(users);
            try
            {
                HttpResponseMessage response = httpClient.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json")).Result; 
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<Users>(result);
                    if (data != null) 
                    users = data;
                }
                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error occured at the API Endpoint , Error Info : " + result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occred at the API Endpoint, Error Info " + ex.Message);
            }
            finally { }
            return users;
        }

        public Users GetUsers(int id)
        {
            Users users = new Users();
            url = url + "/" + id;
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<Users>(result);
                    if (data != null)
                        users = data;
                }
                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error occured at the API Endpoint , Error Info : " + result);
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error occred at the API Endpoint, Error Info " + ex.Message);
            }
            finally { }
            return users;
        }

        public void UpdateUsers(Users users)
        {
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            int id = users.id;

            url = url + "/" + id;
            string json = JsonConvert.SerializeObject(users);

            try
            {
                HttpResponseMessage response = httpClient.PutAsync(url, new StringContent(json, Encoding.UTF8, "application/json")).Result;
                if (!response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error occured at the API Endpoint , Error Info : " + result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occred at the API Endpoint, Error Info " + ex.Message);
            }
            finally { }

            return ;
         
        }

        public void DeleteUser(int id)
        {
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            url = url + "/" + id;

            try
            {
                HttpResponseMessage response = httpClient.DeleteAsync(url).Result;
                if(!response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error occured at the API Endpoint , Error Info : " + result);

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occred at the API Endpoint, Error Info " + ex.Message);
            }
            finally { }

            return;
        }
    }
}
