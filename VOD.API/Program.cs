using Azure.Core;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices();
ConfigureAutoMapper();
ConfigureMiddleware();

void ConfigureServices()
{

	builder.Services.AddCors(policy =>
	{
		policy.AddPolicy("CorsAllAccessPolicy", opt =>
		opt.AllowAnyOrigin()
		 .AllowAnyHeader()
		 .AllowAnyMethod()
		 );
	});

	builder.Services.AddControllers();
	// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
	builder.Services.AddEndpointsApiExplorer();
	builder.Services.AddSwaggerGen();

	builder.Services.AddDbContext<VODContext>(
	options => options.UseSqlServer(
	 builder.Configuration.GetConnectionString("VODConnection")));

	builder.Services.AddScoped<IDbService, DbService>();

}

void ConfigureAutoMapper()
{
	var config = new AutoMapper.MapperConfiguration(cfg =>
	{
		cfg.CreateMap<Director, DirectorDTO>().ReverseMap();
		
		cfg.CreateMap<CreateDirectorDTO, Director>()
			.ForMember(dest => dest.Id, src => src.Ignore());

		cfg.CreateMap<DirectorDTO, Director>();


		cfg.CreateMap<Film, FilmDTO>()
		.ForMember(dest => dest.Director, src => src.MapFrom(s => s.Director.Name))
		.ForMember(dest => dest.Genres, src => src.MapFrom(s => s.Genres.Select(y => y.Name).ToList()))
		.ForMember(dest => dest.SimilarFilms, src => src.MapFrom(s => s.SimilarFilms.Select(y => y.Similar.Title).ToList()))
		.ReverseMap()
		.ForMember(dest => dest.Director, src => src.Ignore())
		.ForMember(dest => dest.Genres, src => src.Ignore())
		.ForMember(dest => dest.SimilarFilms, src => src.Ignore());

		cfg.CreateMap<FilmEditDTO, Film>();

		cfg.CreateMap<CreateFilmDTO, Film>()
			.ForMember(dest => dest.Id, src => src.Ignore());

		//.ForMember(dest => dest.Director, src => src.Ignore());

		cfg.CreateMap<GenreEditDTO, Genre>();

		cfg.CreateMap<Genre, GenreDTO>().ReverseMap();
		cfg.CreateMap<CreateGenreDTO, Genre>()
			.ForMember(dest => dest.Id, src => src.Ignore());

		cfg.CreateMap<GenreDTO, Genre>();

		cfg.CreateMap<FilmGenre, FilmGenreDTO>().ReverseMap();

		cfg.CreateMap<SimilarFilm, SimilarFilmDTO>().ReverseMap();

		//.ForMember(dest => dest.FilmId, src => src.Ignore());

	});
	var mapper = config.CreateMapper();
	builder.Services.AddSingleton(mapper);
}

void ConfigureMiddleware()
{
	var app = builder.Build();
	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();
		app.UseSwaggerUI();
	}

	app.UseHttpsRedirection();
	app.UseCors("CorsAllAccessPolicy");

	app.UseAuthorization();

	app.MapControllers();

	app.Run();
}
