using Business.Models;
using DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IFlightManager
    {
        Task<List<Journey>> GetJournies(string origin, string destination);
    }
}
