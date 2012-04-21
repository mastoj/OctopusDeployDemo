using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Migrator.Framework;

namespace OctopusDeploy.Migrations
{
    [Migration(2)]
    public class CreateSiteInfo_2 : Migration
    {
        public override void Up()
        {
            Database.RenameTable("SiteInfo", "SiteInfo2");
        }

        public override void Down()
        {
            Database.RenameTable("SiteInfo2", "SiteInfo");
        }
    }
}
