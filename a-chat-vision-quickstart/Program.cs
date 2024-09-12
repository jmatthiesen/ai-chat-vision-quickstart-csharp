using Azure.Core;
using Microsoft.SemanticKernel;
using OllamaSharp.Models;
using System;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddKernel();
// OpenAI
builder.Services.AddOpenAIChatCompletion("gpt-4o-mini", Environment.GetEnvironmentVariable("OPENAI_KEY"));

// Azure AI Model Catalog (Azure AI Inference SDK)
#pragma warning disable SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
//builder.Services.AddAzureAIInferenceChatCompletion("model-deployment",
//            Environment.GetEnvironmentVariable("AZURE_INFERENCE_KEY"),
//            new Uri("modelEndpoint"));

// Ollama:
//builder.Services.AddOllamaChatCompletion("llama3.1", new Uri("http://localhost:11434"));

// GitHub Models:
//builder.Services.AddAzureAIInferenceChatCompletion(modelId: "meta-llama-3.1-70b-instruct", Environment.GetEnvironmentVariable("GITHUB_TOKEN"), new Uri("https://models.inference.ai.azure.com"));

// Azure OpenAI
// builder.Services.AddAzureOpenAIChatCompletion(model, endpoint, new DefaultAzureCredential());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

var chatHandler = new ChatHandler();

app.MapPost("/chat", chatHandler.Chat);
app.MapPost("/chat/stream", chatHandler.Stream);

app.Run();


app.Run();
