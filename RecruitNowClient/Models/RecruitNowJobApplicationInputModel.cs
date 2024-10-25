namespace RecruitNowClient.Models;

public class RecruitNowJobApplicationInputModel
{
    public required string VacancyId { get; set; }
    public required string ApplicationFormId { get; set; }
    public required RecruitNowJobApplicationFieldResponseInputModel[] Responses { get; set; }
}

public class RecruitNowJobApplicationFieldResponseInputModel
{
    public required string Type { get; set; }
    public required string Name { get; set; }
    public required object Response { get; set; }
}