﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Bastet.AiLite.OpenAi.Managers;

public partial class OpenAIService : IOpenAIService
{
	private readonly HttpClient _httpClient;
	private readonly OpenAIOptions _options;
	private readonly IOpenAIEndpointProvider _endpointProvider;

	public OpenAIService(OpenAIOptions options)
	{
		_endpointProvider = new OpenAIEndpointProvider(options.ApiVersion);
		_options = options;
		_httpClient = new HttpClient();
		_httpClient.BaseAddress = new Uri(options.BaseDomain);
		_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _options.ApiKey);
	}

	public IFileService Files => this;

	public IChatCompletionService ChatCompletion => this;
}