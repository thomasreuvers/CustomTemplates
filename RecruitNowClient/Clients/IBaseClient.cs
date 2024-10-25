using RecruitNowClient.Attributes;
using Refit;

namespace RecruitNowClient.Clients;

[RefitClient]
[Headers("Accept: application/json")]
public interface IBaseClient;