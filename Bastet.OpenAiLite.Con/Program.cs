using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Bastet.AiLite.OpenAi;
using Bastet.AiLite.OpenAi.Managers;
using Bastet.AiLite.OpenAi.Model.Chat;
using Bastet.AiLite.OpenAi.Model.File;
using Newtonsoft.Json;

namespace Bastet.OpenAiLite.Con;

//https://help.aliyun.com/zh/model-studio/getting-started/models

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

	public static void TestAli1()
	{
		//sk-35ed8c8d99bc47c8ae3dffc5895d9db9
		var opt = new OpenAIOptions();
		opt.ApiKey = "sk-35ed8c8d99bc47c8ae3dffc5895d9db9";
		opt.ApiVersion = "v1";
		opt.BaseDomain = "https://dashscope.aliyuncs.com/compatible-mode/";
		opt.DefaultModel = "qwen-plus";
		var client = new OpenAIService(opt);
		var req = new ChatCompletionCreateRequest();
		req.messages = new Collection<ChatMessage>();
		req.messages.Add(ChatMessage.FromUser("你好，你是谁"));
		var rsp = client.ChatCompletion.CreateCompletion(req).Result;
		return;
	}

	public static async Task TestAli2()
	{
		
		var client = CreateAliAiClient();
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

	public static OpenAIService CreateAliAiClient()
	{
		//sk-35ed8c8d99bc47c8ae3dffc5895d9db9
		var opt = new OpenAIOptions();
		opt.ApiKey = "sk-35ed8c8d99bc47c8ae3dffc5895d9db9";
		opt.ApiVersion = "v1";
		opt.BaseDomain = "https://dashscope.aliyuncs.com/compatible-mode/";
		opt.DefaultModel = "qwen-plus";
		var client = new OpenAIService(opt);
		return client;
	}

	public static void TestAliFile()
	{
		var client = CreateAliAiClient();
		var bindat = File.ReadAllBytes("d:\\test.txt");
		var rsp=client.Files.UploadFile(UploadFilePurposes.FileExtract,bindat,"test.txt").Result;
		return;
	}

	public static void TestAliFile2()
	{
		var client = CreateAliAiClient();
		var req = new ChatCompletionCreateRequest();
		req.messages = new Collection<ChatMessage>();
		req.messages.Add(ChatMessage.FromSystem("fileid://file-fe-1YlcPJwCVJyRdmUkKGpqV3jo"));
		req.messages.Add(ChatMessage.FromUser("这篇文章讲了什么？"));
		var rsp = client.ChatCompletion.CreateCompletion(req,"qwen-turbo").Result;
		return;
	}

	public static void TestAliFile3()
	{
		var client = CreateAliAiClient();
		var rsp = client.Files.ListFile().Result;
		return;
	}

	public static void Main(string[] args)
	{
		TestAliFile2();
	}
}