namespace BoostUp.Controllers
{
    using BoostUp.Data;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class JobsController : Controller
    {
        private readonly BoostUpDbContext data;

        public JobsController(BoostUpDbContext data) => this.data = data;

        public IActionResult Add()
        {
            return View();
        }
    }
}
