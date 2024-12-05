using System;
using System.Collections.Generic;

namespace PortfolioService.Models;

public partial class FeaturedItem
{
    public string FeaturedId { get; set; } = null!;

    public int? Priority { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string ThumbnailUrl { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string? TargetUrl { get; set; }
}
