using Newtonsoft.Json;

namespace Bastet.AiLite.OpenAi.Model.Common;

public class ToolDefinition
{
	public string type { get; set; }

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public FileSearchTool file_search { get; set; }
	
	[JsonIgnore]
	public FunctionDefinition Function { get; set; }

	[JsonIgnore]
	public object FunctionsAsObject { get; set; }
}

public class FileSearchTool
{
	public int? max_num_results { get; set; }

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public RankingOptions ranking_options { get; set; }
}

public class RankingOptions
{
	/// <summary>
	///     The ranker to use for the file search. If not specified will use the auto ranker.
	/// </summary>
	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public string ranker { get; set; }

	/// <summary>
	///     The score threshold for the file search. All values must be a floating point number between 0 and 1.
	/// </summary>
	public float score_threshold { get; set; }
}