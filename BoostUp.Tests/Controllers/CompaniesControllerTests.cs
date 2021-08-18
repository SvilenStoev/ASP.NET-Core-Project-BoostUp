namespace BoostUp.Tests.Controllers
{
    using System.Linq;
    using Xunit;
    using FluentAssertions;
    using MyTested.AspNetCore.Mvc;

    using BoostUp.Controllers;
    using BoostUp.Data.Models;
    using BoostUp.Models.Companies;
    using BoostUp.Models.Addresses;

    using static Data.Companies;
    using static Data.Categories;
    using static Data.Industries;
    using static GlobalConstants;

    public class CompaniesControllerTests
    {
        private const int DefaultCompanyId = 1;
        private const int DefaultJobId = 1;
        private const string TestCompanyOverview = "Some nice company with long overview enought to pass the test :)";

        //Add Action

        [Fact]
        public void AddShouldBeMapped()
           => MyRouting
           .Configuration()
           .ShouldMap($"/Companies/Add")
           .To<CompaniesController>(c => c.Add());

        [Fact]
        public void GetAddShouldBeForAuthorizedUsers()
             => MyController<CompaniesController>
                 .Instance(controller => controller
                    .WithUser(TestUser.Identifier))
                 .Calling(c => c.Add())
                 .ShouldHave()
                 .ActionAttributes(attributes => attributes
                     .RestrictingForAuthorizedRequests());

        [Fact]
        public void GetAddShouldReturnViewWithValidModel()
             => MyController<CompaniesController>
                 .Instance(controller => controller
                    .WithUser(TestUser.Identifier)
                    .WithData(new Recruiter
                    {
                        UserId = TestUser.Identifier
                    })
                    .WithData(FiveCategories())
                    .WithData(FiveIndustries()))
                 .Calling(c => c.Add())
                 .ShouldReturn()
                 .View(view => view
                     .WithModelOfType<CompanyInputModel>()
                     .Passing(model => model.Categories.Should().HaveCount(5)));

        [Fact]
        public void PostAddShouldBeForAuthorizedUsers()
         => MyController<CompaniesController>
             .Instance(controller => controller
                .WithUser(TestUser.Identifier))
             .Calling(c => c.Add(new CompanyInputModel()))
             .ShouldHave()
             .ActionAttributes(attributes => attributes
                  .RestrictingForHttpMethod(HttpMethod.Post)
                  .RestrictingForAuthorizedRequests());

        [Theory]
        [InlineData("BoostUp", 2021, TestCompanyOverview, 1, 1, "Bulgaria", "Sofia")]
        public void PostAddShouldReturnRedirectWithValidModelAndQueryString(
            string name,
            int founded,
            string overview,
            int industryId,
            int categoryId,
            string country,
            string city)
            => MyController<CompaniesController>
                .Instance(controller => controller
                    .WithUser(TestUser.Identifier)
                    .WithData(new Industry
                    {
                        Id = industryId
                    })
                    .WithData(new Category
                    {
                        Id = categoryId
                    }))
                .Calling(c => c.Add(new CompanyInputModel
                {
                    Name = name,
                    Founded = founded,
                    Overview = overview,
                    IndustryId = industryId,
                    CategoryId = categoryId,
                    Address = new AddressInputModel
                    {
                        Country = country,
                        City = city,
                    }
                }))
                .ShouldHave()
                .ValidModelState()
                .Data(data => data
                    .WithSet<Company>(companies => companies
                        .Any(c =>
                            c.Name == name &&
                            c.Overview == overview &&
                            c.Founded == founded)))
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GlobalMessageKey))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<CompaniesController>(c => c.All(With.Any<CompaniesQueryModel>())));

        //All Action

        [Fact]
        public void AllShouldReturnViewWithCorrectModelAndDataForPublicCompanies()
          => MyMvc
            .Pipeline()
            .ShouldMap("/Companies/All")
            .To<CompaniesController>(j => j.All(new CompaniesQueryModel()))
            .Which(controller => controller
                .WithData(FiveCompanies()))
            .ShouldReturn()
            .View(view => view
                .WithModelOfType<CompaniesQueryModel>()
                .Passing(m => m.TotalCompanies.Should().Be(5)));

        //BecomeEmployee Action

        [Fact]
        public void GetBecomeEmployeeShouldBeMapped()
         => MyRouting
         .Configuration()
         .ShouldMap("Companies/BecomeEmployee")
         .To<CompaniesController>(c => c.BecomeEmployee(new CompanyBecomeEmployeeViewModel()));

        [Fact]
        public void GetBecomeEmployeeShouldBeForAuthorizedUsers()
         => MyController<CompaniesController>
             .Instance(controller => controller
                .WithUser(TestUser.Identifier))
             .Calling(c => c.BecomeEmployee(new CompanyBecomeEmployeeViewModel()))
             .ShouldHave()
             .ActionAttributes(attributes => attributes
                 .RestrictingForAuthorizedRequests());

        [Fact]
        public void GetBecomeEmployeeShouldReturnViewWithCorrectModel()
          => MyController<CompaniesController>
              .Instance()
             .Calling(c => c.BecomeEmployee(new CompanyBecomeEmployeeViewModel()))
             .ShouldReturn()
             .View(view => view
                 .WithModelOfType<CompanyBecomeEmployeeViewModel>());

        [Fact]
        public void PostBecomeEmployeeShouldReturnBadRequestWhenUserIsAlreadyEmployeeOfTheCompany()
         => MyController<CompaniesController>
             .Instance(controller => controller
                 .WithUser(TestUser.Identifier)
                 .WithData(new Company
                 {
                     Id = DefaultCompanyId
                 })
                 .WithData(new User
                 {
                     Id = TestUser.Identifier,
                     CompanyId = DefaultCompanyId,
                 }))
            .Calling(c => c.BecomeEmployee(DefaultCompanyId))
            .ShouldReturn()
            .BadRequest();

        [Theory]
        [InlineData("BoostUp", 2021)]
        public void PostBecomeEmployeeShouldBeForAuthorizedUsersAndSetEmployeeAndRedirectToAction(
           string companyName,
           int companyFounded)
           => MyController<CompaniesController>
               .Instance(controller => controller
                   .WithUser(TestUser.Identifier)
                   .WithData(new Company
                   {
                       Id = DefaultCompanyId,
                       Name = companyName,
                       Founded = companyFounded,
                   })
                   .WithData(new User
                   {
                       Id = TestUser.Identifier,
                   }))
               .Calling(c => c.BecomeEmployee(DefaultCompanyId))
               .ShouldHave()
               .ActionAttributes(attributes => attributes
                   .RestrictingForHttpMethod(HttpMethod.Post)
                   .RestrictingForAuthorizedRequests())
               .ValidModelState()
               .TempData(tempData => tempData
                   .ContainingEntryWithKey(GlobalMessageKey))
               .AndAlso()
               .ShouldReturn()
               .Redirect(redirect => redirect
                   .To<CompaniesController>(c => c.Details(DefaultCompanyId, $"{companyName}-{companyFounded}")));

        //Edit Action

        [Fact]
        public void EditShouldBeMapped()
         => MyRouting
         .Configuration()
         .ShouldMap($"/Companies/Edit?id={DefaultJobId}")
         .To<CompaniesController>(c => c.Edit(DefaultJobId));

        [Fact]
        public void GetEditShouldBeForAuthorizedUsers()
             => MyController<CompaniesController>
                 .Instance(controller => controller
                    .WithUser(TestUser.Identifier))
                 .Calling(c => c.Edit(DefaultJobId))
                 .ShouldHave()
                 .ActionAttributes(attributes => attributes
                     .RestrictingForAuthorizedRequests());

        [Fact]
        public void GetEditShouldReturnUnautorizedWhenUserIsNotAdmin()
            => MyController<CompaniesController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.Edit(DefaultCompanyId))
                .ShouldReturn()
                .Unauthorized();

        [Fact]
        public void PostEditShouldBeForAuthorizedUsers()
         => MyController<CompaniesController>
             .Instance(controller => controller
                .WithUser(TestUser.Identifier))
             .Calling(c => c.Edit(DefaultJobId, new CompanyInputModel()))
             .ShouldHave()
             .ActionAttributes(attributes => attributes
                  .RestrictingForHttpMethod(HttpMethod.Post)
                  .RestrictingForAuthorizedRequests());
    }
}
