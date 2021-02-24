using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientFactoryDemo
{
	public class AssemblyAiService
	{
		public HttpClient Client { get; }

		public AssemblyAiService(HttpClient client)
		{
			client.BaseAddress = new Uri("https://api.assemblyai.com/");
			client.DefaultRequestHeaders.Add("Authorization", "YOUR_ASSEMBLY_AI_TOKEN");

			Client = client;
		}

		public async Task<string> UploadAudioFile(StringContent payload)
		{
			HttpResponseMessage response = await Client.PostAsync("v2/transcript", payload);

			string responseJson = await response.Content.ReadAsStringAsync();

			return responseJson;
		}
	}
}
