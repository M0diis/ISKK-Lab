using Microsoft.EntityFrameworkCore;

namespace db;

public partial class MyDatabase : DbContext
{
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if( !optionsBuilder.IsConfigured )
		{
			var dbConnStr = "Server=localhost;User=root;Password=;Database=karkasai";
			
			optionsBuilder.UseMySql(dbConnStr, ServerVersion.AutoDetect(dbConnStr));
		}
	}
}
