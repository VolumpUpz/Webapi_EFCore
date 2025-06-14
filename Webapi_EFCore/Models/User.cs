﻿using System;
using System.Collections.Generic;

namespace Webapi_EFCore.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public int? IsDeleted { get; set; }
    public string Role { get; set; }
}
