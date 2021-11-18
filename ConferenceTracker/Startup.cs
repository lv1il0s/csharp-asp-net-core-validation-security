using ConferenceTracker.Data;
using ConferenceTracker.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConferenceTracker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public string SecretMessage { get; set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("ConferenceTracker"));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddTransient<IPresentationRepository, PresentationRepository>();
            services.AddTransient<ISpeakerRepository, SpeakerRepository>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {            
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            using (var context = scope.ServiceProvider.GetService<ApplicationDbContext>())
                context.Database.EnsureCreated();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}


/*
 * Add Required Attributes to Speaker

In our Speaker class at ConferenceTracker/Entities/, add Required attributes to the following properties:. Note: Required is part of System.ComponentModel.DataAnnotations which we already have a using directive for. Usually you'll need to add this yourself.

Id
FirstName
LastName
Description
2
Add DataType Attributes to Speaker

Add DataType attributes to all appropriate properties in our Speaker class.

Add the DataType attribute with an argument of DataType.Text to the following properties:
FirstName
LastName
Add the DataType attribute with an argument of DataType.MultilineText to the Description property.
Add the DataType attribute with an argument of DataType.EmailAddress to the EmailAddress property.
Add the DataType attribute with an argument of DataType.PhoneNumber to the PhoneNumber property.
3
Add StringLength Attributes to Speaker

Add StringLength attributes to all appropriate properties in our Speaker class.

Add the StringLength attribute with a MaximumLength of 100, and a MinimumLength of 2 to the following properties:
FirstName
LastName
Add the StringLength attribute with a MaximumLength of 500, and a MinimumLength of 10 to the Description property.
4
Inherit IValidatableObject on Speaker

Using the IValidatableObject interface, setup our Speaker class to validate that our EmailAddress property isn't a "Technology Live Conference" email address.

Set up our Speaker class to inherit the IValidatableObject interface. Note: your code will not compile at this point, but will soon once the Validate method is implemented.
Create a new method, Validate, with the following characteristics: - Add an access modifier of public. - Have a return type of IEnumerable<ValidationResult>. - Add a parameter of type ValidationContext. - It declares a variable of type List<ValidationResult>, and instantiates it to a new empty list of ValidationResult objects. - It checks if the EmailAddress property is not null and ends with "TechnologyLiveConference.com". (Use StringComparison to make this case insensitive)
If this is true, add a new ValidationResult with an ErrorMessage of "Technology Live Conference staff should not use their conference email addresses.". - Finally, it returns the List<ValidationResult> variable.
5
Add Client Side Validation to the Speaker Create View

Set up client side validation on our ConferenceTracker/Views/Speaker/Create.cshtml view.

At the end of our Create view, add a Scripts section.
Add the section using @section Scripts { }.
Inside our Scripts section, use @{await Html.RenderPartialAsync("_ValidationScriptsPartial");} to add _ValidateScriptsPartial to our Scripts section. The _ValidateScriptsPartial is included in the template by default. It contains references to jquery.validate. ASP.NET Core uses jquery.validate for client side validation.)
6
Add Validation Messages to Create View

Add a validation summary to our Create view's Create form.

Just inside our Create form, before it's first div, add a div tag with the following attributes:
asp-validation-summary set to "ModelOnly"
class set to "text-danger"
For each of our Create form's inputs, add a span tag with the following attributes:

asp-validate-for set to the same value as the asp-for of the corresponding input
class set to "text-danger"
7
Add ModelState Validation to the Create Action

Setup ModelState validation on our SpeakerController's HttpPost Create action.

Add a condition to our SpeakerController's HttpPost Create action that checks ModelState.IsValid.
If true, the action should perform the Create and RedirectToAction just like it did before.
If false, the action should return View with an argument of speaker. ASP.NET Core will automatically carry any validation errors back to the client so long as you've provided the model that failed validation.
8
Add Antiforgery and Binding to the Create Action

Setup the HttpPost Create action to validate an AntiForgeryToken and use Bind on our speaker parameter to prevent stuffing.

On our HttpPost Create action, add the ValidateAntiForgeryToken attribute. That's it! Anytime you make a Form in ASP.NET Core it, automatically adds a hidden input with the antiforgery token.
Instead of just accepting speaker as is, we should use the Bind attribute with an argument of "Id,FirstName,LastName,Description,EmailAddress,PhoneNumber" to restrict the action to only accepting those properties. Otherwise someone could maliciously alter their submission to set the IsStaff property.

*/