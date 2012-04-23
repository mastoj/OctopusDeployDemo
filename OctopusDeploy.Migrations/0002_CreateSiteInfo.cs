﻿using System;
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
            Database.Update("SiteInfo", new[] {"Message"}, new[] {"Hello migrations 1.0.0.1"});
        }

        public override void Down()
        {
            Database.Update("SiteInfo", new[] { "Message" }, new[] { "Hello migrations 1.0.0.0" });
        }
    }
}
