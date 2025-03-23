using Microsoft.EntityFrameworkCore;
using school_management_graphql.Data;
using school_management_graphql.GraphQL.Mutations;
using school_management_graphql.GraphQL.Types;
using school_management_graphql.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddPooledDbContextFactory<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<ISchoolRoomService, SchoolRoomService>();
builder.Services.AddScoped<IEquipmentService, EquipmentService>();
builder.Services.AddScoped<IFurnitureService, FurnitureService>();
builder.Services.AddScoped<IStudentService, StudentService>();

builder.Services
 .AddGraphQLServer()
 //.RegisterDbContext<AppDbContext>(DbContextKind.Pooled)
 //.RegisterService<ITeacherService>()
 .AddQueryType<QueryType>()
    .AddType<LabRoomType>()
    .AddType<ClassroomType>()
 .AddMutationType<Mutation>();
// Add services to the container.

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapGraphQL();

app.Run();
