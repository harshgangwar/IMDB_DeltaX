using System;
using System.Collections.Generic;
using System.Linq;
using DeltaX.EF.DeltaXDB;
using DeltaX.EF.LoggingService;
using DeltaX.Models;

namespace DeltaX.EF.DeltaXService
{
    public class DeltaXService : IDeltaXService
    {
        #region Private member variables

        private bool _disposed;
        private readonly GenericUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public DeltaXService()
        {
            _unitOfWork = new GenericUnitOfWork();
        }

        #endregion

        public List<MoviesViewModel> GetListOfMovies()
        {
            try
            {
                // Here we can also use automapper 
                var moviesData =
                    _unitOfWork.Repository<Movie>().GetAll().Select( x=>
                        new MoviesViewModel
                        {
                            Id =  x.Id,
                            Name = x.Name,
                            Plot = x.Plot,
                            YearofRelease = x.YearofRelease,
                            ProducerName =  x.Producer.Name,
                            ProducerId =  x.ProducerId,

                            ActorId = x.Actor_Movie.Where(m => m.MovieId == x.Id).Select(y =>y.ActorId).ToList(),
                            ActorName = x.Actor_Movie.Where(m => m.MovieId == x.Id).Select(y => y.Actor.Name).ToList()

                        }
                    ).ToList();

                return moviesData;

            }
            catch (Exception exception)
            {
                // Here, we can use a errorlog table to save the error with all its path details
                ErrorLogService.WriteError(exception, "GetListOfMovies", "DeltaXService");
                throw;
            }
        }

        public MoviesViewModel GetMovieDetailsById(int id)
        {
            try
            {
                var movieDetials = _unitOfWork.Repository<Movie>().Get(id);

                // Here we can use automapper 
                var movie = new MoviesViewModel
                {
                    Id = movieDetials.Id,
                    Name = movieDetials.Name,
                    Plot = movieDetials.Plot
                };
                return movie;
            }
            catch (Exception ex)
            {
                // Here, we can use a errorlog table to save the error with all its path details
                ErrorLogService.WriteError(ex, "GetListOfMovies", "DeltaXService");
                throw;
            }
        }

        // Todo: Or we can use using to avoid using Dispose explicitly 
        #region Common Dispose methods

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _unitOfWork.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}