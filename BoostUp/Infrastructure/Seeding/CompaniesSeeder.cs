namespace BoostUp.Infrastructure.Seeding
{
    using BoostUp.Data;
    using BoostUp.Data.Models;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CompaniesSeeder : ISeeder
    {
        public void Seed(IServiceProvider services)
        {
            var data = services.GetRequiredService<BoostUpDbContext>();

            if (data.Companies.Any())
            {
                return;
            }

            data.Companies.AddRange(new[]
            {
                   new Company
                   {
                        Name = "Cargill",
                        LogoUrl = "https://media-exp1.licdn.com/dms/image/C560BAQEerHzsP6gtVw/company-logo_200_200/0/1568991363595?e=1637193600&v=beta&t=p227F6lrI2fG2v18mBpbdZQ6GOY43UrSfYmzGcmky28",
                        WebsiteUrl = "https://www.cargill.com",
                        CategoryId = 6,
                        IndustryId = 4,
                        Founded = 1965,
                        Overview = "Our team of 160,000 professionals in 70 countries draws together the worlds of food, agriculture, nutrition and risk management. " +
                        "For more than 150 years, we have helped farmers grow more, connecting them to broader markets. " +
                        "We are continuously developing products that give consumers just what they’re seeking, advancing nutrition, food safety and sustainability. " +
                        "And we help all of our partners innovate and manage risk, so they can nourish the world again tomorrow." +
                        "We combine 153 years of experience with new technologies and insights to serve as a trusted partner for food, agriculture, financial and industrial customers in more than 125 countries. " +
                        "Side-by-side, we are building a stronger, sustainable future for agriculture.",
                        IsPublic = true,
                        Address = new Address
                        {
                             Country = "USA",
                             City= "Wayzata",
                             AddressText = "15407 McGinty Rd W, Minnesota 55391"
                        }
                   },
                   new Company
                   {
                        Name = "SoftUni",
                        LogoUrl = "https://media-exp1.licdn.com/dms/image/C560BAQGg8k1-M3B8sw/company-logo_200_200/0/1603699447336?e=1637193600&v=beta&t=SpFeO-KjffSl_MoPz212buAcbLDhAZBnUMck3Nyno00",
                        WebsiteUrl = "https://softuni.org/",
                        CategoryId = 3,
                        IndustryId = 11,
                        Founded = 2013,
                        Overview = "SoftUni is an organization that provides high quality software development, digital and design education, profession and a career start for thousands of young people in Bulgaria. " +
                        "SoftUni teaches its students how to be skillful software engineers, using the latest technologies, tools and platforms. " +
                        "Big part of learning materials (videos, slides, demos, homework assignments) are free and available for everyone through SoftUni's learning systems.",
                        IsPublic = false,
                        Address = new Address
                        {
                             Country = "Bulgaria",
                             City= "Sofia",
                             AddressText = "Mladost 4, Aleksandar Malinov Boulevard 78"
                        }
                   },
            });

            data.SaveChanges();
        }
    }
}
