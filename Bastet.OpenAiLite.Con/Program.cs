using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Bastet.AiLite.OpenAi;
using Bastet.AiLite.OpenAi.Managers;
using Bastet.AiLite.OpenAi.Model.Chat;
using Newtonsoft.Json;

namespace Bastet.OpenAiLite.Con;

internal class Program
{
	//腾讯的已经测试成功了。下面来测试阿里，有个文件接口
	public static void TestTencent1()
	{
		var opt = new OpenAIOptions();
		opt.ApiKey = "sk-ACzlrn8r3JpjgRqqRFDfnQS2y5Twz9NUGfYvzEdPvnh1MOuc";
		opt.ApiVersion = "v1";
		opt.BaseDomain = "https://api.lkeap.cloud.tencent.com";
		opt.DefaultModel = "deepseek-r1";
		var client = new OpenAIService(opt);
		var req = new ChatCompletionCreateRequest();
		req.messages = new Collection<ChatMessage>();
		req.messages.Add(ChatMessage.FromUser("你好，你是谁"));
		var rsp = client.ChatCompletion.CreateCompletion(req).Result;
		return;
	}

	public static async Task TestTencent2()
	{
		var opt = new OpenAIOptions();
		opt.ApiKey = "sk-ACzlrn8r3JpjgRqqRFDfnQS2y5Twz9NUGfYvzEdPvnh1MOuc";
		opt.ApiVersion = "v1";
		opt.BaseDomain = "https://api.lkeap.cloud.tencent.com";
		opt.DefaultModel = "deepseek-r1";
		var client = new OpenAIService(opt);
		var req = new ChatCompletionCreateRequest();
		req.messages = new Collection<ChatMessage>();
		req.messages.Add(ChatMessage.FromUser("你好，你是谁"));
		var completionResult = client.ChatCompletion.CreateCompletionAsStream(req);
		await foreach (var completion in completionResult)
		{
			var xxx = JsonConvert.SerializeObject(completion);
			Console.WriteLine("收到消息:{0}", xxx);
		}

		return;
	}

	public static void TestAli()
	{
	}


	public static void Main(string[] args)
	{
		TestTencent2().Wait();
	}
}