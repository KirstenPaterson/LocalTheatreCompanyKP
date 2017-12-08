namespace LocalTheatreCompany.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.SqlClient;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LocalTheatreCompany.Models.ApplicationDbContext>
    {
        public Configuration()
        {

            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(LocalTheatreCompany.Models.ApplicationDbContext context)
        {

        }
    }
}


