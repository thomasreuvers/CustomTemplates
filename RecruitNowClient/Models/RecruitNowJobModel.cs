namespace RecruitNowClient.Models;

public class RecruitNowJobModel
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public DateTimeOffset PublicationDate { get; set; }
    public RecruitNowJobDescriptionModel Descriptions { get; set; } = new();
    public RecruitNowJobFacetsModel Facets { get; set; } = new();
    public RecruitNowJobSalaryModel Salary { get; set; } = new();
    public RecruitNowJobOfficeModel Office { get; set; } = new();
}

public class RecruitNowJobDescriptionModel
{
    public string Summary { get; set; } = string.Empty;
    public string FunctionDescription { get; set; } = string.Empty;
    public string ClientDescription { get; set; } = string.Empty;
    public string RequirementsDescription { get; set; } = string.Empty;
    public string OfferDescription { get; set; } = string.Empty;
    public string AdditionalDescription { get; set; } = string.Empty;
}

public class RecruitNowJobFacetsModel
{
    public RecruitNowJobFacetModel[] Locations { get; set; } = Array.Empty<RecruitNowJobFacetModel>();
    public RecruitNowJobFacetModel[] WorkLocationPreferences { get; set; } = Array.Empty<RecruitNowJobFacetModel>();
    public RecruitNowJobFacetModel[] HoursPerWeek { get; set; } = Array.Empty<RecruitNowJobFacetModel>();
    public RecruitNowJobFacetModel[] JobTypes { get; set; } = Array.Empty<RecruitNowJobFacetModel>();
    public RecruitNowJobFacetModel[] Regions { get; set; } = Array.Empty<RecruitNowJobFacetModel>();
    public RecruitNowJobFacetModel[] Categories { get; set; } = Array.Empty<RecruitNowJobFacetModel>();
    public RecruitNowJobFacetModel[] Sectors { get; set; } = Array.Empty<RecruitNowJobFacetModel>();
}

public class RecruitNowJobFacetModel
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}

public class RecruitNowJobSalaryModel
{
    public decimal SalaryMin { get; set; }
}

public class RecruitNowJobOfficeModel
{
    public string Name { get; set; } = string.Empty;
}