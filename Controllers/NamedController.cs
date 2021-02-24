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
	public class NamedController : Controller
	{
		private readonly IHttpClientFactory _clientFactory;

		public NamedController(IHttpClientFactory clientFactory)
		{
			_clientFactory = clientFactory;
		}

		[HttpPost]
		public async Task Post()
		{
			var json = new
			{
				audio_url = "https://s3-us-west-2.amazonaws.com/blog.assemblyai.com/audio/8-7-2018-post/7510.mp3"
			};

			string jsonString = JsonSerializer.Serialize(json);
			var payload =  new StringContent(jsonString, Encoding.UTF8, "application/json");

			var client = _clientFactory.CreateClient("AssemblyAIClient");

			var request = new HttpRequestMessage(HttpMethod.Post, string.Empty);

			var response = await client.SendAsync(request);			
			
			string responseJson = await response.Content.ReadAsStringAsync();
			
		}
	}
}
