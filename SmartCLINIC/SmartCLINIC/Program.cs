using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartClinic.Data;
using SmartClinic.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<Korisnik>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
});

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    string[] roles = { "Admin", "Doktor", "Pacijent" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    if (!context.UslugeKlinike.Any())
    {
        context.UslugeKlinike.AddRange(

          new UslugaKlinike { Naziv = "Pregled doktora opće prakse", Oblast = "Opća praksa", Cijena = 30, TrajanjeUsluge = 20 },
new UslugaKlinike { Naziv = "Kontrolni pregled", Oblast = "Opća praksa", Cijena = 20, TrajanjeUsluge = 15 },
new UslugaKlinike { Naziv = "Mjerenje krvnog pritiska i savjetovanje", Oblast = "Opća praksa", Cijena = 10, TrajanjeUsluge = 10 },
new UslugaKlinike { Naziv = "Izdavanje ljekarskog uvjerenja", Oblast = "Opća praksa", Cijena = 35, TrajanjeUsluge = 20 },

new UslugaKlinike { Naziv = "Kardiološki pregled", Oblast = "Kardiologija", Cijena = 80, TrajanjeUsluge = 45 },
new UslugaKlinike { Naziv = "EKG snimanje", Oblast = "Kardiologija", Cijena = 25, TrajanjeUsluge = 15 },
new UslugaKlinike { Naziv = "Ultrazvuk srca", Oblast = "Kardiologija", Cijena = 100, TrajanjeUsluge = 45 },
new UslugaKlinike { Naziv = "Holter EKG", Oblast = "Kardiologija", Cijena = 120, TrajanjeUsluge = 1440 },
new UslugaKlinike { Naziv = "Kontrola srčane terapije", Oblast = "Kardiologija", Cijena = 45, TrajanjeUsluge = 20 },
new UslugaKlinike { Naziv = "Test opterećenja", Oblast = "Kardiologija", Cijena = 130, TrajanjeUsluge = 60 },

new UslugaKlinike { Naziv = "Stomatološki pregled", Oblast = "Stomatologija", Cijena = 25, TrajanjeUsluge = 15 },
new UslugaKlinike { Naziv = "Čišćenje kamenca", Oblast = "Stomatologija", Cijena = 50, TrajanjeUsluge = 30 },
new UslugaKlinike { Naziv = "Popravka zuba", Oblast = "Stomatologija", Cijena = 60, TrajanjeUsluge = 45 },
new UslugaKlinike { Naziv = "Vađenje zuba", Oblast = "Stomatologija", Cijena = 70, TrajanjeUsluge = 30 },
new UslugaKlinike { Naziv = "3D snimanje zuba", Oblast = "Stomatologija", Cijena = 80, TrajanjeUsluge = 20 },
new UslugaKlinike { Naziv = "Izbjeljivanje zuba", Oblast = "Stomatologija", Cijena = 150, TrajanjeUsluge = 60 },
new UslugaKlinike { Naziv = "Liječenje karijesa", Oblast = "Stomatologija", Cijena = 65, TrajanjeUsluge = 35 },

new UslugaKlinike { Naziv = "Dermatološki pregled", Oblast = "Dermatologija", Cijena = 60, TrajanjeUsluge = 30 },
new UslugaKlinike { Naziv = "Pregled mladeža", Oblast = "Dermatologija", Cijena = 50, TrajanjeUsluge = 25 },
new UslugaKlinike { Naziv = "Tretman akni", Oblast = "Dermatologija", Cijena = 70, TrajanjeUsluge = 40 },
new UslugaKlinike { Naziv = "Alergološko savjetovanje kože", Oblast = "Dermatologija", Cijena = 45, TrajanjeUsluge = 25 },
new UslugaKlinike { Naziv = "Uklanjanje bradavica", Oblast = "Dermatologija", Cijena = 90, TrajanjeUsluge = 35 },

new UslugaKlinike { Naziv = "Ginekološki pregled", Oblast = "Ginekologija", Cijena = 70, TrajanjeUsluge = 30 },
new UslugaKlinike { Naziv = "PAPA test", Oblast = "Ginekologija", Cijena = 35, TrajanjeUsluge = 20 },
new UslugaKlinike { Naziv = "Ginekološki ultrazvuk", Oblast = "Ginekologija", Cijena = 60, TrajanjeUsluge = 30 },
new UslugaKlinike { Naziv = "Savjetovanje", Oblast = "Ginekologija", Cijena = 30, TrajanjeUsluge = 20 },
new UslugaKlinike { Naziv = "4D ultrazvuk", Oblast = "Ginekologija", Cijena = 120, TrajanjeUsluge = 45 },
new UslugaKlinike { Naziv = "Savjetovanje o kontracepciji", Oblast = "Ginekologija", Cijena = 35, TrajanjeUsluge = 20 },
new UslugaKlinike { Naziv = "Kolposkopija", Oblast = "Ginekologija", Cijena = 90, TrajanjeUsluge = 30 },

new UslugaKlinike { Naziv = "Pedijatrijski pregled", Oblast = "Pedijatrija", Cijena = 40, TrajanjeUsluge = 25 },
new UslugaKlinike { Naziv = "Kontrola rasta i razvoja", Oblast = "Pedijatrija", Cijena = 30, TrajanjeUsluge = 20 },
new UslugaKlinike { Naziv = "Savjetovanje za ishranu djeteta", Oblast = "Pedijatrija", Cijena = 25, TrajanjeUsluge = 20 },
new UslugaKlinike { Naziv = "Pregled pred vakcinaciju", Oblast = "Pedijatrija", Cijena = 35, TrajanjeUsluge = 20 },
new UslugaKlinike { Naziv = "Alergotest za djecu", Oblast = "Pedijatrija", Cijena = 60, TrajanjeUsluge = 30 },

new UslugaKlinike { Naziv = "Kompletna krvna slika", Oblast = "Laboratorijske analize", Cijena = 15, TrajanjeUsluge = 10 },
new UslugaKlinike { Naziv = "Hormoni štitne žlijezde", Oblast = "Laboratorijske analize", Cijena = 45, TrajanjeUsluge = 15 },
new UslugaKlinike { Naziv = "Biohemijske analize", Oblast = "Laboratorijske analize", Cijena = 35, TrajanjeUsluge = 15 },
new UslugaKlinike { Naziv = "Analiza urina", Oblast = "Laboratorijske analize", Cijena = 10, TrajanjeUsluge = 10 },
new UslugaKlinike { Naziv = "Lipidni status", Oblast = "Laboratorijske analize", Cijena = 25, TrajanjeUsluge = 10 },
new UslugaKlinike { Naziv = "Tumorski markeri", Oblast = "Laboratorijske analize", Cijena = 120, TrajanjeUsluge = 15 },
new UslugaKlinike { Naziv = "Vitamin D analiza", Oblast = "Laboratorijske analize", Cijena = 35, TrajanjeUsluge = 10 },

new UslugaKlinike { Naziv = "Ultrazvuk abdomena", Oblast = "Radiologija", Cijena = 80, TrajanjeUsluge = 30 },
new UslugaKlinike { Naziv = "RTG snimak", Oblast = "Radiologija", Cijena = 50, TrajanjeUsluge = 20 },
new UslugaKlinike { Naziv = "CT pregled", Oblast = "Radiologija", Cijena = 180, TrajanjeUsluge = 45 },
new UslugaKlinike { Naziv = "MRI pregled", Oblast = "Radiologija", Cijena = 250, TrajanjeUsluge = 60 },
new UslugaKlinike { Naziv = "MRI mozga", Oblast = "Radiologija", Cijena = 280, TrajanjeUsluge = 60 },
new UslugaKlinike { Naziv = "CT pluća", Oblast = "Radiologija", Cijena = 190, TrajanjeUsluge = 40 },
new UslugaKlinike { Naziv = "Ultrazvuk štitne žlijezde", Oblast = "Radiologija", Cijena = 75, TrajanjeUsluge = 25 },
new UslugaKlinike { Naziv = "RTG kičme", Oblast = "Radiologija", Cijena = 55, TrajanjeUsluge = 20 },

new UslugaKlinike { Naziv = "Ortopedski pregled", Oblast = "Ortopedija", Cijena = 75, TrajanjeUsluge = 35 },
new UslugaKlinike { Naziv = "Pregled povrede zgloba", Oblast = "Ortopedija", Cijena = 60, TrajanjeUsluge = 30 },
new UslugaKlinike { Naziv = "Kontrola nakon povrede", Oblast = "Ortopedija", Cijena = 35, TrajanjeUsluge = 20 },
new UslugaKlinike { Naziv = "Ortopedski ultrazvuk", Oblast = "Ortopedija", Cijena = 80, TrajanjeUsluge = 35 },
new UslugaKlinike { Naziv = "Pregled kičme", Oblast = "Ortopedija", Cijena = 90, TrajanjeUsluge = 40 },
new UslugaKlinike { Naziv = "Sportska ortopedija", Oblast = "Ortopedija", Cijena = 110, TrajanjeUsluge = 45 },
new UslugaKlinike { Naziv = "Tretman istegnuća", Oblast = "Ortopedija", Cijena = 50, TrajanjeUsluge = 25 },
new UslugaKlinike { Naziv = "Savjetovanje za fizikalnu terapiju", Oblast = "Ortopedija", Cijena = 30, TrajanjeUsluge = 20 },

new UslugaKlinike { Naziv = "Neurološki pregled", Oblast = "Neurologija", Cijena = 85, TrajanjeUsluge = 45 },
new UslugaKlinike { Naziv = "EEG dijagnostika", Oblast = "Neurologija", Cijena = 120, TrajanjeUsluge = 60 },
new UslugaKlinike { Naziv = "EMNG pregled", Oblast = "Neurologija", Cijena = 140, TrajanjeUsluge = 50 },
new UslugaKlinike { Naziv = "Pregled zbog migrena", Oblast = "Neurologija", Cijena = 70, TrajanjeUsluge = 30 },
new UslugaKlinike { Naziv = "Neuromuskularna procjena", Oblast = "Neurologija", Cijena = 95, TrajanjeUsluge = 40 },
new UslugaKlinike { Naziv = "Kontrola terapije", Oblast = "Neurologija", Cijena = 40, TrajanjeUsluge = 25 },

new UslugaKlinike { Naziv = "Oftalmološki pregled", Oblast = "Oftalmologija", Cijena = 55, TrajanjeUsluge = 30 },
new UslugaKlinike { Naziv = "Određivanje dioptrije", Oblast = "Oftalmologija", Cijena = 30, TrajanjeUsluge = 20 },
new UslugaKlinike { Naziv = "Mjerenje očnog pritiska", Oblast = "Oftalmologija", Cijena = 25, TrajanjeUsluge = 15 },
new UslugaKlinike { Naziv = "Pregled očnog dna", Oblast = "Oftalmologija", Cijena = 60, TrajanjeUsluge = 30 },
new UslugaKlinike { Naziv = "Laserska dijagnostika oka", Oblast = "Oftalmologija", Cijena = 120, TrajanjeUsluge = 40 },
new UslugaKlinike { Naziv = "Kontrola dioptrije", Oblast = "Oftalmologija", Cijena = 20, TrajanjeUsluge = 15 },

new UslugaKlinike { Naziv = "Urološki pregled", Oblast = "Urologija", Cijena = 65, TrajanjeUsluge = 35 },
new UslugaKlinike { Naziv = "Ultrazvuk urinarnog sistema", Oblast = "Urologija", Cijena = 70, TrajanjeUsluge = 30 },
new UslugaKlinike { Naziv = "Analiza uroloških nalaza", Oblast = "Urologija", Cijena = 30, TrajanjeUsluge = 20 },
new UslugaKlinike { Naziv = "Kontrolni urološki pregled", Oblast = "Urologija", Cijena = 35, TrajanjeUsluge = 20 },
new UslugaKlinike { Naziv = "Kontrola bubrežnih funkcija", Oblast = "Urologija", Cijena = 55, TrajanjeUsluge = 20 },
new UslugaKlinike { Naziv = "Pregled prostate", Oblast = "Urologija", Cijena = 85, TrajanjeUsluge = 35 });



    context.SaveChanges();
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
