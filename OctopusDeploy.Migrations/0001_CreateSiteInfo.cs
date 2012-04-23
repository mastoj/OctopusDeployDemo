using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Migrator.Framework;

namespace OctopusDeploy.Migrations
{
    [Migration(1)]
    public class CreateSiteInfo_1 : Migration
    {
        public override void Up()
        {
            Database.AddTable("SiteInfo",
                    new Column("Message", DbType.AnsiString, 25)
                    );
            Database.Insert("SiteInfo", new string[] {"Message"}, new string[] {"Hello migrations 1.0.0.0"});
        }

        public override void Down()
        {
            Database.RemoveTable("User");
        }
    }

}
