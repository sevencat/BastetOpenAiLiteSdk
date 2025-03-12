using Bastet.AiLite.OpenAi.Model.Common;
using Bastet.AiLite.OpenAi.Model.FineTuning;
using Bastet.AiLite.OpenAi.Model.VectorStore;

namespace Bastet.AiLite.OpenAi.Managers;

public class OpenAIEndpointProvider : IOpenAIEndpointProvider
{
	public string ModelRetrieve(string model)
	{
		throw new System.NotImplementedException();
	}

	public string CompletionCreate()
	{
		throw new System.NotImplementedException();
	}

	public string EditCreate()
	{
		throw new System.NotImplementedException();
	}

	public string ModelsList()
	{
		throw new System.NotImplementedException();
	}

	public string ModelsDelete(string modelId)
	{
		throw new System.NotImplementedException();
	}

	public string FilesList()
	{
		throw new System.NotImplementedException();
	}

	public string FilesUpload()
	{
		throw new System.NotImplementedException();
	}

	public string FileDelete(string fileId)
	{
		throw new System.NotImplementedException();
	}

	public string FileRetrieve(string fileId)
	{
		throw new System.NotImplementedException();
	}

	public string FileRetrieveContent(string fileId)
	{
		throw new System.NotImplementedException();
	}

	public string FineTuneCreate()
	{
		throw new System.NotImplementedException();
	}

	public string FineTuneList()
	{
		throw new System.NotImplementedException();
	}

	public string FineTuneRetrieve(string fineTuneId)
	{
		throw new System.NotImplementedException();
	}

	public string FineTuneCancel(string fineTuneId)
	{
		throw new System.NotImplementedException();
	}

	public string FineTuneListEvents(string fineTuneId)
	{
		throw new System.NotImplementedException();
	}

	public string FineTuneDelete(string fineTuneId)
	{
		throw new System.NotImplementedException();
	}

	public string FineTuningJobCreate()
	{
		throw new System.NotImplementedException();
	}

	public string FineTuningJobList(FineTuningJobListRequest fineTuningJobListRequest)
	{
		throw new System.NotImplementedException();
	}

	public string FineTuningJobRetrieve(string fineTuningJobId)
	{
		throw new System.NotImplementedException();
	}

	public string FineTuningJobCancel(string fineTuningJobId)
	{
		throw new System.NotImplementedException();
	}

	public string FineTuningJobListEvents(string fineTuningJobId)
	{
		throw new System.NotImplementedException();
	}

	public string EmbeddingCreate()
	{
		throw new System.NotImplementedException();
	}

	public string ModerationCreate()
	{
		throw new System.NotImplementedException();
	}

	public string ImageCreate()
	{
		throw new System.NotImplementedException();
	}

	public string ImageEditCreate()
	{
		throw new System.NotImplementedException();
	}

	public string ImageVariationCreate()
	{
		throw new System.NotImplementedException();
	}

	public string ChatCompletionCreate()
	{
		throw new System.NotImplementedException();
	}

	public string AudioCreateTranscription()
	{
		throw new System.NotImplementedException();
	}

	public string AudioCreateTranslation()
	{
		throw new System.NotImplementedException();
	}

	public string AudioCreateSpeech()
	{
		throw new System.NotImplementedException();
	}

	public string BatchCreate()
	{
		throw new System.NotImplementedException();
	}

	public string BatchRetrieve(string batchId)
	{
		throw new System.NotImplementedException();
	}

	public string BatchCancel(string batchId)
	{
		throw new System.NotImplementedException();
	}

	public string AssistantCreate()
	{
		throw new System.NotImplementedException();
	}

	public string AssistantRetrieve(string assistantId)
	{
		throw new System.NotImplementedException();
	}

	public string AssistantModify(string assistantId)
	{
		throw new System.NotImplementedException();
	}

	public string AssistantDelete(string assistantId)
	{
		throw new System.NotImplementedException();
	}

	public string AssistantList(PaginationRequest assistantListRequest)
	{
		throw new System.NotImplementedException();
	}

	public string AssistantFileCreate(string assistantId)
	{
		throw new System.NotImplementedException();
	}

	public string AssistantFileRetrieve(string assistantId, string fileId)
	{
		throw new System.NotImplementedException();
	}

	public string AssistantFileDelete(string assistantId, string fileId)
	{
		throw new System.NotImplementedException();
	}

	public string AssistantFileList(string assistantId, PaginationRequest assistantFileListRequest)
	{
		throw new System.NotImplementedException();
	}

	public string ThreadCreate()
	{
		throw new System.NotImplementedException();
	}

	public string ThreadRetrieve(string threadId)
	{
		throw new System.NotImplementedException();
	}

	public string ThreadModify(string threadId)
	{
		throw new System.NotImplementedException();
	}

	public string ThreadDelete(string threadId)
	{
		throw new System.NotImplementedException();
	}

	public string MessageCreate(string threadId)
	{
		throw new System.NotImplementedException();
	}

	public string MessageRetrieve(string threadId, string messageId)
	{
		throw new System.NotImplementedException();
	}

	public string MessageModify(string threadId, string messageId)
	{
		throw new System.NotImplementedException();
	}

	public string MessageList(string threadId, MessageListRequest messageListRequest)
	{
		throw new System.NotImplementedException();
	}

	public string RunCreate(string threadId)
	{
		throw new System.NotImplementedException();
	}

	public string RunRetrieve(string threadId, string runId)
	{
		throw new System.NotImplementedException();
	}

	public string RunModify(string threadId, string runId)
	{
		throw new System.NotImplementedException();
	}

	public string RunList(string threadId, PaginationRequest runListRequest)
	{
		throw new System.NotImplementedException();
	}

	public string RunSubmitToolOutputs(string threadId, string runId)
	{
		throw new System.NotImplementedException();
	}

	public string RunCancel(string threadId, string runId)
	{
		throw new System.NotImplementedException();
	}

	public string ThreadAndRunCreate()
	{
		throw new System.NotImplementedException();
	}

	public string RunStepRetrieve(string threadId, string runId, string stepId)
	{
		throw new System.NotImplementedException();
	}

	public string RunStepList(string threadId, string runId, PaginationRequest runStepListRequest)
	{
		throw new System.NotImplementedException();
	}

	public string VectorStoreCreate()
	{
		throw new System.NotImplementedException();
	}

	public string VectorStoreList(PaginationRequest baseListRequest)
	{
		throw new System.NotImplementedException();
	}

	public string VectorStoreRetrieve(string vectorStoreId)
	{
		throw new System.NotImplementedException();
	}

	public string VectorStoreModify(string vectorStoreId)
	{
		throw new System.NotImplementedException();
	}

	public string VectorStoreDelete(string vectorStoreId)
	{
		throw new System.NotImplementedException();
	}

	public string VectorStoreFileCreate(string vectorStoreId)
	{
		throw new System.NotImplementedException();
	}

	public string VectorStoreFileRetrieve(string vectorStoreId, string fileId)
	{
		throw new System.NotImplementedException();
	}

	public string VectorStoreFileDelete(string vectorStoreId, string fileId)
	{
		throw new System.NotImplementedException();
	}

	public string VectorStoreFileList(string vectorStoreId, VectorStoreFileListRequest baseListRequest)
	{
		throw new System.NotImplementedException();
	}

	public string VectorStoreFileBatchCreate(string vectorStoreId)
	{
		throw new System.NotImplementedException();
	}

	public string VectorStoreFileBatchRetrieve(string vectorStoreId, string batchId)
	{
		throw new System.NotImplementedException();
	}

	public string VectorStoreFileBatchCancel(string vectorStoreId, string batchId)
	{
		throw new System.NotImplementedException();
	}

	public string VectorStoreFileBatchList(string vectorStoreId, string batchId, PaginationRequest baseListRequest)
	{
		throw new System.NotImplementedException();
	}

	public string MessageDelete(string threadId, string messageId)
	{
		throw new System.NotImplementedException();
	}
}