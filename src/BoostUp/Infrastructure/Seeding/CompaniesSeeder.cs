namespace BoostUp.Infrastructure.Seeding
{
    using System;
    using System.Linq;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using BoostUp.Data;
    using BoostUp.Data.Models;

    public class CompaniesSeeder : ISeeder
    {
        public void Seed(IServiceProvider services, IConfiguration configuration)
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
                   new Company
                   {
                        Name = "PROXIAD",
                        LogoUrl = "https://media-exp1.licdn.com/dms/image/C4D0BAQFQLCIRu0o2WA/company-logo_200_200/0/1622795560039?e=1637193600&v=beta&t=KKFuwnYKTd2EF8lMfFO-UZtFjyGtoMb3zUPie1_1uBk",
                        WebsiteUrl = "http://www.proxiad.com/",
                        CategoryId = 4,
                        IndustryId = 9,
                        Founded = 1997,
                        Overview = "Créée en 1997, Proxiad est une société de Conseil et d'Expertise dans les nouvelles technologies et les systèmes d'information. " +
                        "Expert en innovation digitale, nous développons un savoir-faire unique en accompagnant nos 750 collaborateurs dans leur épanouissement professionnel pour qu’ils soient en constante progression et toujours à la pointe de la technologie, dans un contexte humainement agréable et motivant.",
                        IsPublic = true,
                        Address = new Address
                        {
                             Country = "France",
                             City= "Paris",
                             AddressText = "47 rue de Ponthieu, 75008, FR"
                        }
                   },
                   new Company
                   {
                        Name = "Chronika",
                        LogoUrl = "https://media-exp1.licdn.com/dms/image/C4D0BAQGZV1kheXMfog/company-logo_200_200/0/1568009319032?e=1637193600&v=beta&t=KXMBHPp77a__ZE_tp2pmf9_liPYFHffsfaGtoztb8tI",
                        WebsiteUrl = "https://www.chronika.com",
                        CategoryId = 2,
                        IndustryId = 13,
                        Founded = 1994,
                        Overview = "We, in Chronika believe that the clients are the heart and the soul of our business. " +
                        "In every aspect of our work we aim to add value to our clients'​ activities and we base our culture on this aim: to provide individual and tailored approach to each client, to assign the right people for the specific client's needs and to deliver professional services on realistic and reasonable price. " +
                        "Chronika is a Bulgaria-based company of business advisors with more than 15 years of proven expertise and experience, serving clients from 25 countries on 3 continents",
                        IsPublic = true,
                        Address = new Address
                        {
                             Country = "Bulgaria",
                             City= "Sofia",
                             AddressText = "2 Grigor Parlichev Str., office 2 and 3, 1606, BG"
                        }
                   },
                   new Company
                   {
                        Name = "Vega Medical",
                        LogoUrl = "https://media-exp1.licdn.com/dms/image/C4E0BAQFyaVS5B0sDcw/company-logo_200_200/0/1519898858769?e=1637193600&v=beta&t=p3d8Ts48dQbuiM0rvHyhCKlzK5liyPRQBZepqFeDnM8",
                        WebsiteUrl = "http://vegamedical.eu/",
                        CategoryId = 3,
                        IndustryId = 7,
                        Founded = 2010,
                        Overview = "Vega Medical is a healthcare improvement company operating from its headquarters in Sofia, Bulgaria and regional subsidiaries. " +
                        "We help hospitals, clinics and physicians deliver better patient care while at the same time improve efficiency and optimize costs. " +
                        "Vega is uniquely positioned to provide products, services and continuous education to help our partners thrive in an ever-changing healthcare system. " +
                        "Our main goal is to make the latest generation medical equipment and devices available to healthcare institutions in the region. " +
                        "In order to achieve that, we have created tailored solutions that not only meet our clients’ needs but open new horizons in front of them. " +
                        "We see our mission in supporting, consulting and inspiring our partners in order to implement new procedures, introduce new technologies to achieve higher standards and better patient care.",
                        IsPublic = true,
                        Address = new Address
                        {
                             Country = "Bulgaria",
                             City= "Sofia",
                             AddressText = "Boul. Bulgaria 109, Vertigo Tower, 1404, BG"
                        }
                   },
                   new Company
                   {
                        Name = "KPMG",
                        LogoUrl = "https://media-exp1.licdn.com/dms/image/C4D0BAQH5BAu-DqiG5w/company-logo_200_200/0/1622573019375?e=1637193600&v=beta&t=SyOwZN3Fvn0Gl9teyweOyad4lFHnr1Sj3hO7nrHacEk",
                        WebsiteUrl = "http://www.kpmg.com",
                        CategoryId = 4,
                        IndustryId = 13,
                        Founded = 1985,
                        Overview = "KPMG is a global network of professional firms providing Audit, Tax and Advisory services. We have 207,000 outstanding professionals working together to deliver value in 153 countries and territories. " +
                        "With a worldwide presence, KPMG continues to build on our successes thanks to clear vision, defined values and, above all, our people.",
                        IsPublic = true,
                        Address = new Address
                        {
                             Country = "USA",
                             City= "Toronto",
                             AddressText = "333 Bay St, Suite 4600, ON M5H2S5, CA"
                        }
                   },
                   new Company
                   {
                        Name = "Telerik Academy",
                        LogoUrl = "https://media-exp1.licdn.com/dms/image/C4D0BAQGw7iNp3SAPMA/company-logo_200_200/0/1625643287364?e=1637193600&v=beta&t=NjLJHhdazh-RX-C0V6qrTHiPP_Kz_zAmweliKs9QVvI",
                        WebsiteUrl = "https://telerikacademy.com",
                        CategoryId = 2,
                        IndustryId = 11,
                        Founded = 2009,
                        Overview = "Telerik Academy provides real-world practical training for children, high-school and university students and professionals in various fields motivated to enhance their digital skills and become skillful software engineers. " +
                        "Founded by Svetozar Georgiev, Boyko Iaramov, Vassil Terziev and Hristo Kosev in 2009, Telerik Academy is one of the most successful education projects in Bulgaria with more than 13,500 onsite students and 51,000 online users from the beginning of the initiative to date. " +
                        "To learn more, visit www.telerikacademy.com.",
                        IsPublic = true,
                        Address = new Address
                        {
                             Country = "Bulgaria",
                             City= "Sofia",
                             AddressText = "30 Krastyo Rakovski​ Str., Mladost 1A 1729, BG"
                        }
                   },
                   new Company
                   {
                        Name = "SAP",
                        LogoUrl = "https://media-exp1.licdn.com/dms/image/C4D0BAQFC1AyyoOWnXg/company-logo_200_200/0/1625579523932?e=1637193600&v=beta&t=qJ4PZH4nMYG4yNH49bh5VZV763a61LaFqGJy6DvNriI",
                        WebsiteUrl = "http://www.sap.com",
                        CategoryId = 6,
                        IndustryId = 9,
                        Founded = 1972,
                        Overview = "At SAP, our purpose is to help the world run better and improve people’s lives. Our promise is to innovate to help our customers run at their best. " +
                        "SAP is committed to helping every customer become a best-run business. " +
                        "We engineer solutions to fuel innovation, foster equality, and spread opportunity across borders and cultures. " +
                        "Together, with our customers and partners, we can transform industries, grow economies, lift up societies, and sustain our environment. ",
                        IsPublic = true,
                        Address = new Address
                        {
                             Country = "USA",
                             City= "Palo Alto",
                             AddressText = "SAP Labs, Inc., 3410 Hillview Ave., Palo Alto, CA 94304, US"
                        }
                   },
                   new Company
                   {
                        Name = "Tesla",
                        LogoUrl = "https://media-exp1.licdn.com/dms/image/C4D0BAQHUcu98SZ2TVw/company-logo_200_200/0/1607665771371?e=1637193600&v=beta&t=s21Z-2_MS0sHu3dFhVLmCfZwKZlu_qMzziGqT0gdsGA",
                        WebsiteUrl = "http://www.tesla.com",
                        CategoryId = 6,
                        IndustryId = 4,
                        Founded = 2003,
                        Overview = "Tesla’s mission is to accelerate the world’s transition to sustainable energy through increasingly affordable electric vehicles in addition to renewable energy generation and storage. " +
                        "California-based Tesla is committed to having the best-in-class in safety, performance, and reliability in all Tesla cars. There are currently over 275,000 Model S, Model X and Model 3 vehicles on the road worldwide. " +
                        "To achieve a sustainable energy future, Tesla also created infinitely scalable energy products: Powerwall, Powerpack and Solar Roof. " +
                        "As the world’s only vertically integrated energy company, Tesla continues to innovate, scale and reduce the costs of commercial and grid-scale systems, with the goal of ultimately getting us to 100% renewable energy grids.",
                        IsPublic = true,
                        Address = new Address
                        {
                             Country = "USA",
                             City= "Palo Alto",
                             AddressText = "3500 Deer Creek Rd, CA 94304, US"
                        }
                   },
            });

            data.SaveChanges();
        }
    }
}
