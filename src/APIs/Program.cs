using APIs.Endpoints;
using Application;
using Asp.Versioning;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddEndpointsApiExplorer()
                    .AddSwaggerGen()
                    .RegisterApplicationServices()
                    .RegisterPersistenceServices(builder.Configuration)
                    .AddApiVersioning(options =>
                    {
                        options.DefaultApiVersion = new ApiVersion(1);
                        options.ReportApiVersions = true;
                        options.ApiVersionReader = new UrlSegmentApiVersionReader();
                    })
                    .AddApiExplorer(options =>
                    {
                        options.GroupNameFormat = "'v'V";
                        options.SubstituteApiVersionInUrl = true;
                    });
}

var app = builder.Build();
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();

    var apiVersionSet = app.NewApiVersionSet()
        .HasApiVersion(new ApiVersion(1))
        .ReportApiVersions()
        .Build();

    var versionGroup = app
        .MapGroup("Api/V{apiVersion:apiVersion}")
        .WithApiVersionSet(apiVersionSet);

    versionGroup.MapBuyerEndpoints();
    versionGroup.MapTransactionEndpoints();
    app.Run();
}