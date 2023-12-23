using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using LoginForm.Models;
using LoginForm.Model;
using static Azure.Core.HttpHeader;
using Microsoft.EntityFrameworkCore;

namespace LoginForm.Repositories
{
    public class RepositoryBase
    {
        public CinemaContext db;
        public RepositoryBase()
        {
            db = new CinemaContext();
        }
    }
}
