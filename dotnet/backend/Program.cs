using System.Net;
using System.Reflection;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using Microsoft.OpenApi.Models;
using modkaz.DBs;


namespace modkaz.Backend;


/// <summary>
/// <para>Application entry point.</para>
/// <para>Static members are thread safe, instance members are not.</para>
/// </summary>
public class Program
{
	/// <summary>
	/// Program entry point.
	/// </summary>
	/// <param name="args">Command line arguments.</param>
	public static void Main(string[] args)
	{
		var self = new Program();
		self.Run(args);
	}

	/// <summary>
	/// Configures and runs the internals of the application.
	/// </summary>
	/// <param name="args">Command line arguments.</param>
	private void Run(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		//add and configure swagger documentation generator
		builder.Services.AddSwaggerGen(opts => {
			//include code comments in swagger documentation
			var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
			opts.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

			//enable JWT authentication support in swagger interface
			opts.AddSecurityDefinition(
				"JWT",
				new OpenApiSecurityScheme {
					Description = "JWT Authorization header using the Bearer scheme.",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.Http,
					Scheme = "bearer"
				}
			);

			opts.AddSecurityRequirement(new OpenApiSecurityRequirement {
				{
					new OpenApiSecurityScheme {
						Reference = new OpenApiReference {
							Type = ReferenceType.SecurityScheme,
							Id = "JWT"
						}
					},
					new List<string>()
				}
			});
		});

		//turn on support for web api controllers
		builder.Services.AddControllers();

		//configure JWT based authentication
		builder.Services
			.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(opts => {
				opts.SaveToken = true;
				opts.TokenValidationParameters =
					//XXX: this is unsafe, use more restrictive settings once it works
					new TokenValidationParameters()	{
						ValidateIssuer = false,
						ValidateAudience = false,
						ValidAudience = "",
						ValidIssuer = "",
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config.JwtSecret))
					};
			});

		//add CORS policies
		builder.Services.AddCors(cr => {
			//allow everything from everywhere
			cr.AddPolicy("allowAll", cp => {
				cp.AllowAnyOrigin();
				cp.AllowAnyMethod();
				cp.AllowAnyHeader();
			});
		});

		
		builder.Services.AddDbContext<MyDatabase>();

		//build the app
		var app = builder.Build();

		//turn CORS policy on
		app.UseCors("allowAll");

		//turn on support for swagger web page
		app.UseSwagger();
		app.UseSwaggerUI();

		//configure serving of SPA static content
		if( !app.Environment.IsDevelopment() )
		{
			app.UseSpaStaticFiles();
		}

		//turn on request routing
		app.UseRouting();

		//these two lines turn on support for authentication and authorization middleware
		app.UseAuthentication();
		app.UseAuthorization();

		//configure routes
		app.UseEndpoints(ep => {
			ep.MapControllerRoute(
				name: "default",
				pattern: "{controller}/{action=Index}/{id?}"
			);
		});

		//configure SPA middleware
		app.UseSpa(spa => {
			//this must point to the frontend project relative to backend project
			spa.Options.SourcePath = "../../frontend";

			//in development mode we proxy to frontend development server
			if( app.Environment.IsDevelopment() )
			{
				spa.UseProxyToSpaDevelopmentServer("http://localhost:3000");
			}
		});

		//start the server, block until it shuts down
		app.Run();
	}
}
