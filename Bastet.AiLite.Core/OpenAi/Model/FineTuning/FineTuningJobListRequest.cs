namespace Bastet.AiLite.OpenAi.Model.FineTuning;

public class FineTuningJobListRequest
{
	public string after { get; set; }
	
	public int? limit { get; set; }
}