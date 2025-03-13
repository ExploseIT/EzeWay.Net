
using Microsoft.EntityFrameworkCore;
using EzeWay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using EzeWay.ef;
using System.Text;

namespace EzeWay.ef
{
    public class dbContext : DbContext
    {

        public dbContext()
        {
        }

        public dbContext(DbContextOptions<dbContext> options)
: base(options)
        { }

        public DbSet<ent_menu> lMenu { get; set; }
        public DbSet<ent_post> lPost { get; set; }
        public DbSet<ent_page> lPage { get; set; }
        public DbSet<cSetting> lSetting { get; set; }
        public DbSet<ent_user> lUser { get; set; }
        public DbSet<ent_title> lTitle { get; set; }
        public DbSet<cCust> lCustomer { get; set; }
        public DbSet<cPageData> lPageData { get; set; }
        public DbSet<cPageContent> lPageContent { get; set; }
        public DbSet<cProductData> lProductData { get; set; }
        public DbSet<cProductSource> lProductSource { get; set; }
        public DbSet<cProductCat> lProductCat { get; set; }
        public DbSet<cContact> lContact { get; set; }
        public DbSet<cBlog> lBlog { get; set; }
        public DbSet<cBlogStatus> lBlogStatus { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }
    }

    }
