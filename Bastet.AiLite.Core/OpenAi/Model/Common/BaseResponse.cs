using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace Bastet.AiLite.OpenAi.Model.Common;

public class ObjectBaseResponse
{
	[JsonProperty(PropertyName = "object")]
	public string ObjectTypeName { get; set; }
}

public class BaseResponse : ObjectBaseResponse
{
	public bool Successful => error == null;

	public Error error { get; set; }

	public string stream_event { get; set; }
	
	[JsonIgnore]
	public HttpStatusCode HttpStatusCode { get; set; }
	
	[JsonIgnore]
	public ResponseHeaderValues HeaderValues { get; set; }
}

public class ResponseHeaderValues
{
	public DateTimeOffset? Date { get; set; }
	public string Connection { get; set; }
	public string AccessControlAllowOrigin { get; set; }
	public string CacheControl { get; set; }
	public string Vary { get; set; }
	public string XRequestId { get; set; }
	public string StrictTransportSecurity { get; set; }
	public string CFCacheStatus { get; set; }
	public List<string> SetCookie { get; set; }
	public string Server { get; set; }
	public string CF_RAY { get; set; }
	public string AltSvc { get; set; }
	public Dictionary<string, IEnumerable<string>> All { get; set; }
	public RateLimitInfo RateLimits { get; set; }
	public OpenAIInfo OpenAI { get; set; }
}

public class OpenAIInfo
{
	public string Model { get; set; }
	public string Organization { get; set; }
	public string ProcessingMs { get; set; }
	public string Version { get; set; }
}

public class RateLimitInfo
{
	public string LimitRequests { get; set; }
	public string LimitTokens { get; set; }
	public string LimitTokensUsageBased { get; set; }
	public string RemainingRequests { get; set; }
	public string RemainingTokens { get; set; }
	public string RemainingTokensUsageBased { get; set; }
	public string ResetRequests { get; set; }
	public string ResetTokens { get; set; }
	public string ResetTokensUsageBased { get; set; }
}

public class Error
{
	public string code { get; set; }

	public string param { get; set; }

	public string type { get; set; }

	public int? line { get; set; }

	[JsonIgnore]
	public string Message { get; private set; }

	[JsonIgnore]
	public List<string> Messages { get; private set; }

	public object message { get; set; }

	public string event_id { get; set; }
}