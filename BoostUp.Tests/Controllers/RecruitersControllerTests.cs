namespace BoostUp.Tests.Controllers
{
    using System.Linq;
    using Xunit;
    using MyTested.AspNetCore.Mvc;

    using BoostUp.Controllers;
    using BoostUp.Data.Models;
    using BoostUp.Models.Recruiters;
    using BoostUp.Models.Companies;

    using static WebConstants;

    public class RecruitersControllerTests
    {
        private const int DefaultCompanyId = 1;

        [Fact]
        public void GetBecomeShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap($"/Recruiters/Become?companyId={DefaultCompanyId}")
            .To<RecruitersController>(c => c.Become(DefaultCompanyId));

        [Fact]
        public void GetBecomeShouldBeForAuthorizedUsersAndReturnViewWithValidModel()
            => MyController<RecruitersController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.Become(DefaultCompanyId))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<RecruiterInputModel>()
                    .Passing(model => model.CompanyId == DefaultCompanyId));

        [Theory]
        [InlineData("recruiter@abv.bg", "+359888888888", "BoostUp", 2021)]
        public void PostBecomeShouldBeForAuthorizedUsersAndReturnRedirectWithValidModelAndQueryString(
            string email,
            string phonenumber,
            string companyName,
            int companyFounded)
            => MyController<RecruitersController>
                .Instance(controller => controller
                    .WithUser(TestUser.Identifier)
                    .WithData(new Company
                    {
                        Id = DefaultCompanyId,
                        Name = companyName,
                        Founded = companyFounded,
                    }))
                .Calling(c => c.Become(new RecruiterInputModel
                {
                    Email = email,
                    PhoneNumber = phonenumber,
                    CompanyId = DefaultCompanyId
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data => data
                    .WithSet<Recruiter>(recruiters => recruiters
                        .Any(r =>
                            r.Email == email &&
                            r.PhoneNumber == phonenumber &&
                            r.UserId == TestUser.Identifier)))
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GlobalMessageKey))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<CompaniesController>(c => c.Details(DefaultCompanyId, $"{companyName}-{companyFounded}")));

        [Theory]
        [InlineData("recruiter@abv.bg", "+359888888888")]
        public void PostBecomeShouldReturnRedirectToCompaniesAllWhenCompanyIdIsNotValid(
          string email,
          string phonenumber)
          => MyController<RecruitersController>
              .Instance(controller => controller
                  .WithUser())
              .Calling(c => c.Become(new RecruiterInputModel
              {
                  Email = email,
                  PhoneNumber = phonenumber,
                  CompanyId = 0
              }))
              .ShouldHave()
              .ValidModelState()
              .Data(data => data
                  .WithSet<Recruiter>(recruiters => recruiters
                      .Any(r =>
                          r.Email == email &&
                          r.PhoneNumber == phonenumber &&
                          r.UserId == TestUser.Identifier)))
              .TempData(tempData => tempData
                  .ContainingEntryWithKey(GlobalMessageKey))
              .AndAlso()
              .ShouldReturn()
              .Redirect(redirect => redirect
                  .To<CompaniesController>(c => c.All(With.Any<CompaniesQueryModel>())));
    }
}
