using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq; // Thư viện xử lý JSON


namespace Maps.ApiProcessing
{
    internal class API
    {
        private const string apiKey = "2d1c165ac7212e11673b19dcab63557f";

        // Hàm lấy dữ liệu thời tiết từ OpenWeatherMap
        public static async Task<string> GetWeatherInfo(double lat, double lon)
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&units=metric&appid={apiKey}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    JObject data = JObject.Parse(json);

                    // Lấy nhiệt độ và độ ẩm
                    double temperature = (double)data["main"]["temp"];
                    int humidity = (int)data["main"]["humidity"];
                    string weatherDescription = (string)data["weather"][0]["description"];

                    return $" {temperature}°C • {humidity}% • {weatherDescription}";
                }
                else
                {
                    return "Không thể lấy dữ liệu thời tiết!";
                }
            }
        }

    }
}
