using System;
using Bastet.AiLite.OpenAi.Model.Common;

namespace Bastet.AiLite.OpenAi.Model.File;

public class FileResponse : BaseResponse
{
	public int? bytes { get; set; }

	public string filename { get; set; }

	public UploadFilePurposes.UploadFilePurpose PurposeEnum => UploadFilePurposes.ToEnum(purpose);

	public string purpose { get; set; }

	public string status { get; set; }

	public long created_at { get; set; }

	public DateTimeOffset CreatedAt => DateTimeOffset.FromUnixTimeSeconds(created_at);

	public string id { get; set; }
}