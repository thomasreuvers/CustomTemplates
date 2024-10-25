# FlexiApiClient Class Library

## Overview

The **FlexiApiClient** class library is designed to facilitate making HTTP API calls in a flexible and extensible manner. It leverages `Polly` for retry policies and `Refit` for generating type-safe API clients. The library supports different authentication mechanisms such as Bearer and Basic authentication. By using a combination of dependency injection and configurable settings, this template provides a clean and maintainable approach to handling HTTP requests across multiple APIs.

## Key Features

- **Flexible API Client Generation**: Automatically generate API clients using Refit.
- **Pluggable Authentication**: Easily extend or swap authentication mechanisms (Bearer, Basic).
- **Polly Retry Policies**: Resilient to transient errors with retry logic.
- **Centralized Exception Handling**: Consistent error handling for API exceptions.
- **Configuration-Driven**: API configurations (e.g., base URL, authentication) are managed via `appsettings.json`.

## Components

### 1. **`BaseHttpClient<TApi>`**
This is the core of the library. It provides shared logic for:
- Executing API requests with retry policies.
- Handling exceptions.
- Initializing the Refit API client.

### 2. **`ExecuteWithPolicyAsync`**
A helper method in `BaseHttpClient` that wraps API requests with Polly’s retry logic. It supports both methods that return a result (`TResult`) and methods that only return `Task`.

### 3. **`HandleExceptionAsync`**
A virtual method that allows child classes to provide specific exception handling logic. By default, it handles common exceptions like `HttpRequestException`.

### 4. **Authentication Handlers**
- **`BearerAuthHandler`**: Handles Bearer token authentication.
- **`BasicAuthHandler`**: Handles Basic authentication with username and password.

### 5. **Configuration Settings**
API settings are stored in `appsettings.json` and injected into the clients via `IOptions<T>`. Each API client will have its own settings class, such as `ExampleApiSettings` and `AnotherApiSettings`, which define properties like `BaseUrl`, `Token`, and `AuthenticationType`.

### 6. **`IServiceCollectionExtensions`**
This class provides an extension method to dynamically register all API clients and their configurations with the DI container.
