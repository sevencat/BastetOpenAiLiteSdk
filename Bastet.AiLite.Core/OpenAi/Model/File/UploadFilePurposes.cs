using System;

namespace Bastet.AiLite.OpenAi.Model.File;

public static class UploadFilePurposes
{
	public enum UploadFilePurpose
	{
		FineTune,
		FineTuneResults,
		Assistants,
		AssistantsOutput,
		Vision,
		Batch,
		BatchOutput
	}

	public const string Assistants = "assistants";
	public const string AssistantsOutput = "assistants_output";
	public const string Vision = "vision";
	public const string Batch = "batch";
	public const string BatchOutput = "batch_output";
	public const string FineTune = "fine-tune";
	public const string FineTuneResults = "fine-tune-results";

	public static string EnumToString(this UploadFilePurpose uploadFilePurpose)
	{
		return uploadFilePurpose switch
		{
			UploadFilePurpose.Assistants => Assistants,
			UploadFilePurpose.Vision => Vision,
			UploadFilePurpose.Batch => Batch,
			UploadFilePurpose.FineTune => FineTune,
			UploadFilePurpose.BatchOutput => BatchOutput,
			UploadFilePurpose.AssistantsOutput => AssistantsOutput,
			UploadFilePurpose.FineTuneResults => FineTuneResults,
			_ => throw new ArgumentOutOfRangeException(nameof(uploadFilePurpose), uploadFilePurpose, null)
		};
	}

	public static UploadFilePurpose ToEnum(string filePurpose)
	{
		return filePurpose switch
		{
			Assistants => UploadFilePurpose.Assistants,
			Vision => UploadFilePurpose.Vision,
			Batch => UploadFilePurpose.Batch,
			FineTune => UploadFilePurpose.FineTune,
			BatchOutput => UploadFilePurpose.BatchOutput,
			AssistantsOutput => UploadFilePurpose.AssistantsOutput,
			FineTuneResults => UploadFilePurpose.FineTuneResults,
			_ => throw new ArgumentOutOfRangeException(nameof(filePurpose), filePurpose, null)
		};
	}
}