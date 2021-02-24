using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HttpClientFactoryDemo.Controllers
{
	[ApiController]
	public class BasicController : Controller
	{
		private readonly IHttpClientFactory _clientFactory;

		public BasicController(IHttpClientFactory clientFactory)
		{
			_clientFactory = clientFactory;
		}

		[HttpPost]
		public async Task Basic()
		{
			var json = new
			{
				audio_url = "https://s3-us-west-2.amazonaws.com/blog.assemblyai.com/audio/8-7-2018-post/7510.mp3"
			};

			string jsonString = JsonSerializer.Serialize(json);
			var payload =  new StringContent(jsonString, Encoding.UTF8, "application/json");

			var client = _clientFactory.CreateClient();
			client.DefaultRequestHeaders.Add("Authorization", "YOUR_ASSEMBLY_AI_TOKEN");

			HttpResponseMessage response = await client.PostAsync("https://api.assemblyai.com/v2/transcript", payload);			
			
			string responseJson = await response.Content.ReadAsStringAsync();
			
		}
	}
}
