using System;
using Bastet.AiLite.OpenAi.Model.Common;

namespace Bastet.AiLite.OpenAi.Model.File;

public class FileUploadResponse : BaseResponse
{
	public string id { get; set; }

	public int bytes { get; set; }

	public string filename { get; set; }

	public string purpose { get; set; }

	public long created_at { get; set; }

	public DateTimeOffset CreatedAt => DateTimeOffset.FromUnixTimeSeconds(created_at);
}