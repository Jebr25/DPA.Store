﻿using System;
using System.Collections.Generic;

namespace DPA.Store.DOMAIN.Core.Entities;

public partial class Category
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Product> Product { get; set; } = new List<Product>();
}
