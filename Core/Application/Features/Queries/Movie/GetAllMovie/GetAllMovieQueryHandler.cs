    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Abstraction.Services;
    using Application.Dtos.ActorDto;
    using Application.Dtos.MovieDto;
    using Application.Repositories.Actor;
    using Application.Repositories.Movie;
    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    namespace Application.Features.Queries.Movie.GetAllMovie
    {
        public class GetAllMovieQueryHandler : IRequestHandler<GetAllMovieQueryRequest, GetAllMovieQueryResponse>
        {
            private readonly IMovieReadRepository _movieReadRepository;
            private IMapper _mapper;
            private readonly ILoggerService _loggerService;

            public GetAllMovieQueryHandler(IMovieReadRepository movieReadRepository, IMapper mapper, ILoggerService loggerService)
            {
                _movieReadRepository = movieReadRepository;
                _mapper = mapper;
                _loggerService = loggerService;
            }

            public async Task<GetAllMovieQueryResponse> Handle(GetAllMovieQueryRequest request, CancellationToken cancellationToken)
            {
                var movies = await _movieReadRepository.Table
                    .Include(movie => movie.Actors)
                    .ToListAsync();

                var movieDtos = movies.Select(movie => new MovieDto
                {
                    Id = movie.Id,
                    Name = movie.Name,
                    Price = movie.Price,
                    Year = movie.Year,
                    Type = movie.Type
                }).ToList();

                var response = new GetAllMovieQueryResponse
                {
                    Movies = movieDtos
                };
                _loggerService.Write("tum movieler getirildii.");
                return _mapper.Map<GetAllMovieQueryResponse>(response);
            }


        }
    }