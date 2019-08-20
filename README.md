# CPW213-eCommerceSite
In-Class ASP.NET Core MVC Sample Site
<h1> e-Commerce - The Game Radius <h1>

This repository focuses on implementing the ASP.NET MVC design pattern as well as using the 
Entity framework to ensure data driven capability.

<h4> Run website locally from Visual Studio after installing these prerequisites. <h4>

**.NET CORE 2.2**
https://dotnet.microsoft.com/download

**Entity Framework**
https://www.entityframeworktutorial.net/what-is-entityframework.aspx

Package Manager Console
PM> Install-Package EntityFramwork -Version 6.2.0

This e-Commerce prototype implements CRUD functionality and is a great reference tool.
https://stackify.com/what-are-crud-operations/

**INSIDE THE DATA FOLDER**

<h3> The GameContext class is what is needed to setup the context class for THe Game Radius store <h3>

public class GameContext : DbContext 
    {
        public GameContext(DbContextOptions<GameContext> options)
            : base(options)
        {

        }

        // Add a db set of <T> for each entity you want to keep track of in the database
        public DbSet<VideoGame> VideoGames { get; set; }
        public DbSet<Member> Members { get; set; }
    }
}

After getting the other Db classes setup be sure to run 
**Update-Database**
Inside the Package Manager Console





