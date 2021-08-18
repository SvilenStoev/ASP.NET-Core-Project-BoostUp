namespace BoostUp.Tests.Mocks
{
    using BoostUp.Services.Jobs;
    using BoostUp.Services.Jobs.Models;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class JobServiceMock
    {
        public static IJobService Instance
        {
            get
            {
                var jobServiceMock = new Mock<IJobService>();

                jobServiceMock
                    .Setup(s => s.Details(1))
                    .Returns(new JobDetailsServiceModel
                    {
                        Id = 1,
                        JobTitle = "Accountant",
                        RecruiterId = "Test",
                        UserId = "Test",
                    });

                return jobServiceMock.Object;
            }
        }
    }
}
