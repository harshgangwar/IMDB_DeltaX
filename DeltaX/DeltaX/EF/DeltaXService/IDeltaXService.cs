using System;
using System.Collections.Generic;
using DeltaX.Models;

namespace DeltaX.EF.DeltaXService
{
    public interface IDeltaXService : IDisposable
    {
        /// <summary>
        /// List of movies
        /// </summary>
        /// <returns>List of movies</returns>
        List<MoviesViewModel> GetListOfMovies();

        MoviesViewModel GetMovieDetailsById(int id);

    }
}