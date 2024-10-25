using RecruitNowClient.Constants;
using RecruitNowClient.Models;
using Refit;

namespace RecruitNowClient.Clients;

public interface IJobClient : IBaseClient
{
    [Get(RecruitNowConstants.AllJobsEndpoint)]
    Task<RecruitNowJobModel[]> AllJobsAsync();
    
    [Multipart]
    [Post(RecruitNowConstants.SendJobEndpoint)]
    Task SendJobAsync([Body] JobApplicationRequestModel jobApplicationRequest);
}