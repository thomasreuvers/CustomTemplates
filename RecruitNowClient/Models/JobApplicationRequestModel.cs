using Refit;

namespace RecruitNowClient.Models;

public class JobApplicationRequestModel
{
    public required RecruitNowJobApplicationInputModel Application { get; set; }
    public StreamPart? CvFile { get; set; }
}