﻿using System.Collections.Generic;
using Bastet.AiLite.OpenAi.Model.Common;
using Newtonsoft.Json;

namespace Bastet.AiLite.OpenAi.Model.Chat;

public class ChatMessage
{
	public ChatMessage()
	{
	}

	public ChatMessage(string role, string content, string name = null, IList<ToolCall> toolCalls = null,
		string toolCallId = null)
	{
		this.role = role;
		this.Content = content;
		this.name = name;
		this.tool_calls = toolCalls;
		this.tool_call_id = toolCallId;
	}

	public ChatMessage(string role, IList<MessageContent> contents, string name = null,
		IList<ToolCall> toolCalls = null, string toolCallId = null)
	{
		this.role = role;
		this.Contents = contents;
		this.name = name;
		this.tool_calls = toolCalls;
		this.tool_call_id = toolCallId;
	}

	public string role { get; set; }

	[JsonIgnore]
	public string Content { get; set; }

	[JsonIgnore]
	public IList<MessageContent> Contents { get; set; }

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public string name { get; set; }
	
	public string reasoning_content { get; set; }

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public string tool_call_id { get; set; }

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public FunctionCall function_call { get; set; }

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public IList<ToolCall> tool_calls { get; set; }

	[JsonProperty]
	public object content
	{
		get => Content != null ? Content : Contents;
		set => Content = value?.ToString();
	}

	public static ChatMessage FromAssistant(string content, string name = null, IList<ToolCall> toolCalls = null)
	{
		return new ChatMessage(StaticValues.ChatMessageRoles.Assistant, content, name, toolCalls);
	}

	public static ChatMessage FromTool(string content, string toolCallId)
	{
		return new ChatMessage(StaticValues.ChatMessageRoles.Tool, content, toolCallId: toolCallId);
	}

	public static ChatMessage FromUser(string content, string name = null)
	{
		return new ChatMessage(StaticValues.ChatMessageRoles.User, content, name);
	}

	public static ChatMessage FromSystem(string content, string name = null)
	{
		return new ChatMessage(StaticValues.ChatMessageRoles.System, content, name);
	}

	public static ChatMessage FromUser(IList<MessageContent> contents)
	{
		return new ChatMessage(StaticValues.ChatMessageRoles.User, contents);
	}
}