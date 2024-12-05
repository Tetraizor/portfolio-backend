namespace PortfolioService.Models;

public partial class Post
{
    public string PostId { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string UrlString { get; set; } = null!;

    public int Views { get; set; } = 0;

    public bool IsPinned { get; set; } = false;

    public string ThumbnailUrl { get; set; } = null!;

    public bool IsHidden { get; set; } = false;

    public bool IsDraft { get; set; } = false;
}
