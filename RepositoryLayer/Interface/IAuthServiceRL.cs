﻿using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IAuthServiceRL
    {
       
        public string GenerateJwtToken(UserEntity user);
    }
}
